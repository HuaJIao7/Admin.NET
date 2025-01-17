// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Checkin.DTO;

/// <summary>
/// 获取用户签到记录
/// </summary>
public class GetUserRecordResponse : DingtalkResponseErrorResultRequestId<GetUserRecordResponseResultDomain>
{
}

public class GetUserRecordResponseResultDomain
{
    /// <summary>
    /// 下次查询的游标，为null代表没有更多的数据
    /// </summary>
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public long? NextCursor { get; set; }

    /// <summary>
    /// 签到信息
    /// </summary>
    [JsonProperty("page_list")]
    [JsonPropertyName("page_list")]
    public List<GetUserRecordResponsePageDomain> PageList { get; set; }
}

public class GetUserRecordResponsePageDomain
{
    /// <summary>
    /// 签到时间，单位毫秒
    /// </summary>
    [JsonProperty("checkin_time")]
    [JsonPropertyName("checkin_time")]
    public long CheckinTime { get; set; }

    /// <summary>
    /// 签到照片URL列表
    /// </summary>
    [JsonProperty("image_list")]
    [JsonPropertyName("image_list")]
    public List<string> ImageList { get; set; }

    /// <summary>
    /// 签到详细地址
    /// </summary>
    [JsonProperty("detail_place")]
    [JsonPropertyName("detail_place")]
    public string DetailPlace { get; set; }

    /// <summary>
    /// 签到备注
    /// </summary>
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// 签到用户userId
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 签到地址
    /// </summary>
    [JsonProperty("place")]
    [JsonPropertyName("place")]
    public string Place { get; set; }

    /// <summary>
    /// 签到位置经度（暂未开放）
    /// </summary>
    [JsonProperty("longitude")]
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }

    /// <summary>
    /// 签到位置维度（暂未开放）
    /// </summary>
    [JsonProperty("latitude")]
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    /// <summary>
    /// 签到的拜访对象，可以为外部联系人的userId或者用户自己输入的名字
    /// </summary>
    [JsonProperty("visit_user")]
    [JsonPropertyName("visit_user")]
    public string VisitUser { get; set; }
}