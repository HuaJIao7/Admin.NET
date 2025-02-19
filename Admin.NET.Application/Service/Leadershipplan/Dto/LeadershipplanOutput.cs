﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Application;

/// <summary>
/// 带班计划输出参数
/// </summary>
public class LeadershipplanOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }


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
    public string? Shift { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }    
    
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }    
    
    /// <summary>
    /// 创建者Id
    /// </summary>
    public long? CreateUserId { get; set; }    
    
    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; set; }    
    
    /// <summary>
    /// 修改者Id
    /// </summary>
    public long? UpdateUserId { get; set; }    
    
    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; set; }    
    
    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public long? CreateOrgId { get; set; }    
    
    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; set; }    
    
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDelete { get; set; }
 
    
}

/// <summary>
/// 带班全天计划输出参数
/// </summary>
public class LeadershipplanOneDayOutput
{

    /// <summary>
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }
    /// <summary>
    /// id（早班）
    /// </summary>
    public string? IdMorning { get; set; }
    /// <summary>
    /// 班次（早班）
    /// </summary>
    public string? ShiftMorning { get; set; }

    /// <summary>
    /// 人员（早班）
    /// </summary>
    public string? StaffMorning { get; set; }


    /// <summary>
    /// id（早班）
    /// </summary>
    public string? IdNoon { get; set; }
    /// <summary>
    /// 班次（中班）
    /// </summary>
    public string? ShiftNoon { get; set; }

    /// <summary>
    /// 人员（中班）
    /// </summary>
    public string? StaffNoon { get; set; }


    /// <summary>
    /// id（早班）
    /// </summary>
    public string? Idevening { get; set; }

    /// <summary>
    /// 班次（晚班）
    /// </summary>
    public string? Shiftevening { get; set; }

    /// <summary>
    /// 人员（晚班）
    /// </summary>
    public string? Staffevening { get; set; }


    /// <summary>
    /// 带班时间
    /// </summary>
    public string? ShiftTime { get; set; }


    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }



    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    public long? UpdateUserId { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; set; }

    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public long? CreateOrgId { get; set; }

    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDelete { get; set; }
}




/// <summary>
/// 带班计划数据导入模板实体
/// </summary>
public class ExportLeadershipplanOutput : ImportLeadershipplanInput
{
    [ImporterHeader(IsIgnore = true)]
    [ExporterHeader(IsIgnore = true)]
    public override string Error { get; set; }
}
