// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 出勤批准完成响应
/// </summary>
public class AttendanceApprovalsFinishResponse : DingtalkResponseSuccessResult<AttendanceApprovalsFinishResponseResult>
{
}

/// <summary>
/// 考勤批准完成响应结果
/// </summary>
public class AttendanceApprovalsFinishResponseResult
{
    /// <summary>
    /// 总时长，该字段的单位与本企业内对应审批单设置的单位一致
    /// </summary>
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public double Duration;

    /// <summary>
    /// 详细信息
    /// </summary>
    [JsonProperty("durationDetail")]
    [JsonPropertyName("durationDetail")]
    public List<DurationDetailDomain> DurationDetail;
}

public class DurationDetailDomain
{
    /// <summary>
    /// 审批通过日期
    /// </summary>
    [JsonProperty("date")]
    [JsonPropertyName("date")]
    public string Date;

    /// <summary>
    /// 每日时长
    /// </summary>
    [JsonProperty("duration")]
    [JsonPropertyName("duration")]
    public double Duration;
}