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

namespace Admin.NET.Application.Service.Real_timeData.Dto;
/// <summary>
/// 实时数据输入参数
/// </summary>
public class Real_timeDataInput : BasePageInput
{
    /// <summary>
    /// 人员卡编码
    /// </summary>
    public   string? PersonnelCardCode { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public   string? Name { get; set; }

    /// <summary>
    /// 出入井标志位
    /// </summary>
    public   string? EntranceExitSigns { get; set; }

    /// <summary>
    /// 入井时间
    /// </summary>
    public   DateTime? TimeEnteringWell { get; set; }

    /// <summary>
    /// 出井时间
    /// </summary>
    public   DateTime? ExitTime { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    public   string? RegionalCode { get; set; }

    /// <summary>
    /// 进入当前区域时刻
    /// </summary>
    public   string? TimeEnteringCurrentArea { get; set; }

    /// <summary>
    /// 基站编码
    /// </summary>
    public   string? BaseStationCode { get; set; }

    /// <summary>
    /// 进入当前所处基站时刻
    /// </summary>
    public   string? EnterCurrentBaseStationTime { get; set; }

    /// <summary>
    /// 劳动组织方式
    /// </summary>
    public   string? LaborOrganizationMode { get; set; }

    /// <summary>
    /// 距离基站距离
    /// </summary>
    public   string? DistanceFromBaseStation { get; set; }

    /// <summary>
    /// 人员工作状态
    /// </summary>
    public   int? PersonnelWorkStatus { get; set; }

    /// <summary>
    /// 是否矿领导
    /// </summary>
    public   bool? IsItLeader { get; set; }

    /// <summary>
    /// 是否特种人员
    /// </summary>
    public   bool? IsItSpecialPersonnel { get; set; }

    /// <summary>
    /// 行进轨迹基站, 时间集合
    /// </summary>
    public List<StringBuilder>? BaseStationInformations { get; set; }
}

/// <summary>
/// 删除实时数据参数
/// </summary>
public class DeleteReal_timeDataInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }
}

/// <summary>
/// 更新实时数据参数
/// </summary>
public class UpdateReal_timeDataInput : Real_timeDataInput
{

}
