// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.Top;
using Admin.NET.Plugin.DingTalk.RequestProxy.HRM;
using Admin.NET.Plugin.DingTalk.RequestProxy.HRM.DTO;
using Admin.NET.Core.Service;

namespace Admin.NET.Plugin.DingTalk.Service;

/// <summary>
/// 钉钉服务 🧩
/// </summary>
[ApiDescriptionSettings(DingTalkConst.GroupName, Order = 100)]
public class DingTalkService : IDynamicApiController, IScoped
{
    private readonly IDingTalkApi _dingTalkApi;
    private readonly DingTalkOptions _dingTalkOptions;
    private readonly SysCacheService _sysCacheService;
    private readonly TopRequest _topRequest;
    private readonly HrmRequest _hrmRequest;
    private readonly string _accessTokenKey;

    public DingTalkService(IDingTalkApi dingTalkApi,
        IOptions<DingTalkOptions> dingTalkOptions,
        TopRequest topRequest,
        HrmRequest hrmRequest,
        SysCacheService sysCacheService)
    {
        _dingTalkApi = dingTalkApi;
        _dingTalkOptions = dingTalkOptions.Value;
        _sysCacheService = sysCacheService;
        _topRequest = topRequest;
        _hrmRequest = hrmRequest;
        _accessTokenKey = DingTalkConst.AccessTokenKeyPrefix + _dingTalkOptions.ClientId;
    }

    [HttpGet, DisplayName("获取花名册元数据")]
    public async Task<GetRosterMetaResponse> GetRosterMeta()
    {
        var token = await GetDingTalkToken();
        var res = await _hrmRequest.GetRosterMeta(token, _dingTalkOptions.AgentId);
        return res;
    }

    /// <summary>
    /// 获取企业内部应用的access_token
    /// </summary>
    /// <returns></returns>
    [HttpGet, DisplayName("获取企业内部应用的access_token")]
    public async Task<string> GetDingTalkToken()
    {
        var token = _sysCacheService.Get<string>(_accessTokenKey);
        if (token != null) return token;

        var tokenRes = await _topRequest.GetAccessToken(_dingTalkOptions.ClientId, _dingTalkOptions.ClientSecret);
        _sysCacheService.Set(_accessTokenKey, tokenRes.AccessToken, TimeSpan.FromSeconds(tokenRes.ExpireIn));
        return tokenRes.AccessToken;
    }

    /// <summary>
    /// 获取在职员工列表 🔖
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="statusList"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    [HttpPost, DisplayName("获取在职员工列表")]
    public async Task<EmployeeQueryOnJobResponse> GetDingTalkCurrentEmployeesList(string accessToken, List<string> statusList, int size, int offset)
    {
        return await _hrmRequest.EmployeeQueryOnJob(accessToken, statusList, size, offset);
    }

    /// <summary>
    /// 获取员工花名册字段信息 🔖
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="userIdList"></param>
    /// <param name="fieldFilterList"></param>
    /// <param name="appAgentId"></param>
    /// <returns></returns>
    [HttpPost, DisplayName("获取员工花名册字段信息")]
    public async Task<RosterListsQueryResponse> GetDingTalkCurrentEmployeesRosterList(string accessToken, List<string> userIdList, List<string> fieldFilterList, long appAgentId)
    {
        return await _hrmRequest.RosterListsQuery(accessToken, userIdList, fieldFilterList, appAgentId);
    }

    /// <summary>
    /// 发送钉钉互动卡片 🔖
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("给指定用户发送钉钉互动卡片")]
    public async Task<DingTalkSendInteractiveCardsOutput> DingTalkSendInteractiveCards(string accessToken, DingTalkSendInteractiveCardsInput input)
    {
        return await _dingTalkApi.DingTalkSendInteractiveCards(accessToken, input);
    }
}