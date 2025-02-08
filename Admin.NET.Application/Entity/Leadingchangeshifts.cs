// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 带班计划交接班
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "带班计划交接班")]
public class Leadingchangeshifts : EntityBaseData
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    [SugarColumn(ColumnName = "PlanId", ColumnDescription = "带班计划id")]
    public virtual long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 10)]
    public virtual string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "交班人id")]
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "交班人姓名", Length = 32)]
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>
    [SugarColumn(ColumnName = "Deptid", ColumnDescription = "交班人部门id")]
    public virtual long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "DeptName", ColumnDescription = "交班人部门名称", Length = 32)]
    public virtual string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>
    [SugarColumn(ColumnName = "TakeUserId", ColumnDescription = "接班人id")]
    public virtual long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>
    [SugarColumn(ColumnName = "TakeUserName", ColumnDescription = "接班人姓名", Length = 32)]
    public virtual string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>
    [SugarColumn(ColumnName = "TakeDeptid", ColumnDescription = "接班人部门id")]
    public virtual long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "TakeDeptName", ColumnDescription = "接班人部门名称", Length = 32)]
    public virtual string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>
    [SugarColumn(ColumnName = "Content", ColumnDescription = "交接班内容", Length = 500)]
    public virtual string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>
    [SugarColumn(ColumnName = "ImgUrl", ColumnDescription = "交接班图片", Length = 500)]
    public virtual string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>
    [SugarColumn(ColumnName = "VideoUrl", ColumnDescription = "交接班视频", Length = 500)]
    public virtual string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    [SugarColumn(ColumnName = "Classes", ColumnDescription = "班次", Length = 32)]
    public virtual string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间
    /// </summary>
    [SugarColumn(ColumnName = "Time", ColumnDescription = "交接班时间")]
    public virtual DateTime? Time { get; set; }
    
}
