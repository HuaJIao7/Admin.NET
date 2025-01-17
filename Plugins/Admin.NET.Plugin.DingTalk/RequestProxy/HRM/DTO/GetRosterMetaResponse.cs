// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.HRM.DTO;

/// <summary>
/// 获取花名册元数据
/// </summary>
public class GetRosterMetaResponse : DingtalkResponseErrorResultRequestId<List<RosterMetaResult>>
{
    /// <summary>
    /// 接口调用是否成功
    /// </summary>
    [JsonProperty("success")]
    [JsonPropertyName("success")]
    public virtual bool Success { get; set; }
}

public class RosterMetaResult
{
    /// <summary>
    /// 分组是否支持明细
    /// </summary>
    [JsonProperty("detail")]
    [JsonPropertyName("detail")]
    public bool Detail { get; set; }

    /// <summary>
    /// 花名册分组内字段定义
    /// </summary>
    [JsonProperty("field_meta_info_list")]
    [JsonPropertyName("field_meta_info_list")]
    public List<RosterMetaFieldInfo> FieldMetaInfoList { get; set; }

    /// <summary>
    /// 分组标识
    /// </summary>
    [JsonProperty("group_id")]
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    [JsonProperty("group_name")]
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; }
}

public class RosterMetaFieldInfo
{
    /// <summary>
    /// 是否衍生字段，例如司龄、年龄等系统计算的字段
    /// </summary>
    [JsonProperty("derived")]
    [JsonPropertyName("derived")]
    public bool Derived { get; set; }

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
    /// 可用选项的序列化结果
    /// </summary>
    [JsonProperty("option_text")]
    [JsonPropertyName("option_text")]
    public string? OptionText { get; set; }
}

/// <summary>
/// 可用选项的反序列化类型
/// </summary>
public class OptionTextDomain
{
    [JsonProperty("label")]
    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }
}