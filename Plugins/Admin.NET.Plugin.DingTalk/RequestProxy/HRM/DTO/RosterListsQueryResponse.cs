// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.HRM.DTO;

/// <summary>
/// 获取员工花名册字段信息
/// </summary>
public class RosterListsQueryResponse : DingTalkResponseAll<RosterListResultDomain[]>
{
}

public class RosterListResultDomain
{
    /// <summary>
    /// 企业的corpId
    /// </summary>
    [JsonProperty("corp_id")]
    [JsonPropertyName("corp_id")]
    public string CorpId { get; set; }

    /// <summary>
    /// 员工的userId
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 返回的字段信息列表
    /// </summary>
    [JsonProperty("field_data_list")]
    [JsonPropertyName("field_data_list")]
    public List<FieldDataDomain> FieldDataList { get; set; }
}

public class FieldDataDomain
{
    /// <summary>
    /// 字段标识
    /// </summary>
    [JsonProperty("field_code")]
    [JsonPropertyName("field_code")]
    public string FieldCode { get; set; }

    /// <summary>
    /// 字段名称
    /// </summary>
    [JsonProperty("field_name")]
    [JsonPropertyName("field_name")]
    public string FieldName { get; set; }

    /// <summary>
    /// 分组标识
    /// </summary>
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; }

    /// <summary>
    /// 字段值列表
    /// </summary>
    [JsonProperty("field_value_list")]
    [JsonPropertyName("field_value_list")]
    public List<FieldValueDomain> FieldValueList { get; set; }
}

public class FieldValueDomain
{
    /// <summary>
    /// 字段取值，选项类型字段对应选项的key
    /// </summary>
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }

    /// <summary>
    /// 字段展示值，选项类型字段对应选项的value
    /// </summary>
    [JsonProperty("label")]
    [JsonPropertyName("label")]
    public string Label { get; set; }

    /// <summary>
    /// 第几条的明细标识，下标从0开始
    /// </summary>
    [JsonProperty("item_index")]
    [JsonPropertyName("item_index")]
    public int ItemIndex { get; set; }
}