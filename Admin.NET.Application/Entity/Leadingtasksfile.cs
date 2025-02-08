// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 带班任务上报文件
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "带班任务上报文件")]
public class Leadingtasksfile : EntityBaseData
{
    /// <summary>
    /// 任务id
    /// </summary>
    [SugarColumn(ColumnName = "TaskId", ColumnDescription = "任务id")]
    public virtual long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>
    [SugarColumn(ColumnName = "Type", ColumnDescription = "文件类型", Length = 32)]
    public virtual string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>
    [SugarColumn(ColumnName = "Url", ColumnDescription = "文件路径", Length = 500)]
    public virtual string? Url { get; set; }
    
}
