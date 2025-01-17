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
/// 问题中心
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "问题中心")]
public class Problemcentered : EntityBaseData
{
    /// <summary>
    /// 计划id
    /// </summary>
    [SugarColumn(ColumnName = "PlanId", ColumnDescription = "计划id")]
    public virtual long? PlanId { get; set; }

    /// <summary>
    /// 计划名称
    /// </summary>
    [SugarColumn(ColumnName = "PlanName", ColumnDescription = "计划名称", Length = 32)]
    public virtual string? PlanName { get; set; }

    /// <summary>
    /// 点位id
    /// </summary>
    [SugarColumn(ColumnName = "PlaceId", ColumnDescription = "点位id")]
    public virtual long? PlaceId { get; set; }

    /// <summary>
    /// 点位名称
    /// </summary>
    [SugarColumn(ColumnName = "PlaceName", ColumnDescription = "点位名称", Length = 32)]
    public virtual string? PlaceName { get; set; }

    /// <summary>
    /// 上报人id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "上报人id")]
    public virtual long? UserId { get; set; }

    /// <summary>
    /// 上报人名称
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "上报人名称", Length = 32)]
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 上报内容
    /// </summary>
    [SugarColumn(ColumnName = "ReportContent", ColumnDescription = "上报内容", Length = 500)]
    public virtual string? ReportContent { get; set; }

    /// <summary>
    /// 问题来源
    /// </summary>
    [SugarColumn(ColumnName = "Source", ColumnDescription = "问题来源", Length = 32)]
    public virtual string? Source { get; set; }

    /// <summary>
    /// 上报人部门Id
    /// </summary>
    [SugarColumn(ColumnName = "UserDeptId", ColumnDescription = "上报人部门Id")]
    public virtual long? UserDeptId { get; set; }

    /// <summary>
    /// 上报人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "UserDeptName", ColumnDescription = "上报人部门名称", Length = 32)]
    public virtual string? UserDeptName { get; set; }

    /// <summary>
    /// 问题图片
    /// </summary>
    [SugarColumn(ColumnName = "ReportImg", ColumnDescription = "问题图片", Length = 500)]
    public virtual string? ReportImg { get; set; }

    /// <summary>
    /// 问题视频
    /// </summary>
    [SugarColumn(ColumnName = "ReportVideo", ColumnDescription = "问题视频", Length = 500)]
    public virtual string? ReportVideo { get; set; }

    /// <summary>
    /// 问题音频
    /// </summary>
    [SugarColumn(ColumnName = "ReportMp3", ColumnDescription = "问题音频", Length = 500)]
    public virtual string? ReportMp3 { get; set; }

    /// <summary>
    /// 上报时间
    /// </summary>
    [SugarColumn(ColumnName = "ReportTime", ColumnDescription = "上报时间")]
    public virtual DateTime? ReportTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 32)]
    public virtual string? Status { get; set; }

    /// <summary>
    /// 处理人id
    /// </summary>
    [SugarColumn(ColumnName = "HandleUserId", ColumnDescription = "处理人id")]
    public virtual long? HandleUserId { get; set; }

    /// <summary>
    /// 处理人名称
    /// </summary>
    [SugarColumn(ColumnName = "HandleUserName", ColumnDescription = "处理人名称", Length = 32)]
    public virtual string? HandleUserName { get; set; }

    /// <summary>
    /// 处理人部门id
    /// </summary>
    [SugarColumn(ColumnName = "HandleDeptId", ColumnDescription = "处理人部门id")]
    public virtual long? HandleDeptId { get; set; }

    /// <summary>
    /// 处理人部门
    /// </summary>
    [SugarColumn(ColumnName = "HandleDeptName", ColumnDescription = "处理人部门", Length = 32)]
    public virtual string? HandleDeptName { get; set; }

    /// <summary>
    /// 处理结果
    /// </summary>
    [SugarColumn(ColumnName = "HandleContent", ColumnDescription = "处理结果", Length = 500)]
    public virtual string? HandleContent { get; set; }

    /// <summary>
    /// 处理图片
    /// </summary>
    [SugarColumn(ColumnName = "HandleImg", ColumnDescription = "处理图片", Length = 500)]
    public virtual string? HandleImg { get; set; }

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
    /// 处理时间
    /// </summary>
    [SugarColumn(ColumnName = "HandleTime", ColumnDescription = "处理时间")]
    public virtual DateTime? HandleTime { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    [SugarColumn(ColumnName = "GiveUpCount", ColumnDescription = "点赞数量")]
    public virtual int? GiveUpCount { get; set; }
    /// <summary>
    /// 问题建议集合
    /// </summary>
    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<ProblemSuggestions> Suggestions { get; set; }

    /// <summary>
    /// 问题评论集合
    /// </summary>
    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<ProblemComment> Comments { get; set; }
}