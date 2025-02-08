// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 带班任务上报基础输入参数
/// </summary>
public class LeadingtasksBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 带班计划id
    /// </summary>
    public virtual long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>
    public virtual long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    public virtual string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>
    public virtual string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>
    public virtual string? Content { get; set; }
    
    /// <summary>
    /// 上报时间
    /// </summary>
    public virtual DateTime? Time { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>
    public virtual string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public virtual string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>
    [Required(ErrorMessage = "任务类型不能为空")]
    public virtual string Type { get; set; }
    
}

/// <summary>
/// 带班任务上报分页查询输入参数
/// </summary>
public class PageLeadingtasksInput : BasePageInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>
    public string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// 上报时间范围
    /// </summary>
     public DateTime?[] TimeRange { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 带班任务上报增加输入参数
/// </summary>
public class AddLeadingtasksInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "上报人员姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "上报人员部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>
    [MaxLength(100, ErrorMessage = "上报地点字符长度不能超过100")]
    public string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>
    [MaxLength(500, ErrorMessage = "上报内容字符长度不能超过500")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 上报时间
    /// </summary>
    public DateTime? Time { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>
    [MaxLength(200, ErrorMessage = "任务描述字符长度不能超过200")]
    public string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [MaxLength(10, ErrorMessage = "状态字符长度不能超过10")]
    public string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>
    [Required(ErrorMessage = "任务类型不能为空")]
    [MaxLength(10, ErrorMessage = "任务类型字符长度不能超过10")]
    public string Type { get; set; }
    /// <summary>
    /// 文件
    /// </summary>
    public List<Leadingtasksfile> files { get; set; }
}

/// <summary>
/// 带班任务上报删除输入参数
/// </summary>
public class DeleteLeadingtasksInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 带班任务上报更新输入参数
/// </summary>
public class UpdateLeadingtasksInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 带班计划id
    /// </summary>    
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>    
    public long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "上报人员姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>    
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "上报人员部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>    
    [MaxLength(100, ErrorMessage = "上报地点字符长度不能超过100")]
    public string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>    
    [MaxLength(500, ErrorMessage = "上报内容字符长度不能超过500")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 上报时间
    /// </summary>    
    public DateTime? Time { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>    
    [MaxLength(200, ErrorMessage = "任务描述字符长度不能超过200")]
    public string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>    
    [Required(ErrorMessage = "状态不能为空")]
    [MaxLength(10, ErrorMessage = "状态字符长度不能超过10")]
    public string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>    
    [Required(ErrorMessage = "任务类型不能为空")]
    [MaxLength(10, ErrorMessage = "任务类型字符长度不能超过10")]
    public string Type { get; set; }
    
    public List<Leadingtasksfile> files { get; set; }
}

/// <summary>
/// 带班任务上报主键查询输入参数
/// </summary>
public class QueryByIdLeadingtasksInput : DeleteLeadingtasksInput
{
}

/// <summary>
/// 带班任务上报数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportLeadingtasksInput : BaseImportInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    [ImporterHeader(Name = "带班计划id")]
    [ExporterHeader("带班计划id", Format = "", Width = 25, IsBold = true)]
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>
    [ImporterHeader(Name = "上报人员id")]
    [ExporterHeader("上报人员id", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>
    [ImporterHeader(Name = "上报人员姓名")]
    [ExporterHeader("上报人员姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>
    [ImporterHeader(Name = "上报人员部门id")]
    [ExporterHeader("上报人员部门id", Format = "", Width = 25, IsBold = true)]
    public long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    [ImporterHeader(Name = "上报人员部门名称")]
    [ExporterHeader("上报人员部门名称", Format = "", Width = 25, IsBold = true)]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>
    [ImporterHeader(Name = "上报地点")]
    [ExporterHeader("上报地点", Format = "", Width = 25, IsBold = true)]
    public string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>
    [ImporterHeader(Name = "上报内容")]
    [ExporterHeader("上报内容", Format = "", Width = 25, IsBold = true)]
    public string? Content { get; set; }
    
    /// <summary>
    /// 上报时间
    /// </summary>
    [ImporterHeader(Name = "上报时间")]
    [ExporterHeader("上报时间", Format = "", Width = 25, IsBold = true)]
    public DateTime? Time { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>
    [ImporterHeader(Name = "任务描述")]
    [ExporterHeader("任务描述", Format = "", Width = 25, IsBold = true)]
    public string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [ImporterHeader(Name = "*状态")]
    [ExporterHeader("*状态", Format = "", Width = 25, IsBold = true)]
    public string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>
    [ImporterHeader(Name = "*任务类型")]
    [ExporterHeader("*任务类型", Format = "", Width = 25, IsBold = true)]
    public string Type { get; set; }
    
}
