// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Top;

public interface ITopRequestProxy : IHttpDispatchProxy
{
    [Post("https://oapi.dingtalk.com/topapi/v2/user/getuserinfo"), JsonSerialization(typeof(NewtonsoftJsonSerializerProvider))]
    Task<string> GetUserInfo([QueryString("access_token")] string accessToken, [Body] DTO.GetUserInfoRequest request);

    [Post("https://api.dingtalk.com/v1.0/oauth2/accessToken"), JsonSerialization(typeof(NewtonsoftJsonSerializerProvider))]
    Task<string> GetAccessToken([Body] DTO.GetAccessTokenRequest request);

    [Post("https://api.dingtalk.com/v1.0/oauth2/jsapiTickets"), JsonSerialization(typeof(NewtonsoftJsonSerializerProvider))]
    Task<string> GetApiTickets([Headers("x-acs-dingtalk-access-token")] string accessToken);
}