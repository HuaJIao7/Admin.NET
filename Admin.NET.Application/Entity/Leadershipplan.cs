// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 带班计划
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "带班计划")]
public class Leadershipplan : EntityBaseData
{

    /// <summary>
    /// 计划名称
    /// </summary>
    [SugarColumn(ColumnName = "ShiftName", ColumnDescription = "计划名称", Length = 32)]
    public virtual string? ShiftName { get; set; }


    /// <summary>
    /// 带班时间
    /// </summary>
    [SugarColumn(ColumnName = "ShiftTime", ColumnDescription = "带班时间")]
    public virtual DateTime? ShiftTime { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    [SugarColumn(ColumnName = "Shift", ColumnDescription = "班次", Length = 32)]
    public virtual string? Shift { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 32)]
    public virtual string? Status { get; set; }

}
