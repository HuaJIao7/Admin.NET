// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;
/// <summary>
/// 实时数据
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "实时数据")]
public class Real_timeData : EntityBaseData
{
    /// <summary>
    /// 人员卡编码
    /// </summary>
    [SugarColumn(ColumnName = "PersonnelCardCode", ColumnDescription = "人员卡编码")]
    public virtual string? PersonnelCardCode { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "姓名")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// 出入井标志位
    /// </summary>
    [SugarColumn(ColumnName = "EntranceExitSigns", ColumnDescription = "出入井标志位")]
    public virtual string? EntranceExitSigns { get; set; }

    /// <summary>
    /// 入井时间
    /// </summary>
    [SugarColumn(ColumnName = "TimeEnteringWell", ColumnDescription = "入井时刻")]
    public virtual DateTime? TimeEnteringWell { get; set; }

    /// <summary>
    /// 出井时间
    /// </summary>
    [SugarColumn(ColumnName = "ExitTime", ColumnDescription = "出井时刻")]
    public virtual DateTime? ExitTime { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [SugarColumn(ColumnName = "RegionalCode", ColumnDescription = "区域编码", Length = 32)]
    public virtual string? RegionalCode { get; set; }

    /// <summary>
    /// 进入当前区域时刻
    /// </summary>
    [SugarColumn(ColumnName = "TimeEnteringCurrentArea", ColumnDescription = "进入当前区域时刻")]
    public virtual string? TimeEnteringCurrentArea { get; set; }

    /// <summary>
    /// 基站编码
    /// </summary>
    [SugarColumn(ColumnName = "BaseStationCode", ColumnDescription = "基站编码", Length = 32)]
    public virtual string? BaseStationCode { get; set; }

    /// <summary>
    /// 进入当前所处基站时刻
    /// </summary>
    [SugarColumn(ColumnName = "EnterCurrentBaseStationTime", ColumnDescription = "进入当前所处基站时刻")]
    public virtual string? EnterCurrentBaseStationTime { get; set; }

    /// <summary>
    /// 劳动组织方式
    /// </summary>
    [SugarColumn(ColumnName = "LaborOrganizationMode", ColumnDescription = "劳动组织方式")]
    public virtual string? LaborOrganizationMode { get; set; }

    /// <summary>
    /// 距离基站距离
    /// </summary>
    [SugarColumn(ColumnName = "DistanceFromBaseStation", ColumnDescription = "距离基站距离")]
    public virtual string? DistanceFromBaseStation { get; set; }

    /// <summary>
    /// 人员工作状态
    /// </summary>
    [SugarColumn(ColumnName = "PersonnelWorkStatus", ColumnDescription = "人员工作状态")]
    public virtual int? PersonnelWorkStatus { get; set; }

    /// <summary>
    /// 是否矿领导
    /// </summary>
    [SugarColumn(ColumnName = "IsItLeader", ColumnDescription = "是否矿领导")]
    public virtual bool? IsItLeader { get; set; }

    /// <summary>
    /// 是否特种人员
    /// </summary>
    [SugarColumn(ColumnName = "IsItSpecialPersonnel", ColumnDescription = "是否特种人员")]
    public virtual bool? IsItSpecialPersonnel { get; set; }

    /// <summary>
    /// 行进轨迹基站, 时间集合
    /// </summary>
    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<StringBuilder>? BaseStationInformations { get; set; }
}
