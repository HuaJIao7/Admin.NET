// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 通知审批撤销
/// </summary>
public class AttendanceApprovelsCancelRequest
{
    /// <summary>
    /// 用户id
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string? UserId { get; set; }

    /// <summary>
    /// 审批ID。 企业内部应用，来自通知审批通过接口自定义的参数approve_id。 第三方企业应用，来自通知审批通过接口自定义的参数approve_id。
    /// </summary>
    [JsonProperty("approve_id")]
    [JsonPropertyName("approve_id")]
    public string ApproveId { get; set; }
}