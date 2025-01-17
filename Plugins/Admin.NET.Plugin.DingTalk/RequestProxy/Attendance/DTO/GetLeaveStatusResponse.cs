// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 查询请假状态
/// </summary>
public class GetLeaveStatusResponse : DingTalkResponseAll<GetLeaveStatusResult>
{
}

public class GetLeaveStatusResult
{
    /// <summary>
    /// 是否还有更多的数据
    /// </summary>
    [JsonProperty("has_more")]
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    /// <summary>
    /// 请假状态列表
    /// </summary>
    [JsonProperty("leave_status")]
    [JsonPropertyName("leave_status")]
    public List<LeaveStatusDomain> LeaveStatus { get; set; }
}

public class LeaveStatusDomain
{
    /// <summary>
    /// 请假单位：    percent_day：天    percent_hour：小时
    /// </summary>
    [JsonProperty("duration_unit")]
    [JsonPropertyName("duration_unit")]
    public string DurationUnit { get; set; }

    /// <summary>
    /// 假期时长*100，例如用户请假时长为1天，该值就等于100。
    /// </summary>
    [JsonProperty("duration_percent")]
    [JsonPropertyName("duration_percent")]
    public double DurationPercent { get; set; }

    /// <summary>
    /// 请假结束时间，Unix时间戳。
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; set; }

    /// <summary>
    /// 请假开始时间，Unix时间戳。
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }

    /// <summary>
    /// 用户ID。
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
}