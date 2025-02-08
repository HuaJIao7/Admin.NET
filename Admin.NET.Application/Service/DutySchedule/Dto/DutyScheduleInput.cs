// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 值班计划基础输入参数
/// </summary>
public class DutyScheduleBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 日期
    /// </summary>
    public virtual DateTime? DutyDate { get; set; }
    
    /// <summary>
    /// 人员Id
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
    
    /// <summary>
    /// 值班类型
    /// </summary>
    public virtual string? Type { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Flights { get; set; }
}

/// <summary>
/// 值班计划分页查询输入参数
/// </summary>
public class PageDutyScheduleInput : BasePageInput
{
    /// <summary>
    /// 日期
    /// </summary>
    public DateTime? DutyDate { get; set; }
    
    /// <summary>
    /// 人员Id
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
    /// 值班类型
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Flights { get; set; }
}

/// <summary>
/// 值班计划增加输入参数
/// </summary>
public class AddDutyScheduleInput
{
    /// <summary>
    /// 日期
    /// </summary>
   
    public DateTime? DutyDate { get; set; }
    
    /// <summary>
    /// 人员Id
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
    
    /// <summary>
    /// 值班类型
    /// </summary>
    [MaxLength(32, ErrorMessage = "值班类型字符长度不能超过32")]
    public string? Type { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Flights { get; set; }
}

/// <summary>
/// 值班计划删除输入参数
/// </summary>
public class DeleteDutyScheduleInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 值班计划更新输入参数
/// </summary>
public class UpdateDutyScheduleInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 日期
    /// </summary>    
    public DateTime? DutyDate { get; set; }
    
    /// <summary>
    /// 人员Id
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
    
    /// <summary>
    /// 值班类型
    /// </summary>    
    [MaxLength(32, ErrorMessage = "值班类型字符长度不能超过32")]
    public string? Type { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Flights { get; set; }

}

/// <summary>
/// 值班计划主键查询输入参数
/// </summary>
public class QueryByIdDutyScheduleInput : DeleteDutyScheduleInput
{
}

/// <summary>
/// 值班计划数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportDutyScheduleInput : BaseImportInput
{
    /// <summary>
    /// 日期
    /// </summary>
    [ImporterHeader(Name = "日期")]
    [ExporterHeader("日期", Format = "", Width = 25, IsBold = true)]
    public DateTime? DutyDate { get; set; }
    
    /// <summary>
    /// 人员Id
    /// </summary>
    [ImporterHeader(Name = "人员Id")]
    [ExporterHeader("人员Id", Format = "", Width = 25, IsBold = true)]
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
    
    /// <summary>
    /// 值班类型
    /// </summary>
    [ImporterHeader(Name = "值班类型")]
    [ExporterHeader("值班类型", Format = "", Width = 25, IsBold = true)]
    public string? Type { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Flights { get; set; }

}
