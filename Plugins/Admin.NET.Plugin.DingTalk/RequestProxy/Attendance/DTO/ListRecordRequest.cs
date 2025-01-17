// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 获取打卡详情
/// </summary>
public class ListRecordRequest
{
    /// <summary>
    /// 企业内的员工ID列表，最大值50
    /// </summary>
    [JsonProperty("userIds")]
    [JsonPropertyName("userIds")]
    public List<string> UserIds { get; set; }

    /// <summary>
    /// 查询考勤打卡记录的起始工作日。格式为：yyyy-MM-dd hh:mm:ss。例如，参数传"2021-12-01 10:00:00"，员工在09:00的打卡信息获取不到。
    /// </summary>
    [JsonProperty("checkDateFrom")]
    [JsonPropertyName("checkDateFrom")]
    public string CheckDateFrom { get; set; }

    /// <summary>
    /// 查询考勤打卡记录的结束工作日。格式为：yyyy-MM-dd hh:mm:ss。例如，参数传"2021-12-01 18:00:00"，员工在19:00的打卡信息获取不到。
    /// </summary>
    [JsonProperty("checkDateTo")]
    [JsonPropertyName("checkDateTo")]
    public string CheckDateTo { get; set; }

    /// <summary>
    /// 是否为海外企业使用： true：海外平台使用。false（默认）：国内平台使用
    /// </summary>
    [JsonProperty("isI18n")]
    [JsonPropertyName("isI18n")]
    public bool IsI18n { get; set; } = false;
}