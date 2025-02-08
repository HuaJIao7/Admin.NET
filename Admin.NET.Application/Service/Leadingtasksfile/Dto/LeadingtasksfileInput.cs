// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 带班任务上报文件基础输入参数
/// </summary>
public class LeadingtasksfileBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 任务id
    /// </summary>
    public virtual long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>
    public virtual string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>
    public virtual string? Url { get; set; }
    
}

/// <summary>
/// 带班任务上报文件分页查询输入参数
/// </summary>
public class PageLeadingtasksfileInput : BasePageInput
{
    /// <summary>
    /// 任务id
    /// </summary>
    public long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>
    public string? Url { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 带班任务上报文件增加输入参数
/// </summary>
public class AddLeadingtasksfileInput
{
    /// <summary>
    /// 任务id
    /// </summary>
    public long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>
    [MaxLength(32, ErrorMessage = "文件类型字符长度不能超过32")]
    public string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>
    [MaxLength(500, ErrorMessage = "文件路径字符长度不能超过500")]
    public string? Url { get; set; }
    
}

/// <summary>
/// 带班任务上报文件删除输入参数
/// </summary>
public class DeleteLeadingtasksfileInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 带班任务上报文件更新输入参数
/// </summary>
public class UpdateLeadingtasksfileInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 任务id
    /// </summary>    
    public long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>    
    [MaxLength(32, ErrorMessage = "文件类型字符长度不能超过32")]
    public string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>    
    [MaxLength(500, ErrorMessage = "文件路径字符长度不能超过500")]
    public string? Url { get; set; }
    
}

/// <summary>
/// 带班任务上报文件主键查询输入参数
/// </summary>
public class QueryByIdLeadingtasksfileInput : DeleteLeadingtasksfileInput
{
}

/// <summary>
/// 带班任务上报文件数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportLeadingtasksfileInput : BaseImportInput
{
    /// <summary>
    /// 任务id
    /// </summary>
    [ImporterHeader(Name = "任务id")]
    [ExporterHeader("任务id", Format = "", Width = 25, IsBold = true)]
    public long? TaskId { get; set; }
    
    /// <summary>
    /// 文件类型
    /// </summary>
    [ImporterHeader(Name = "文件类型")]
    [ExporterHeader("文件类型", Format = "", Width = 25, IsBold = true)]
    public string? Type { get; set; }
    
    /// <summary>
    /// 文件路径
    /// </summary>
    [ImporterHeader(Name = "文件路径")]
    [ExporterHeader("文件路径", Format = "", Width = 25, IsBold = true)]
    public string? Url { get; set; }
    
}
