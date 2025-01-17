// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Checkin.DTO;

/// <summary>
/// 获取用户签到记录
/// </summary>
public class GetUserRecordRequest
{
    /// <summary>
    /// 需要查询的用户列表，最大列表长度为10
    /// </summary>
    [JsonProperty("userid_list")]
    [JsonPropertyName("userid_list")]
    public string UserIdList { get; set; }

    /// <summary>
    /// 开始时间，Unix时间戳，单位毫秒
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long? StartTime { get; set; }

    /// <summary>
    /// 截止时间，单位毫秒。如果是取1个人的数据，时间范围最大10天。如果是取多个人的数据，时间范围最大1天。
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long? EndTime { get; set; }

    /// <summary>
    /// 分页查询的游标，最开始可以传0
    /// </summary>
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public long? Cursor { get; set; }

    /// <summary>
    /// 分页查询的每页大小，最大100
    /// </summary>
    [JsonProperty("size")]
    [JsonPropertyName("size")]
    public long? Size { get; set; }
}