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
/// 点赞
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "点赞表")]
public class LikeList : EntityBaseData
{
    /// <summary>
    /// 问题id
    /// </summary>
    [SugarColumn(ColumnName = "ProblemId", ColumnDescription = "问题id")]
    public virtual long ProblemId { get; set; }

    /// <summary>
    /// 点赞id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "点赞id")]
    public virtual long UserId { get; set; }

    /// <summary>
    /// 点赞用户名称
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "点赞用户名称")]
    public virtual string? UserName { get; set; }
}
