// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.User.DTO;

/// <summary>
/// 获取部门用户基础信息
/// </summary>
public class UserListSimpleRequest
{
    /// <summary>
    /// 部门ID，如果是根部门，该参数传1
    /// </summary>
    [JsonProperty("dept_id")]
    [JsonPropertyName("dept_id")]
    public long? DeptId { get; set; }

    /// <summary>
    /// 分页查询的游标，最开始传0，后续传返回参数中的next_cursor值
    /// </summary>
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public long? Cursor { get; set; }

    /// <summary>
    /// 分页长度，最大值100
    /// </summary>
    [JsonProperty("size")]
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    /// 部门成员的排序规则。默认值，custom
    /// entry_asc：代表按照进入部门的时间升序。
    ///entry_desc：代表按照进入部门的时间降序。
    ///modify_asc：代表按照部门信息修改时间升序。
    ///modify_desc：代表按照部门信息修改时间降序。
    ///custom：代表用户定义(未定义时按照拼音)排序。
    /// </summary>
    [JsonProperty("order_field")]
    [JsonPropertyName("order_field")]
    public string OrderField { get; set; }

    /// <summary>
    /// 是否返回访问受限的员工
    /// </summary>
    [JsonProperty("contain_access_limit")]
    [JsonPropertyName("contain_access_limit")]
    public bool? ContainAccessLimit { get; set; }

    /// <summary>
    /// 通讯录语言，取值。 zh_CN：中文（默认值）。 en_US：英文。
    /// </summary>
    [JsonProperty("language")]
    [JsonPropertyName("language")]
    public string Language { get; set; }
}