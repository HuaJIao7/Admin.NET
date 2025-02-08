// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 带班计划人员基础输入参数
/// </summary>
public class LeadershipplanuserBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 计划id
    /// </summary>
    public virtual long? PlanId { get; set; }
    
    /// <summary>
    /// 人员类型
    /// </summary>
    public virtual string? Type { get; set; }
    
    /// <summary>
    /// 人员id
    /// </summary>
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 人员姓名
    /// </summary>
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 部门id
    /// </summary>
    public virtual long? DeptId { get; set; }
    
    /// <summary>
    /// 部门名称
    /// </summary>
    public virtual string? DeptName { get; set; }
    
}

/// <summary>
/// 带班计划人员分页查询输入参数
/// </summary>
public class PageLeadershipplanuserInput : BasePageInput
{
    /// <summary>
    /// 计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 人员类型
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// 人员id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 人员姓名
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 部门id
    /// </summary>
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 带班计划人员增加输入参数
/// </summary>
public class AddLeadershipplanuserInput
{
    /// <summary>
    /// 计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 人员类型
    /// </summary>
    [MaxLength(32, ErrorMessage = "人员类型字符长度不能超过32")]
    public string? Type { get; set; }
    
    /// <summary>
    /// 人员id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 人员姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "人员姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 部门id
    /// </summary>
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
}

/// <summary>
/// 带班计划人员删除输入参数
/// </summary>
public class DeleteLeadershipplanuserInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 带班计划人员更新输入参数
/// </summary>
public class UpdateLeadershipplanuserInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 计划id
    /// </summary>    
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 人员类型
    /// </summary>    
    [MaxLength(32, ErrorMessage = "人员类型字符长度不能超过32")]
    public string? Type { get; set; }
    
    /// <summary>
    /// 人员id
    /// </summary>    
    public long? UserId { get; set; }
    
    /// <summary>
    /// 人员姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "人员姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 部门id
    /// </summary>    
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
}

/// <summary>
/// 带班计划人员主键查询输入参数
/// </summary>
public class QueryByIdLeadershipplanuserInput : DeleteLeadershipplanuserInput
{
}

/// <summary>
/// 带班计划人员数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportLeadershipplanuserInput : BaseImportInput
{
    /// <summary>
    /// 计划id
    /// </summary>
    [ImporterHeader(Name = "计划id")]
    [ExporterHeader("计划id", Format = "", Width = 25, IsBold = true)]
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 人员类型
    /// </summary>
    [ImporterHeader(Name = "人员类型")]
    [ExporterHeader("人员类型", Format = "", Width = 25, IsBold = true)]
    public string? Type { get; set; }
    
    /// <summary>
    /// 人员id
    /// </summary>
    [ImporterHeader(Name = "人员id")]
    [ExporterHeader("人员id", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }
    
    /// <summary>
    /// 人员姓名
    /// </summary>
    [ImporterHeader(Name = "人员姓名")]
    [ExporterHeader("人员姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 部门id
    /// </summary>
    [ImporterHeader(Name = "部门id")]
    [ExporterHeader("部门id", Format = "", Width = 25, IsBold = true)]
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 部门名称
    /// </summary>
    [ImporterHeader(Name = "部门名称")]
    [ExporterHeader("部门名称", Format = "", Width = 25, IsBold = true)]
    public string? DeptName { get; set; }
    
}
