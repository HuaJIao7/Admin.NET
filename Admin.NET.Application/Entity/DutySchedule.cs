// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 值班计划
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "值班计划")]
public class DutySchedule : EntityBaseData
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "DutyDate", ColumnDescription = "日期", Length = 32)]
    public virtual DateTime? DutyDate { get; set; }

    /// <summary>
    /// 人员Id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "人员Id")]
    public virtual long? UserId { get; set; }

    /// <summary>
    /// 人员姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "人员姓名", Length = 32)]
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 部门id
    /// </summary>
    [SugarColumn(ColumnName = "DeptId", ColumnDescription = "部门id")]
    public virtual long? DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "DeptName", ColumnDescription = "部门名称", Length = 32)]
    public virtual string? DeptName { get; set; }

    /// <summary>
    /// 值班类型
    /// </summary>
    [SugarColumn(ColumnName = "Type", ColumnDescription = "值班类型", Length = 32)]
    public virtual string? Type { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    [SugarColumn(ColumnName = "Flights", ColumnDescription = "班次", Length = 20)]
    public virtual string? Flights { get; set; }
}
