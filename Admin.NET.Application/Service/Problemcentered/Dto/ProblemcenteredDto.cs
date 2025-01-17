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

namespace Admin.NET.Application.Service.Problemcentered.Dto;
/// <summary>
/// 查询问题中心
/// </summary>
public class ProblemcenteredDto
{
    /// <summary>
    /// 计划id
    /// </summary>
    public   long? PlanId { get; set; }

    /// <summary>
    /// 计划名称
    /// </summary>
    public   string? PlanName { get; set; }

    /// <summary>
    /// 点位id
    /// </summary>
    public   long? PlaceId { get; set; }

    /// <summary>
    /// 点位名称
    /// </summary>
    public   string? PlaceName { get; set; }

    /// <summary>
    /// 上报人id
    /// </summary>
    public   long? UserId { get; set; }

    /// <summary>
    /// 上报人名称
    /// </summary>
    public   string? UserName { get; set; }

    /// <summary>
    /// 上报内容
    /// </summary>
    public   string? ReportContent { get; set; }

    /// <summary>
    /// 问题来源
    /// </summary>
    public   string? Source { get; set; }

    /// <summary>
    /// 上报人部门Id
    /// </summary>
    public   long? UserDeptId { get; set; }

    /// <summary>
    /// 上报人部门名称
    /// </summary>
    public   string? UserDeptName { get; set; }

    /// <summary>
    /// 问题图片
    /// </summary>
    public   string? ReportImg { get; set; }

    /// <summary>
    /// 问题视频
    /// </summary>
    public   string? ReportVideo { get; set; }

    /// <summary>
    /// 问题音频
    /// </summary>
    public   string? ReportMp3 { get; set; }

    /// <summary>
    /// 上报时间
    /// </summary>
    public   DateTime? ReportTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public   string? Status { get; set; }

    /// <summary>
    /// 处理人id
    /// </summary>
    public   long? HandleUserId { get; set; }

    /// <summary>
    /// 处理人名称
    /// </summary>
    public   string? HandleUserName { get; set; }

    /// <summary>
    /// 处理人部门id
    /// </summary>
    public   long? HandleDeptId { get; set; }

    /// <summary>
    /// 处理人部门
    /// </summary>
    public   string? HandleDeptName { get; set; }

    /// <summary>
    /// 处理结果
    /// </summary>
    public   string? HandleContent { get; set; }

    /// <summary>
    /// 处理图片
    /// </summary>
    public   string? HandleImg { get; set; }

    /// <summary>
    /// 处理视频
    /// </summary>
    public   string? HandleVideo { get; set; }

    /// <summary>
    /// 处理音频
    /// </summary>
    public   string? HandleMp3 { get; set; }

    /// <summary>
    /// 处理时间
    /// </summary>
    [SugarColumn(ColumnName = "HandleTime", ColumnDescription = "处理时间")]
    public   DateTime? HandleTime { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    [SugarColumn(ColumnName = "GiveUpCount", ColumnDescription = "点赞数量")]
    public   int? GiveUpCount { get; set; }
    /// <summary>
    /// 问题建议集合
    /// </summary>
    public List<Entity.ProblemSuggestions> Suggestions { get; set; }

    /// <summary>
    /// 问题评论集合
    /// </summary>
    public List<Entity.ProblemComment> Comments { get; set; }
}

public class DateTimes
{
    public DateTime? ReportTimeMinimum { get; set; }
    public DateTime? ReportTimeMax { get; set; }
    public DateTime? HandleTimeMinimum { get; set; }
    public DateTime? HandleTimeMax { get; set; }
}
