// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.User.DTO;

/// <summary>
/// 获取部门用户详情
/// </summary>
public class UserListResponse : DingtalkResponseErrorResult<DingtalkResponseHasMoreList<UserListResponseResultDomain>>
{
}

public class UserListResponseResultDomain
{
    /// <summary>
    /// 是否激活了钉钉
    /// </summary>
    [JsonProperty("active")]
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>
    /// 是否为企业的管理员
    /// </summary>
    [JsonProperty("admin")]
    [JsonPropertyName("admin")]
    public bool Admin { get; set; }

    /// <summary>
    /// 头像地址
    /// </summary>
    [JsonProperty("avatar")]
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 是否为企业的老板
    /// </summary>
    [JsonProperty("boss")]
    [JsonPropertyName("boss")]
    public bool Boss { get; set; }

    /// <summary>
    /// 所属部门id列表
    /// </summary>
    [JsonProperty("dept_id_list")]
    [JsonPropertyName("dept_id_list")]
    public List<long> DeptIdList { get; set; }

    /// <summary>
    /// 员工在部门中的排序
    /// </summary>
    [JsonProperty("dept_order")]
    [JsonPropertyName("dept_order")]
    public long DeptOrder { get; set; }

    /// <summary>
    /// 员工邮箱
    /// </summary>
    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    /// 是否企业账号
    /// </summary>
    [JsonProperty("exclusive_account")]
    [JsonPropertyName("exclusive_account")]
    public bool ExclusiveAccount { get; set; }

    /// <summary>
    /// 扩展属性
    /// </summary>
    [JsonProperty("extension")]
    [JsonPropertyName("extension")]
    public string Extension { get; set; }

    /// <summary>
    /// 是否号码隐藏
    /// </summary>
    [JsonProperty("hide_mobile")]
    [JsonPropertyName("hide_mobile")]
    public bool HideMobile { get; set; }

    /// <summary>
    /// 入职时间，Unix时间戳，单位毫秒
    /// </summary>
    [JsonProperty("hired_date")]
    [JsonPropertyName("hired_date")]
    public long HiredDate { get; set; }

    /// <summary>
    /// 员工工号
    /// </summary>
    [JsonProperty("job_number")]
    [JsonPropertyName("job_number")]
    public string JobNumber { get; set; }

    /// <summary>
    /// 是否是部门的主管
    /// </summary>
    [JsonProperty("leader")]
    [JsonPropertyName("leader")]
    public bool Leader { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [JsonProperty("mobile")]
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 用户姓名
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// 员工的企业邮箱
    /// </summary>
    [JsonProperty("org_email")]
    [JsonPropertyName("org_email")]
    public string OrgEmail { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// 国际电话区号
    /// </summary>
    [JsonProperty("state_code")]
    [JsonPropertyName("state_code")]
    public string StateCode { get; set; }

    /// <summary>
    /// 分机号
    /// </summary>
    [JsonProperty("telephone")]
    [JsonPropertyName("telephone")]
    public string Telephone { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// 用户在当前开发者企业账号范围内的唯一标识
    /// </summary>
    [JsonProperty("unionid")]
    [JsonPropertyName("unionid")]
    public string Unionid { get; set; }

    /// <summary>
    /// 用户的userId
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 办公地点
    /// </summary>
    [JsonProperty("work_place")]
    [JsonPropertyName("work_place")]
    public string WorkPlace { get; set; }
}