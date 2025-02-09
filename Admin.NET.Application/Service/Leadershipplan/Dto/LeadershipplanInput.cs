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
/// 带班计划增加输入参数（三班及人员）
/// </summary>
public class AddLeadershipplanOneDayInput
{

    /// <summary>
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }

    /// <summary>
    /// 带班时间
    /// </summary>
    public DateTime? ShiftTime { get; set; }

    /// <summary>
    /// 带班领导早班
    /// </summary>
    public string? ClassLeaderMorning { get; set; }
    /// <summary>
    /// 值班领导早班
    /// </summary>
    public string? DutyLeaderMorning { get; set; }
    /// <summary>
    /// 值班人员早班
    /// </summary>
    public string? DutyMorning { get; set; }
    /// <summary>
    /// 班次早班
    /// </summary>
    public string? ShiftMorning { get; set; }
    /// <summary>
    /// 带班人员早班
    /// </summary>
    public string? ClassrMorning { get; set; }



    /// <summary>
    /// 带班领导中班
    /// </summary>
    public string? ClassLeaderNoon { get; set; }
    /// <summary>
    /// 值班领导中班
    /// </summary>
    public string? DutyLeaderNoon { get; set; }
    /// <summary>
    /// 值班人员中班
    /// </summary>
    public string? DutyNoon { get; set; }
    /// <summary>
    /// 班次中班
    /// </summary>
    public string? ShiftNoon { get; set; }
    /// <summary>
    /// 带班人员中班
    /// </summary>
    public string? ClassrNoon { get; set; }


    /// <summary>
    /// 带班领导晚班
    /// </summary>
    public string? ClassLeadertEvening { get; set; }
    /// <summary>
    /// 值班领导晚班
    /// </summary>
    public string? DutyLeaderEvening { get; set; }
    /// <summary>
    /// 值班人员晚班
    /// </summary>
    public string? DutyEvening { get; set; }
    /// <summary>
    /// 班次晚班
    /// </summary>
    public string? ShiftEvening { get; set; }
    /// <summary>
    /// 带班人员晚班
    /// </summary>
    public string? ClassrEvening { get; set; }


}


/// <summary>
/// 带班计划全天分页查询输入参数
/// </summary>
public class PageLeadershipplanOneDayInput : BasePageInput
{

    /// <summary>
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }
    /// <summary>
    /// 人员姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 带班时间范围
    /// </summary>
    public DateTime? ShiftTimeRange { get; set; }


    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Shift { get; set; }
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
/// 带班计划查询带出调休人员及修改替班参数
/// </summary>
public class PageLeadershipplannOneDayShiftInput : BasePageInput
{
    /// <summary>
    /// 带班时间
    /// </summary>
    public DateTime? ShiftTime { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public string? Shift { get; set; }



}

/// <summary>
/// 更新人员替班
/// </summary>
public class UpdateLeadershipplanUserDayInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }

    /// <summary>
    /// 调休人员
    /// </summary>
    public string? CompensatoryLeaveUser { get; set; }

    /// <summary>
    /// 替班人员
    /// </summary>
    public string? reliefUser { get; set; }

}




/// <summary>
/// 带班计划分页查询输入参数
/// </summary>
public class PageLeadershipplanInput : BasePageInput
{
    /// <summary>
    /// 带班时间
    /// </summary>
     public DateTime? ShiftTime { get; set; }
    
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
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }

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
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }

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
    /// 计划名称
    /// </summary>
    [ImporterHeader(Name = "计划名称")]
    [ExporterHeader("计划名称", Format = "", Width = 25, IsBold = true)]
    public string? ShiftName { get; set; }

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




/// <summary>
/// 带班任务上报
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "带班任务上报")]
public class LeadingtasksNew : EntityBaseData
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    [SugarColumn(ColumnName = "PlanId", ColumnDescription = "带班计划id")]
    public virtual long? PlanId { get; set; }

    /// <summary>
    /// 上报人员id
    /// </summary>
    [SugarColumn(ColumnName = "UserId", ColumnDescription = "上报人员id")]
    public virtual long? UserId { get; set; }

    /// <summary>
    /// 上报人员姓名
    /// </summary>
    [SugarColumn(ColumnName = "UserName", ColumnDescription = "上报人员姓名", Length = 32)]
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 上报人员部门id
    /// </summary>
    [SugarColumn(ColumnName = "DeptId", ColumnDescription = "上报人员部门id")]
    public virtual long? DeptId { get; set; }

    /// <summary>
    /// 上报人员部门名称
    /// </summary>
    [SugarColumn(ColumnName = "DeptName", ColumnDescription = "上报人员部门名称", Length = 32)]
    public virtual string? DeptName { get; set; }

    /// <summary>
    /// 上报地点
    /// </summary>
    [SugarColumn(ColumnName = "Location", ColumnDescription = "上报地点", Length = 100)]
    public virtual string? Location { get; set; }

    /// <summary>
    /// 上报内容
    /// </summary>
    [SugarColumn(ColumnName = "Content", ColumnDescription = "上报内容", Length = 500)]
    public virtual string? Content { get; set; }

    /// <summary>
    /// 上报时间
    /// </summary>
    [SugarColumn(ColumnName = "Time", ColumnDescription = "上报时间")]
    public virtual DateTime? Time { get; set; }

    /// <summary>
    /// 任务描述
    /// </summary>
    [SugarColumn(ColumnName = "Description", ColumnDescription = "任务描述", Length = 200)]
    public virtual string? Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Status", ColumnDescription = "状态", Length = 10)]
    public virtual string Status { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Type", ColumnDescription = "任务类型", Length = 10)]
    public virtual string Type { get; set; }
}
