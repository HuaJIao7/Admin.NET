// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Checkin.DTO;

/// <summary>
/// 获取部门用户签到记录
/// </summary>
public class GetDeptRecordResponse : DingtalkResponseError
{
    /// <summary>
    /// 返回结果
    /// </summary>
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public List<GetDeptRecordResponseData> Data { get; set; }
}

public class GetDeptRecordResponseData
{
    /// <summary>
    /// 员工姓名
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// 员工userId，不可修改
    /// </summary>
    [JsonProperty("userId")]
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    /// <summary>
    /// 头像URL
    /// </summary>
    [JsonProperty("avatar")]
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 签到时间，不可修改
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// 签到地址
    /// </summary>
    [JsonProperty("place")]
    [JsonPropertyName("place")]
    public string Place { get; set; }

    /// <summary>
    /// 签到详细地址
    /// </summary>
    [JsonProperty("detailPlace")]
    [JsonPropertyName("detailPlace")]
    public string DetailPlace { get; set; }

    /// <summary>
    /// 签到备注
    /// </summary>
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// 签到照片URL列表
    /// </summary>
    [JsonProperty("imageList")]
    [JsonPropertyName("imageList")]
    public List<string> ImageList { get; set; }

    /// <summary>
    /// 纬度
    /// </summary>
    [JsonProperty("latitude")]
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    [JsonProperty("longitude")]
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }
}