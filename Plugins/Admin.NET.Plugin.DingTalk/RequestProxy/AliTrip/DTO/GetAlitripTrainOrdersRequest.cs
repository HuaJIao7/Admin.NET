// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip.DTO;

/// <summary>
/// 获取企业火车票订单数据
/// </summary>
public class GetAliTripTrainOrdersRequest
{
    public GetAliTripTrainOrdersRequestDomain rq { get; set; }
}

public class GetAliTripTrainOrdersRequestDomain
{
    /// <summary>
    /// 开始时间 2017-05-01 00:00:00
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// 商旅申请单id
    /// </summary>
    [JsonProperty("apply_id")]
    [JsonPropertyName("apply_id")]
    public string? ApplyId { get; set; }

    /// <summary>
    /// 页数，从1开始
    /// </summary>
    [JsonProperty("page")]
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string? UserId { get; set; }

    /// <summary>
    /// 每页数据量，默认10，最高50
    /// </summary>
    [JsonProperty("page_size")]
    [JsonPropertyName("page_size")]
    public int? PageSize { get; set; }

    /// <summary>
    /// 部门id
    /// </summary>
    [JsonProperty("deptid")]
    [JsonPropertyName("deptid")]
    public string? Deptid { get; set; }

    /// <summary>
    /// 结束时间 2017-05-01 00:00:00
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    /// <summary>
    /// 企业id
    /// </summary>
    [JsonProperty("corpid")]
    [JsonPropertyName("corpid")]
    public string? Corpid { get; set; }

    /// <summary>
    /// 更新结束时间 2017-05-01 00:00:00
    /// </summary>
    [JsonProperty("update_end_time")]
    [JsonPropertyName("update_end_time")]
    public string? UpdateEndTime { get; set; }

    /// <summary>
    /// 更新开始时间 2017-05-01 00:00:00
    /// </summary>
    [JsonProperty("update_start_time")]
    [JsonPropertyName("update_start_time")]
    public string? UpdateStartTime { get; set; }

    /// <summary>
    /// false：仅搜索未报销的申请单
    /// </summary>
    [JsonProperty("all_apply")]
    [JsonPropertyName("all_apply")]
    public string? AllApply { get; set; }

    /// <summary>
    /// 第三方申请单ID
    /// </summary>
    [JsonProperty("thirdpart_apply_id")]
    [JsonPropertyName("thirdpart_apply_id")]
    public string? ThirdpartApplyId { get; set; }
}