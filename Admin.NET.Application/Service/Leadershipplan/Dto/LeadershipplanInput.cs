// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 带班计划基础输入参数
/// </summary>
public class LeadershipplanBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }


    /// <summary>
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }

    /// <summary>
    /// 带班时间
    /// </summary>
    public virtual DateTime? ShiftTime { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Shift { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public virtual string? Status { get; set; }
    
}

/// <summary>
/// 带班计划分页查询输入参数
/// </summary>
public class PageLeadershipplanInput : BasePageInput
{
    /// <summary>
    /// 带班时间范围
    /// </summary>
     public DateTime?[] ShiftTimeRange { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    public string? Shift { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 带班计划增加输入参数
/// </summary>
public class AddLeadershipplanInput
{
    /// <summary>
    /// 带班时间
    /// </summary>
    public DateTime? ShiftTime { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    [MaxLength(32, ErrorMessage = "班次字符长度不能超过32")]
    public string? Shift { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [MaxLength(32, ErrorMessage = "状态字符长度不能超过32")]
    public string? Status { get; set; }
    
}

/// <summary>
/// 带班计划删除输入参数
/// </summary>
public class DeleteLeadershipplanInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 带班计划更新输入参数
/// </summary>
public class UpdateLeadershipplanInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 带班时间
    /// </summary>    
    public DateTime? ShiftTime { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>    
    [MaxLength(32, ErrorMessage = "班次字符长度不能超过32")]
    public string? Shift { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>    
    [MaxLength(32, ErrorMessage = "状态字符长度不能超过32")]
    public string? Status { get; set; }
    
}

/// <summary>
/// 带班计划主键查询输入参数
/// </summary>
public class QueryByIdLeadershipplanInput : DeleteLeadershipplanInput
{
}

/// <summary>
/// 带班计划数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportLeadershipplanInput : BaseImportInput
{
    /// <summary>
    /// 带班时间
    /// </summary>
    [ImporterHeader(Name = "带班时间")]
    [ExporterHeader("带班时间", Format = "", Width = 25, IsBold = true)]
    public DateTime? ShiftTime { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    [ImporterHeader(Name = "班次")]
    [ExporterHeader("班次", Format = "", Width = 25, IsBold = true)]
    public string? Shift { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [ImporterHeader(Name = "状态")]
    [ExporterHeader("状态", Format = "", Width = 25, IsBold = true)]
    public string? Status { get; set; }
    
}
