// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip.DTO;

/// <summary>
/// 获取阿里商旅访问地址
/// </summary>
public class GetAliTripAddressRequest
{
    [JsonProperty("request")]
    [JsonPropertyName("request")]
    public AliTripAddressRequestDomain Request { get; set; }
}

public class AliTripAddressRequestDomain
{
    /// <summary>
    /// 用户id
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string? UserId { get; set; }

    [JsonProperty("corpid")]
    [JsonPropertyName("corpid")]
    public string CorpId { get; set; }

    /// <summary>
    /// 类目类型：1：机票,2：火车票,3：酒店,4：用车
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public AliTripTypeEnum Type { get; set; }

    /// <summary>
    /// 类目类型
    /// </summary>
    [SuppressSniffer]
    public enum AliTripTypeEnum
    {
        机票 = 1,
        火车票 = 2,
        酒店 = 3,
        用车 = 4
    }

    /// <summary>
    /// 操作类型：1：预订,2：我的订单列表,3：商旅管理后台，如果需要获取该场景的地址，只需提供corpid，userid,4：商旅h5主页
    /// </summary>
    [JsonProperty("action_type")]
    [JsonPropertyName("action_type")]
    public AliTripActionTypeEnum ActionType { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [SuppressSniffer]
    public enum AliTripActionTypeEnum
    {
        预订 = 1,
        我的订单列表 = 2,
        商旅管理后台 = 3,
        商旅h5主页 = 4
    }

    /// <summary>
    /// 第三方行程ID。存在代表通过审批单预订，不存在代表特殊场景：普通员工是预览，特殊授权人和代订人是免审批预订场景。
    /// </summary>
    [JsonProperty("itinerary_id")]
    [JsonPropertyName("itinerary_id")]
    public string ItineraryId { get; set; }

    /// <summary>
    /// 员工第一次使用用车需要手机号，与司机联系。
    /// </summary>
    [JsonProperty("phone")]
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}