// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Application;

/// <summary>
/// 用户证件表输出参数
/// </summary>
public class UserCardDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }
    
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
    
    /// <summary>
    /// 创建者Id
    /// </summary>
    public long? CreateUserId { get; set; }
    
    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; set; }
    
    /// <summary>
    /// 修改者Id
    /// </summary>
    public long? UpdateUserId { get; set; }
    
    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; set; }
    
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public long? CreateOrgId { get; set; }
    
    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; set; }
    
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDelete { get; set; }
    
    /// <summary>
    /// 用户id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    public string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    public string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    public string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    public string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    public string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    public string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    public string? ManagementNumber { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    public virtual string? Type { get; set; }
}
