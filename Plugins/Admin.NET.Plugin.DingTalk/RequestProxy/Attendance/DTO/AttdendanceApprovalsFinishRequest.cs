// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 出勤审批完成请求入参
/// </summary>
public class AttdendanceApprovalsFinishRequest
{
    /// <summary>
    /// 审批单类型名称，最大长度20个字符。 支持类型如下： 请假,出差,外出,加班
    /// </summary>
    [JsonProperty("tagName")]
    [JsonPropertyName("tagName")]
    public string TagName;

    /// <summary>
    /// 子类型名称，最大长度20个字符。审批单类型biz_type=3时，该参数必传。
    /// </summary>
    [JsonProperty("subType")]
    [JsonPropertyName("subType")]
    public string? SubType;

    /// <summary>
    /// 审批单ID，最大长度100个字符，自定义值。
    /// </summary>
    [JsonProperty("approveId")]
    [JsonPropertyName("approveId")]
    public string ApproveId;

    /// <summary>
    /// 审批单跳转地址，最大长度200个字符。
    /// </summary>
    [JsonProperty("jumpUrl")]
    [JsonPropertyName("jumpUrl")]
    public string JumpUrl;

    /// <summary>
    /// biz_type为1时必传，加班时长单位小时。
    /// </summary>
    [JsonProperty("overtimeDuration")]
    [JsonPropertyName("overtimeDuration")]
    public string? OvertimeDuration;

    /// <summary>
    /// biz_type为1时必传：1：加班转调休,2：加班转工资
    /// </summary>
    [JsonProperty("overTimeToMore")]
    [JsonPropertyName("overTimeToMore")]
    public long? OverTimeToMore;

    /// <summary>
    /// 时长相关入参。
    /// </summary>
    [JsonProperty("topCalculateApproveDurationParam")]
    [JsonPropertyName("topCalculateApproveDurationParam")]
    public TopCalculateApproveDurationParamDomain TopCalculateApproveDurationParam;
}

public class TopCalculateApproveDurationParamDomain
{
    /// <summary>
    /// 审批单类型，可取值：1：加班,2：出差、外出,3：请假
    /// </summary>
    [JsonProperty("bizType")]
    [JsonPropertyName("bizType")]
    public AttendanceBizTypeEnum BizType;

    /// <summary>
    /// 开始时间。开始时间不能早于当前时间前31天。 支持以下格式：2019-08-15,2019-08-15 AM,2019-08-15 12:43
    /// </summary>
    [JsonProperty("fromTime")]
    [JsonPropertyName("fromTime")]
    public string FromTime;

    /// <summary>
    /// 结束时间。结束时间减去开始时间的天数不能超过31天。biz_type为1时，结束时间减去开始时间的天数不能超过1天。
    /// </summary>
    [JsonProperty("toTime")]
    [JsonPropertyName("toTime")]
    public string ToTime;

    /// <summary>
    /// 时长单位，支持格式如下：day,halfDay,hour：biz_type为1时仅支持hour。
    /// </summary>
    [JsonProperty("durationUnit")]
    [JsonPropertyName("durationUnit")]
    public string DurationUnit;

    /// <summary>
    /// 计算方法：0：按自然日计算,1：按工作日计算
    /// </summary>
    [JsonProperty("calculateModel")]
    [JsonPropertyName("calculateModel")]
    public long CalculateModel;

    /// <summary>
    /// 假期规则唯一标识。选填。仅支持bizType=3 请假时传不为空，可以支持根据假期类型设置的取整规则进行时长取整。
    /// </summary>
    [JsonProperty("leaveCode")]
    [JsonPropertyName("leaveCode")]
    public string? LeaveCode;
}