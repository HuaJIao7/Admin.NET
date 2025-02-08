// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 带班任务上报
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "带班任务上报")]
public class Leadingtasks : EntityBaseData
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    [SugarColumn(ColumnName = "PlanId", ColumnDescription = "带班计划id")]
    public virtual long? PlanId { get; set; }
    
    /// <summary>
    /// 上报人员id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "上报人员id")]
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 上报人员姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "上报人员姓名", Length = 32)]
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 上报人员部门id
    /// </summary>
    [SugarColumn(ColumnName = "DeptId", ColumnDescription = "上报人员部门id")]
    public virtual long? DeptId { get; set; }
    
    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    [SugarColumn(ColumnName = "DeptName", ColumnDescription = "上报人员部门名称", Length = 32)]
    public virtual string? DeptName { get; set; }
    
    /// <summary>
    /// 上报地点
    /// </summary>
    [SugarColumn(ColumnName = "Location", ColumnDescription = "上报地点", Length = 100)]
    public virtual string? Location { get; set; }
    
    /// <summary>
    /// 上报内容
    /// </summary>
    [SugarColumn(ColumnName = "Content", ColumnDescription = "上报内容", Length = 500)]
    public virtual string? Content { get; set; }
    
    /// <summary>
    /// 上报时间
    /// </summary>
    [SugarColumn(ColumnName = "Time", ColumnDescription = "上报时间")]
    public virtual DateTime? Time { get; set; }
    
    /// <summary>
    /// 任务描述
    /// </summary>
    [SugarColumn(ColumnName = "Description", ColumnDescription = "任务描述", Length = 200)]
    public virtual string? Description { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 10)]
    public virtual string Status { get; set; }
    
    /// <summary>
    /// 任务类型
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Type", ColumnDescription = "任务类型", Length = 10)]
    public virtual string Type { get; set; }
    /// <summary>
    /// 文件
    /// </summary>
    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<Leadingtasksfile> files { get; set; }
}
