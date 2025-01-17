// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Newtonsoft.Json.Converters;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Message.DTO;

/// <summary>
/// 发送工作通知
/// </summary>
public class SendCorpConversationRequest
{
    /// <summary>
    /// 发送消息时使用的微应用的AgentID
    /// </summary>
    [JsonProperty("agent_id")]
    [JsonPropertyName("agent_id")]
    public long? AgentId { get; set; }

    /// <summary>
    /// 接收者的userid列表，最大用户列表长度100
    /// </summary>
    [JsonProperty("userid_list")]
    [JsonPropertyName("userid_list")]
    public string? UserIdList { get; set; }

    /// <summary>
    /// 接收者的部门id列表，最大列表长度20
    /// </summary>
    [JsonProperty("dept_id_list")]
    [JsonPropertyName("dept_id_list")]
    public string? DeptIdList { get; set; }

    /// <summary>
    /// 是否发送给企业全部用户
    /// </summary>
    [JsonProperty("to_all_user")]
    [JsonPropertyName("to_all_user")]
    public bool? ToAllUser { get; set; }

    /// <summary>
    /// 消息内容，最长不超过2048个字节
    /// </summary>
    [JsonProperty("msg")]
    [JsonPropertyName("msg")]
    public MsgDomain Msg { get; set; }
}

public class MsgDomain
{
    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonProperty("msgtype")]
    [JsonPropertyName("msgtype")]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SendCorpConversationMsgTypeEnum MsgType { get; set; }

    /// <summary>
    /// 文本消息
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public MsgTextDomain? Text { get; set; }

    /// <summary>
    /// 图片消息
    /// </summary>
    [JsonProperty("image")]
    [JsonPropertyName("image")]
    public MsgImageDomain? Image { get; set; }

    /// <summary>
    /// 语音消息
    /// </summary>
    [JsonProperty("voice")]
    [JsonPropertyName("voice")]
    public MsgVoiceDomain? Voice { get; set; }

    /// <summary>
    /// 文件消息
    /// </summary>
    [JsonProperty("file")]
    [JsonPropertyName("file")]
    public MsgVoiceDomain? File { get; set; }

    /// <summary>
    /// 链接消息
    /// </summary>
    [JsonProperty("link")]
    [JsonPropertyName("link")]
    public MsgLinkDomain? Link { get; set; }

    /// <summary>
    /// OA消息
    /// </summary>
    [JsonProperty("oa")]
    [JsonPropertyName("oa")]
    public MsgOaDomain? Oa { get; set; }

    /// <summary>
    /// markdown消息
    /// </summary>
    [JsonProperty("markdown")]
    [JsonPropertyName("markdown")]
    public MsgMarkdownDomain? Markdown { get; set; }

    /// <summary>
    /// 卡片消息
    /// </summary>
    [JsonProperty("action_card")]
    [JsonPropertyName("action_card")]
    public MsgActionCardDomain? ActionCard { get; set; }
}

/// <summary>
/// 消息类型
/// </summary>
[SuppressSniffer]
[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
public enum SendCorpConversationMsgTypeEnum
{
    text, image, voice, file, link, oa, markdown, action_card
}

/// <summary>
/// 文本消息
/// </summary>
public class MsgTextDomain
{
    /// <summary>
    /// 消息内容，建议500字符以内
    /// </summary>
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}

/// <summary>
/// 图片消息
/// </summary>
public class MsgImageDomain
{
    /// <summary>
    /// 媒体文件mediaid，建议宽600像素 x 400像素，宽高比3 : 2
    /// </summary>
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
}

/// <summary>
/// 语音消息
/// </summary>
public class MsgVoiceDomain
{
    /// <summary>
    /// 媒体文件ID
    /// </summary>
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }

    /// <summary>
    /// 正整数，小于60，表示音频时长
    /// </summary>
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public string Duration { get; set; }
}

/// <summary>
/// 文件消息
/// </summary>
public class MsgFileDomain
{
    /// <summary>
    /// 媒体文件ID，引用的媒体文件最大10MB
    /// </summary>
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
}

/// <summary>
/// 链接消息
/// </summary>
public class MsgLinkDomain
{
    /// <summary>
    /// 消息点击链接地址，当发送消息为小程序时支持小程序跳转链接
    /// </summary>
    [JsonProperty("messageUrl")]
    [JsonPropertyName("messageUrl")]
    public string MessageUrl { get; set; }

    [JsonProperty("picUrl")]
    [JsonPropertyName("picUrl")]
    public string PicUrl { get; set; }

    /// <summary>
    /// 消息标题，建议100字符以内
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// 消息描述，建议500字符以内
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

/// <summary>
/// OA消息
/// </summary>
public class MsgOaDomain
{
    /// <summary>
    /// 消息点击链接地址，当发送消息为小程序时支持小程序跳转链接
    /// </summary>
    [JsonProperty("message_url")]
    [JsonPropertyName("message_url")]
    public string MessageUrl { get; set; }

    /// <summary>
    /// PC端点击消息时跳转到的地址
    /// </summary>
    [JsonProperty("pc_message_url")]
    [JsonPropertyName("pc_message_url")]
    public string? PcMessageUrl { get; set; }

    /// <summary>
    /// 消息头部内容
    /// </summary>
    [JsonProperty("head")]
    [JsonPropertyName("head")]
    public MsgOaHeadDomain Head { get; set; }

    /// <summary>
    /// 消息体
    /// </summary>
    [JsonProperty("body")]
    [JsonPropertyName("body")]
    public MsgOaBodyDomain Body { get; set; }

    /// <summary>
    /// 消息状态栏，只支持接收者的userid列表，userid最多不能超过5个人
    /// </summary>
    [JsonProperty("status_bar")]
    [JsonPropertyName("status_bar")]
    public MsgOaStatusBarDomain? StatusBar { get; set; }
}

public class MsgOaHeadDomain
{
    /// <summary>
    /// 消息头部的背景颜色。长度限制为8个英文字符，其中前2为表示透明度，后6位表示颜色值。不要添加0x。
    /// </summary>
    [JsonProperty("bgcolor")]
    [JsonPropertyName("bgcolor")]
    public string BgColor { get; set; }

    /// <summary>
    /// 消息的头部标题
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class MsgOaStatusBarDomain
{
    /// <summary>
    /// 状态栏文案
    /// </summary>
    [JsonProperty("status_value")]
    [JsonPropertyName("status_value")]
    public string StatusValue { get; set; }

    /// <summary>
    /// 状态栏背景色，默认为黑色，推荐0xFF加六位颜色值
    /// </summary>
    [JsonProperty("status_bg")]
    [JsonPropertyName("status_bg")]
    public string StatusBg { get; set; }
}

public class MsgOaBodyDomain
{
    /// <summary>
    /// 消息体的标题，建议50个字符以内
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// 消息体的表单，最多显示6个，超过会被隐藏
    /// </summary>
    [JsonProperty("form")]
    [JsonPropertyName("form")]
    public MsgOaBodyFormDomain[]? Form { get; set; }

    /// <summary>
    /// 单行富文本信息
    /// </summary>
    [JsonProperty("rich")]
    [JsonPropertyName("rich")]
    public MsgOaBodyRichDomain? Rich { get; set; }

    /// <summary>
    /// 消息体的内容，最多显示3行
    /// </summary>
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// 消息体中的图片，支持图片资源@mediaId。建议宽600像素 x 400像素，宽高比3 : 2
    /// </summary>
    [JsonProperty("image")]
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>
    /// 自定义的附件数目。此数字仅供显示，钉钉不作验证
    /// </summary>
    [JsonProperty("file_count")]
    [JsonPropertyName("file_count")]
    public string? FileCount { get; set; }

    /// <summary>
    /// 自定义的作者名字
    /// </summary>
    [JsonProperty("author")]
    [JsonPropertyName("author")]
    public string? Author { get; set; }
}

public class MsgOaBodyRichDomain
{
    /// <summary>
    /// 单行富文本信息的数目
    /// </summary>
    [JsonProperty("num")]
    [JsonPropertyName("num")]
    public string? Num { get; set; }

    /// <summary>
    /// 单行富文本信息的单位
    /// </summary>
    [JsonProperty("unit")]
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }
}

public class MsgOaBodyFormDomain
{
    /// <summary>
    /// 消息体的关键字
    /// </summary>
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// 消息体的关键字对应的值
    /// </summary>
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>
/// markdown消息
/// </summary>
public class MsgMarkdownDomain
{
    /// <summary>
    /// 首屏会话透出的展示内容
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// markdown格式的消息，最大不超过5000字符
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

/// <summary>
/// 卡片消息
/// </summary>
public class MsgActionCardDomain
{
    /// <summary>
    /// 透出到会话列表和通知的文案
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// 消息内容，支持markdown，语法参考标准markdown语法。建议1000个字符以内
    /// </summary>
    [JsonProperty("markdown")]
    [JsonPropertyName("markdown")]
    public string Markdown { get; set; }

    /// <summary>
    /// 使用整体跳转ActionCard样式时的标题。必须与single_url同时设置，最长20个字符
    /// </summary>
    [JsonProperty("single_title")]
    [JsonPropertyName("single_title")]
    public string? SingleTitle { get; set; }

    /// <summary>
    /// 消息点击链接地址，当发送消息为小程序时支持小程序跳转链接，最长500个字符
    /// </summary>
    [JsonProperty("single_url")]
    [JsonPropertyName("single_url")]
    public string? SingleUrl { get; set; }

    /// <summary>
    /// 使用独立跳转ActionCard样式时的按钮排列方式：0：竖直排列，1：横向排列。必须与btn_json_list同时设置
    /// </summary>
    [JsonProperty("btn_orientation")]
    [JsonPropertyName("btn_orientation")]
    public ActionCardBtnOrientationEnum? BtnOrientation { get; set; }

    /// <summary>
    /// 使用独立跳转ActionCard样式时的按钮列表；必须与btn_orientation同时设置，且长度不超过1000字符
    /// </summary>
    [JsonProperty("btn_json_list")]
    [JsonPropertyName("btn_json_list")]
    public MsgActionCardBtnJsonListDomain[]? BtnJsonList { get; set; }
}

/// <summary>
/// 使用独立跳转ActionCard样式时的按钮排列方式
/// </summary>
[SuppressSniffer]
public enum ActionCardBtnOrientationEnum
{
    竖直排列 = 0,
    横向排列 = 1
}

public class MsgActionCardBtnJsonListDomain
{
    /// <summary>
    /// 使用独立跳转ActionCard样式时的按钮的标题，最长20个字符
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// 使用独立跳转ActionCard样式时的跳转链接，最长700个字符
    /// </summary>
    [JsonProperty("action_url")]
    [JsonPropertyName("action_url")]
    public string ActionUrl { get; set; }
}