﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
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

[Tenant("1300000000001")]
[SugarTable(null, "巡检记录")]
public class InspectionRecord : EntityBaseData
{

    /// <summary>
    /// 名称
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    public virtual long? UserInformationId { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public string? Shift { get; set; }

    /// <summary>
    /// 周期
    /// </summary>
    public string? Cycle { get; set; }

    /// <summary>
    /// 路线
    /// </summary>
    public string? Route { get; set; }

    /// <summary>
    /// 处理视频
    /// </summary>
    [SugarColumn(ColumnName = "HandleVideo", ColumnDescription = "处理视频", Length = 500)]
    public virtual string? HandleVideo { get; set; }

    /// <summary>
    /// 处理音频
    /// </summary>
    [SugarColumn(ColumnName = "HandleMp3", ColumnDescription = "处理音频", Length = 500)]
    public virtual string? HandleMp3 { get; set; }

    /// <summary>
    /// 处理图片
    /// </summary>
    [SugarColumn(ColumnName = "HandleImg", ColumnDescription = "处理图片", Length = 500)]
    public virtual string? HandleImg { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string State { get; set; }

}
