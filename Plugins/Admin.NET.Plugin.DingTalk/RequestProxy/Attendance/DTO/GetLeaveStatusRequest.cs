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
public class GetLeaveStatusRequest:DingtalkRequestOffsetSize
{
    /// <summary>
    /// 待查询用户的ID列表，每次最多100个
    /// </summary>
    [JsonProperty("userid_list")]
    [JsonPropertyName("userid_list")]
    public string UserIdList { get; set; }

    /// <summary>
    /// 开始时间 ，Unix时间戳，支持最多180天的查询
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }

    /// <summary>
    /// 结束时间，Unix时间戳，支持最多180天的查询
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; set; }
}