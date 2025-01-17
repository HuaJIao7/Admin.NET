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
/// 问题建议表
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "问题建议表")]
public class ProblemSuggestions : EntityBaseData
{
    /// <summary>
    /// id
    /// </summary>
    [SugarColumn(ColumnName = "Id", ColumnDescription = "id")]
    public virtual long? Id { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    [SugarColumn(ColumnName = "ProbleId", ColumnDescription = "问题id")]
    public virtual long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "发表人")]
    public virtual long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "发表人姓名", Length = 32)]
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>
    [SugarColumn(ColumnName = "UserDeptId", ColumnDescription = "建议人部门")]
    public virtual long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "UserDeptName", ColumnDescription = "建议人部门名称", Length = 32)]
    public virtual string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>
    [SugarColumn(ColumnName = "Content", ColumnDescription = "发表内容", Length = 500)]
    public virtual string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [SugarColumn(ColumnName = "Status", ColumnDescription = "是否采纳", Length = 32)]
    public virtual string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    [SugarColumn(ColumnName = "ProblemId", ColumnDescription = "问题id")]
    public virtual long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>
    [SugarColumn(ColumnName = "DeptId", ColumnDescription = "发表人部门id")]
    public virtual long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "DeptName", ColumnDescription = "发表人部门名称", Length = 32)]
    public virtual string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [SugarColumn(ColumnName = "Floag", ColumnDescription = "是否采纳", Length = 1)]
    public virtual string? Floag { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    [SugarColumn(ColumnName = "PublishTime", ColumnDescription = "发表时间")]
    public virtual DateTime? PublishTime { get; set; }

}