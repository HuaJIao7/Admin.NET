// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 用户证件表
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "用户证件表")]
public class UserCard : EntityBaseData
{
    /// <summary>
    /// 用户id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "用户id")]
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "用户姓名", Length = 32)]
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    [SugarColumn(ColumnName = "ImgFront", ColumnDescription = "图片正面", Length = 200)]
    public virtual string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    [SugarColumn(ColumnName = "ImgBack", ColumnDescription = "图片背面", Length = 200)]
    public virtual string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "Sex", ColumnDescription = "性别", Length = 32)]
    public virtual string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    [SugarColumn(ColumnName = "IDNumber", ColumnDescription = "证件号", Length = 50)]
    public virtual string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "BirthDate", ColumnDescription = "出生日期")]
    public virtual DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    [SugarColumn(ColumnName = "Level", ColumnDescription = "级别", Length = 32)]
    public virtual string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    [SugarColumn(ColumnName = "Major", ColumnDescription = "专业", Length = 32)]
    public virtual string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>
    [SugarColumn(ColumnName = "ApprovalDate", ColumnDescription = "批准日期")]
    public virtual DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    [SugarColumn(ColumnName = "ManagementNumber", ColumnDescription = "管理号", Length = 50)]
    public virtual string? ManagementNumber { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    [SugarColumn(ColumnName = "Type", ColumnDescription = "证件类型", Length = 50)]
    public virtual string? Type { get; set; }
}
