// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Department.DTO;

/// <summary>
/// 获取部门列表
/// </summary>
public class ListSubResponse : DingtalkResponseErrorResultRequestId<List<ListSubResponseResultDomain>>
{
}

public class ListSubResponseResultDomain
{
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonProperty("dept_id")]
    [JsonPropertyName("dept_id")]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// 父部门ID
    /// </summary>
    [JsonProperty("parent_id")]
    [JsonPropertyName("parent_id")]
    public long ParentId { get; set; }

    /// <summary>
    /// 是否同步创建一个关联此部门的企业群
    /// </summary>
    [JsonProperty("create_dept_group")]
    [JsonPropertyName("create_dept_group")]
    public bool CreateDeptGroup { get; set; }

    /// <summary>
    /// 部门群已经创建后，有新人加入部门是否会自动加入该群
    /// </summary>
    [JsonProperty("auto_add_user")]
    [JsonPropertyName("auto_add_user")]
    public bool AutoAddUser { get; set; }
}