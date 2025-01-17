// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Department.DTO;

/// <summary>
/// 获取部门详情
/// </summary>
public class GetDeptInfoResponse : DingtalkResponseErrorResultRequestId<GetDeptInfoResponseResultDomain>
{
}

public class GetDeptInfoResponseResultDomain
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
    /// 部门标识字段
    /// </summary>
    [JsonProperty("source_identifier")]
    [JsonPropertyName("source_identifier")]
    public string SourceIdentifier { get; set; }

    /// <summary>
    /// 是否同步创建一个关联此部门的企业群
    /// </summary>
    [JsonProperty("create_dept_group")]
    [JsonPropertyName("create_dept_group")]
    public bool CreateDeptGroup { get; set; }

    /// <summary>
    /// 当部门群已经创建后，是否有新人加入部门会自动加入该群
    /// </summary>
    [JsonProperty("auto_add_user")]
    [JsonPropertyName("auto_add_user")]
    public bool AutoAddUser { get; set; }

    /// <summary>
    /// 是否默认同意加入该部门的申请
    /// </summary>
    [JsonProperty("auto_approve_apply")]
    [JsonPropertyName("auto_approve_apply")]
    public bool AutoApproveApply { get; set; }

    /// <summary>
    /// 部门是否来自关联组织
    /// </summary>
    [JsonProperty("from_union_org")]
    [JsonPropertyName("from_union_org")]
    public bool FromUnionOrg { get; set; }

    /// <summary>
    /// 教育部门标签： campus：校区，period：学段，grade：年级，class：班级
    /// </summary>
    [JsonProperty("tags")]
    [JsonPropertyName("tags")]
    public string Tags { get; set; }

    /// <summary>
    /// 在父部门中的次序值
    /// </summary>
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public long Order { get; set; }

    /// <summary>
    /// 部门群ID
    /// </summary>
    [JsonProperty("dept_group_chat_id")]
    [JsonPropertyName("dept_group_chat_id")]
    public string DeptGroupChatId { get; set; }

    /// <summary>
    /// 部门群是否包含子部门
    /// </summary>
    [JsonProperty("group_contain_sub_dept")]
    [JsonPropertyName("group_contain_sub_dept")]
    public bool GroupContainSubDept { get; set; }

    /// <summary>
    /// 企业群群主userId
    /// </summary>
    [JsonProperty("org_dept_owner")]
    [JsonPropertyName("org_dept_owner")]
    public string OrgDeptOwner { get; set; }

    /// <summary>
    /// 部门的主管userd列表
    /// </summary>
    [JsonProperty("dept_manager_userid_list")]
    [JsonPropertyName("dept_manager_userid_list")]
    public List<string> DeptManagerUserIdList { get; set; }

    /// <summary>
    /// 是否限制本部门成员查看通讯录
    /// </summary>
    [JsonProperty("outer_dept")]
    [JsonPropertyName("outer_dept")]
    public bool OuterDept { get; set; }

    /// <summary>
    /// 配置的部门员工可见部门Id列表
    /// </summary>
    [JsonProperty("outer_permit_depts")]
    [JsonPropertyName("outer_permit_depts")]
    public List<long> OuterPermitDepts { get; set; }

    /// <summary>
    /// 配置的部门员工可见员工userId列表
    /// </summary>
    [JsonProperty("outer_permit_users")]
    [JsonPropertyName("outer_permit_users")]
    public List<string> OuterPermitUsers { get; set; }

    /// <summary>
    /// 是否开启隐藏本部门
    /// </summary>
    [JsonProperty("hide_dept")]
    [JsonPropertyName("hide_dept")]
    public bool HideDept { get; set; }

    /// <summary>
    /// 隐藏部门的员工userId列表
    /// </summary>
    [JsonProperty("user_permits")]
    [JsonPropertyName("user_permits")]
    public List<string> UserPermits { get; set; }

    /// <summary>
    /// 隐藏部门的部门Id列表
    /// </summary>
    [JsonProperty("dept_permits")]
    [JsonPropertyName("dept_permits")]
    public List<long> DeptPermits { get; set; }
}