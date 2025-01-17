// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.Message.DTO;

using NewLife;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Message;

public class MessageRequest : IScoped
{
    private readonly IMessageRequestProxy _request;

    public MessageRequest(IMessageRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 发送工作通知 -- markdown消息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="agentId">发送消息时使用的微应用的AgentID</param>
    /// <param name="useridList">接收者的userid列表，最大用户列表长度100</param>
    /// <param name="deptIdList">接收者的部门id列表，最大列表长度20。接收者是部门ID时，包括子部门下的所有用户</param>
    /// <param name="mdTitle">首屏会话透出的展示内容</param>
    /// <param name="mdText">markdown格式的消息，最大不超过5000字符</param>
    /// <param name="toAllUser">是否发送给企业全部用户</param>
    /// <returns></returns>
    public async Task<SendCorpConversationResponse> SendCorpMarkdown(string accessToken, long agentId, List<string>? useridList, List<long> deptIdList
        , string mdTitle, string mdText, bool toAllUser = false)
    {
        var resStr = await _request.SendCorpConversation(accessToken, new SendCorpConversationRequest
        {
            AgentId = agentId,
            UserIdList = useridList?.ToArray().Join(),
            DeptIdList = deptIdList?.ToArray().Join(),
            ToAllUser = toAllUser,
            Msg = new MsgDomain
            {
                MsgType = SendCorpConversationMsgTypeEnum.markdown,
                Markdown = new MsgMarkdownDomain
                {
                    Title = mdTitle,
                    Text = mdText,
                }
            }
        });
        var res = resStr.ToObject<SendCorpConversationResponse>();
        return res;
    }

    /// <summary>
    /// 发送工作通知 -- 卡片消息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="agentId">发送消息时使用的微应用的AgentID</param>
    /// <param name="useridList">接收者的userid列表，最大用户列表长度100</param>
    /// <param name="deptIdList">接收者的部门id列表，最大列表长度20。接收者是部门ID时，包括子部门下的所有用户</param>
    /// <param name="markdown">消息内容，支持markdown，语法参考标准markdown语法。建议1000个字符以内</param>
    /// <param name="title">透出到会话列表和通知的文案</param>
    /// <param name="singleTitle">使用整体跳转ActionCard样式时的标题。必须与single_url同时设置，最长20个字符</param>
    /// <param name="singleUrl">消息点击链接地址，当发送消息为小程序时支持小程序跳转链接，最长500个字符</param>
    /// <param name="btnOrientation">使用独立跳转ActionCard样式时的按钮排列方式</param>
    /// <param name="btnJsonList">使用独立跳转ActionCard样式时的按钮列表；必须与btn_orientation同时设置，且长度不超过1000字符</param>
    /// <param name="toAllUser">是否发送给企业全部用户</param>
    /// <returns></returns>
    public async Task<SendCorpConversationResponse> SendCorpActionCard(string accessToken, long agentId, List<string>? useridList, List<long>? deptIdList
        , string markdown, string? title = null, string? singleTitle = null, string? singleUrl = null
        , ActionCardBtnOrientationEnum? btnOrientation = null, MsgActionCardBtnJsonListDomain[]? btnJsonList = null
        , bool toAllUser = false)
    {
        var resStr = await _request.SendCorpConversation(accessToken, new SendCorpConversationRequest
        {
            AgentId = agentId,
            UserIdList = useridList?.ToArray().Join(),
            DeptIdList = deptIdList?.ToArray().Join(),
            ToAllUser = toAllUser,
            Msg = new MsgDomain
            {
                MsgType = SendCorpConversationMsgTypeEnum.action_card,
                ActionCard = new MsgActionCardDomain
                {
                    Markdown = markdown,
                    Title = title,
                    SingleTitle = singleTitle,
                    SingleUrl = singleUrl,
                    BtnOrientation = btnOrientation,
                    BtnJsonList = btnJsonList
                }
            }
        });
        var res = resStr.ToObject<SendCorpConversationResponse>();
        return res;
    }
}