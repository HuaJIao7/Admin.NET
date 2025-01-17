// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.Top.DTO;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Top;

public class TopRequest : IScoped
{
    private readonly ITopRequestProxy _request;

    public TopRequest(ITopRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 通过免登码获取用户信息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="code">免登授权码</param>
    /// <returns></returns>
    public async Task<GetUserInfoResponse> GetUserInfo(string accessToken, string code)
    {
        var resStr = await _request.GetUserInfo(accessToken, new GetUserInfoRequest { Code = code });
        var res = resStr.ToObject<GetUserInfoResponse>();
        return res;
    }

    /// <summary>
    /// 获取企业内部应用的accessToken
    /// </summary>
    /// <param name="appKey">已创建的企业内部应用的AppKey</param>
    /// <param name="appSecret">已创建的企业内部应用的AppSecret</param>
    /// <returns></returns>
    public async Task<GetAccessTokenResponse> GetAccessToken(string appKey, string appSecret)
    {
        var resStr = await _request.GetAccessToken(new GetAccessTokenRequest
        {
            AppKey = appKey,
            AppSecret = appSecret
        });
        var res = resStr.ToObject<GetAccessTokenResponse>();
        return res;
    }

    /// <summary>
    /// 获取jsapiTicket
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    public async Task<GetApiTicketResponse> GetJsApiTicket(string accessToken)
    {
        var resStr = await _request.GetApiTickets(accessToken);
        var res = resStr.ToObject<GetApiTicketResponse>();
        return res;
    }
}