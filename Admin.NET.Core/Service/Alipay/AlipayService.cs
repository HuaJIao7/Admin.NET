// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using Microsoft.AspNetCore.Hosting;
// ReSharper disable All

namespace Admin.NET.Core.Service;

/// <summary>
/// 支付宝支付服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 240)]
public class AlipayService : IDynamicApiController, ITransient
{
    private readonly IEnumerable<IAlipayNotify> _alipayNotifyList;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly SysConfigService _sysConfigService;
    private readonly IHttpContextAccessor _httpContext;
    private readonly AlipayOptions _alipayOptions;
    private readonly IAopClient _alipayClient;
    private readonly UserManager _userManager;

    public AlipayService(
        UserManager userManager,
        IHttpContextAccessor httpContext,
        SysConfigService sysConfigService,
        IWebHostEnvironment webHostEnvironment,
        IOptions<AlipayOptions> alipayOptions)
    {
        _userManager = userManager;
        _httpContext = httpContext;
        _sysConfigService = sysConfigService;
        _alipayOptions = alipayOptions.Value;
        _webHostEnvironment = webHostEnvironment;
        _alipayNotifyList = App.GetServices<IAlipayNotify>();

        // 初始化支付宝客户端
        string path = App.WebHostEnvironment.ContentRootPath;
        _alipayClient = new DefaultAopClient(new AlipayConfig
        {
            Format = "json",
            Charset = "UTF-8",
            AppId = _alipayOptions.AppId,
            SignType = _alipayOptions.SignType,
            ServerUrl = _alipayOptions.ServerUrl,
            PrivateKey = _alipayOptions.PrivateKey,
            EncryptKey = _alipayOptions.EncryptKey,
            AppCertPath = Path.Combine(path, _alipayOptions.AppCertPath),
            RootCertPath = Path.Combine(path, _alipayOptions.RootCertPath),
            AlipayPublicCertPath = Path.Combine(path, _alipayOptions.AlipayPublicCertPath)
        });
    }

    /// <summary>
    /// 获取授权信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [NonUnify]
    [AllowAnonymous]
    [DisplayName("获取授权信息")]
    [ApiDescriptionSettings(Name = "GetAuthInfo"), HttpGet]
    public ActionResult GetAuthInfo([FromQuery] AlipayAuthInfoInput input)
    {
        var type = input.UserId.Split('-').FirstOrDefault().ToInt();
        var userId = input.UserId.Split('-').LastOrDefault().ToLong();

        // 当前网页接口地址
        var currentUrl = $"{_httpContext.HttpContext!.Request.GetOrigin()}{_httpContext.HttpContext!.Request.Path}?userId={input.UserId}";
        if (string.IsNullOrEmpty(input.AuthCode))
        {
            // 重新授权
            var url = $"{_alipayOptions.AuthUrl}?app_id={_alipayOptions.AppId}&scope=auth_user&redirect_uri={currentUrl}";
            return new RedirectResult(url);
        }

        // 组装授权请求参数
        AlipaySystemOauthTokenRequest request = new()
        {
            GrantType = AlipayConst.GrantType,
            Code = input.AuthCode
        };
        AlipaySystemOauthTokenResponse response = _alipayClient.CertificateExecute(request);

        // token换取用户信息
        AlipayUserInfoShareRequest infoShareRequest = new();
        AlipayUserInfoShareResponse info = _alipayClient.CertificateExecute(infoShareRequest, response.AccessToken);

        // 循环执行扫码后需要执行的业务逻辑，需要至少一个继承方法返回true，否则抛出异常
        var pass = false;
        foreach (var notify in _alipayNotifyList)
            if (notify.ScanCallback(type, userId, info)) pass = true;
        if (!pass) throw Oops.Oh("未处理的授权逻辑");

        // 执行完，重定向到指定界面
        var authPageUrl = _sysConfigService.GetConfigValue<string>(ConfigConst.AlipayAuthPageUrl + type).Result;
        return new RedirectResult(authPageUrl);
    }

    /// <summary>
    /// 支付回调
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("支付回调")]
    [ApiDescriptionSettings(Name = "Notify"), HttpPost]
    public string Notify()
    {
        SortedDictionary<string, string> sorted = new();
        foreach (string key in _httpContext.HttpContext!.Request.Form.Keys)
            sorted.Add(key, _httpContext.HttpContext.Request.Form[key]);

        string alipayPublicKey = Path.Combine(_webHostEnvironment.ContentRootPath, _alipayOptions.AlipayPublicCertPath!.Replace('/','\\').TrimStart('\\'));
        bool signVerified = AlipaySignature.RSACertCheckV1(sorted, alipayPublicKey, "UTF-8", _alipayOptions.SignType); // 调用SDK验证签名
        if (!signVerified) throw Oops.Oh("交易失败");

        var outTradeNo = sorted.GetValueOrDefault("out_trade_no");
        try
        {
            // 记录回调日志
            File.AppendAllText($"{_webHostEnvironment.ContentRootPath}\\AlipayLog\\Notify-{DateTime.Today:yyyy-MM-dd}.txt",
                $"支付宝支付到平台({DateTime.Now:yyyy-MM-dd HH:mm:ss})：{Environment.NewLine} " +
                $"登录人：{_userManager.UserId}-{_userManager.RealName}{Environment.NewLine} " +
                $"IP：{App.HttpContext?.GetRemoteIpAddressToIPv4(true)} {Environment.NewLine} " +
                $"交易号：{outTradeNo}{Environment.NewLine} " +
                $"参数：{JSON.Serialize(sorted)}{Environment.NewLine} " +
                $"-----------------------------------------------------------------------------------------------------------------------" +
                $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Log.Error("支付宝支付回调日志写入失败：", ex);
        }

        if (sorted.GetValueOrDefault(AlipayConst.TradeStatus) == AlipayConst.TradeSuccess)
        {
            // 约定交易码前四位为类型码，后面为订单号
            var tradeNo = long.Parse(outTradeNo);
            var type = long.Parse(outTradeNo.Substring(0, 4));

            // 循环执行业务逻辑，若都未处理(回调全部返回false)则交易失败
            var isError = true;
            foreach (var notify in _alipayNotifyList)
                if (notify.TopUpCallback(type, tradeNo)) isError = false;
            if (isError) throw Oops.Oh("交易失败");
        }
        return "success";
    }

    /// <summary>
    ///  统一收单下单并支付页面接口
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("统一收单下单并支付页面接口")]
    [ApiDescriptionSettings(Name = "AlipayTradePagePay"), HttpPost]
    public string AlipayTradePagePay(AlipayTradePagePayInput input)
    {
        AlipayTradeWapPayRequest request = new();

        // 组装业务参数model
        AlipayTradeWapPayModel model = new()
        {
            Subject = input.Subject,
            OutTradeNo = input.OutTradeNo,
            TotalAmount = input.TotalAmount,
            Body = input.Body,
            ProductCode = "QUICK_WAP_WAY",
            TimeExpire = input.TimeoutExpress
        };
        request.SetBizModel(model);

        // 设置异步通知接收地址
        request.SetNotifyUrl(_alipayOptions.NotifyUrl);

        var response = _alipayClient.SdkExecute(request);
        if (response.IsError) throw Oops.Oh(response.SubMsg);
        return $"{_alipayOptions.ServerUrl}?{response.Body}";
    }

    /// <summary>
    ///  交易预创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("交易预创建")]
    [ApiDescriptionSettings(Name = "AlipayPreCreate"), HttpPost]
    public string AlipayPreCreate(AlipayPreCreateInput input)
    {
        AlipayTradePrecreateRequest request = new();

        // 设置异步通知接收地址
        request.SetNotifyUrl(_alipayOptions.NotifyUrl);

        // 组装业务参数model
        AlipayTradePrecreateModel model = new()
        {
            Subject = input.Subject,
            OutTradeNo = input.OutTradeNo,
            TotalAmount = input.TotalAmount,
            TimeoutExpress = input.TimeoutExpress
        };
        request.SetBizModel(model);

        var response = _alipayClient.CertificateExecute(request);
        if (response.IsError) throw Oops.Oh(response.SubMsg);
        return response.QrCode;
    }

    /// <summary>
    /// 单笔转账到支付宝账户
    ///  https://opendocs.alipay.com/open/62987723_alipay.fund.trans.uni.transfer
    /// </summary>
    [NonAction]
    public Task<AlipayFundTransUniTransferResponse> Transfer(AlipayFundTransUniTransferInput input)
    {
        // 构造请求参数以调用接口
        AlipayFundTransUniTransferRequest request = new();
        AlipayFundTransUniTransferModel model = new();
        model.BizScene = AlipayConst.BizScene;
        model.ProductCode = AlipayConst.ProductCode;

        // 设置商家侧唯一订单号
        model.OutBizNo = input.OutBizNo;

        // 设置订单总金额
        model.TransAmount = input.TransAmount.ToString();

        // 设置转账业务的标题
        model.OrderTitle = input.OrderTitle;

        // 设置收款方信息
        Participant payeeInfo = new();
        payeeInfo.CertType = input.CertType.ToString();
        payeeInfo.CertNo = input.CertNo;
        payeeInfo.Identity = input.Identity;
        payeeInfo.Name = input.Name;
        payeeInfo.IdentityType = input.IdentityType.ToString();
        model.PayeeInfo = payeeInfo;

        // 设置业务备注
        model.Remark = input.Remark;

        // 设置转账业务请求的扩展参数
        string payerShowNameUseAlias = input.PayerShowNameUseAlias.ToString().ToLower();
        model.BusinessParams = $"{{\"payer_show_name_use_alias\":\"{payerShowNameUseAlias}\"}}";

        request.SetBizModel(model);
        var response = _alipayClient.CertificateExecute(request);

        try
        {
            File.AppendAllText($"{_webHostEnvironment.ContentRootPath}\\AlipayLog\\{DateTime.Today:yyyy-MM-dd}.txt",
                $"支付宝付款到账户({DateTime.Now:yyyy-MM-dd HH:mm:ss})：{Environment.NewLine} " +
                $"登录人：{_userManager.UserId}-{_userManager.RealName}{Environment.NewLine} " +
                $"IP：{App.HttpContext?.GetRemoteIpAddressToIPv4(true)} {Environment.NewLine} " +
                $"参数：{JSON.Serialize(model)}{Environment.NewLine} " +
                $"返回：{JSON.Serialize(response)}{Environment.NewLine}" +
                $"-----------------------------------------------------------------------------------------------------------------------" +
                $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
        }
        catch(Exception ex)
        {
            Log.Error("单笔转账到支付宝账户日志写入失败：", ex);
        }
        return Task.FromResult(response);
    }
}