// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 带班计划交接班基础输入参数
/// </summary>
public class LeadingchangeshiftsBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 带班计划id
    /// </summary>
    public virtual long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public virtual string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>
    public virtual long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>
    public virtual string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>
    public virtual long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>
    public virtual string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>
    public virtual long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>
    public virtual string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>
    public virtual string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>
    public virtual string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>
    public virtual string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    public virtual string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间
    /// </summary>
    public virtual DateTime? Time { get; set; }
    
}

/// <summary>
/// 带班计划交接班分页查询输入参数
/// </summary>
public class PageLeadingchangeshiftsInput : BasePageInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>
    public long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>
    public long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>
    public string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>
    public long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>
    public string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>
    public string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>
    public string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    public string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间范围
    /// </summary>
     public DateTime?[] TimeRange { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 带班计划交接班增加输入参数
/// </summary>
public class AddLeadingchangeshiftsInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [MaxLength(10, ErrorMessage = "状态字符长度不能超过10")]
    public string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "交班人姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>
    public long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "交班人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>
    public long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "接班人姓名字符长度不能超过32")]
    public string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>
    public long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "接班人部门名称字符长度不能超过32")]
    public string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>
    [MaxLength(500, ErrorMessage = "交接班内容字符长度不能超过500")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>
    [MaxLength(500, ErrorMessage = "交接班图片字符长度不能超过500")]
    public string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>
    [MaxLength(500, ErrorMessage = "交接班视频字符长度不能超过500")]
    public string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    [MaxLength(32, ErrorMessage = "班次字符长度不能超过32")]
    public string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间
    /// </summary>
    public DateTime? Time { get; set; }
    
}

/// <summary>
/// 带班计划交接班删除输入参数
/// </summary>
public class DeleteLeadingchangeshiftsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 带班计划交接班更新输入参数
/// </summary>
public class UpdateLeadingchangeshiftsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 带班计划id
    /// </summary>    
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>    
    [MaxLength(10, ErrorMessage = "状态字符长度不能超过10")]
    public string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>    
    public long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "交班人姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>    
    public long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "交班人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>    
    public long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "接班人姓名字符长度不能超过32")]
    public string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>    
    public long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "接班人部门名称字符长度不能超过32")]
    public string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>    
    [MaxLength(500, ErrorMessage = "交接班内容字符长度不能超过500")]
    public string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>    
    [MaxLength(500, ErrorMessage = "交接班图片字符长度不能超过500")]
    public string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>    
    [MaxLength(500, ErrorMessage = "交接班视频字符长度不能超过500")]
    public string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>    
    [MaxLength(32, ErrorMessage = "班次字符长度不能超过32")]
    public string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间
    /// </summary>    
    public DateTime? Time { get; set; }
    
}

/// <summary>
/// 带班计划交接班主键查询输入参数
/// </summary>
public class QueryByIdLeadingchangeshiftsInput : DeleteLeadingchangeshiftsInput
{
}

/// <summary>
/// 带班计划交接班数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportLeadingchangeshiftsInput : BaseImportInput
{
    /// <summary>
    /// 带班计划id
    /// </summary>
    [ImporterHeader(Name = "带班计划id")]
    [ExporterHeader("带班计划id", Format = "", Width = 25, IsBold = true)]
    public long? PlanId { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    [ImporterHeader(Name = "状态")]
    [ExporterHeader("状态", Format = "", Width = 25, IsBold = true)]
    public string? Status { get; set; }
    
    /// <summary>
    /// 交班人id
    /// </summary>
    [ImporterHeader(Name = "交班人id")]
    [ExporterHeader("交班人id", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }
    
    /// <summary>
    /// 交班人姓名
    /// </summary>
    [ImporterHeader(Name = "交班人姓名")]
    [ExporterHeader("交班人姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 交班人部门id
    /// </summary>
    [ImporterHeader(Name = "交班人部门id")]
    [ExporterHeader("交班人部门id", Format = "", Width = 25, IsBold = true)]
    public long? Deptid { get; set; }
    
    /// <summary>
    /// 交班人部门名称
    /// </summary>
    [ImporterHeader(Name = "交班人部门名称")]
    [ExporterHeader("交班人部门名称", Format = "", Width = 25, IsBold = true)]
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 接班人id
    /// </summary>
    [ImporterHeader(Name = "接班人id")]
    [ExporterHeader("接班人id", Format = "", Width = 25, IsBold = true)]
    public long? TakeUserId { get; set; }
    
    /// <summary>
    /// 接班人姓名
    /// </summary>
    [ImporterHeader(Name = "接班人姓名")]
    [ExporterHeader("接班人姓名", Format = "", Width = 25, IsBold = true)]
    public string? TakeUserName { get; set; }
    
    /// <summary>
    /// 接班人部门id
    /// </summary>
    [ImporterHeader(Name = "接班人部门id")]
    [ExporterHeader("接班人部门id", Format = "", Width = 25, IsBold = true)]
    public long? TakeDeptid { get; set; }
    
    /// <summary>
    /// 接班人部门名称
    /// </summary>
    [ImporterHeader(Name = "接班人部门名称")]
    [ExporterHeader("接班人部门名称", Format = "", Width = 25, IsBold = true)]
    public string? TakeDeptName { get; set; }
    
    /// <summary>
    /// 交接班内容
    /// </summary>
    [ImporterHeader(Name = "交接班内容")]
    [ExporterHeader("交接班内容", Format = "", Width = 25, IsBold = true)]
    public string? Content { get; set; }
    
    /// <summary>
    /// 交接班图片
    /// </summary>
    [ImporterHeader(Name = "交接班图片")]
    [ExporterHeader("交接班图片", Format = "", Width = 25, IsBold = true)]
    public string? ImgUrl { get; set; }
    
    /// <summary>
    /// 交接班视频
    /// </summary>
    [ImporterHeader(Name = "交接班视频")]
    [ExporterHeader("交接班视频", Format = "", Width = 25, IsBold = true)]
    public string? VideoUrl { get; set; }
    
    /// <summary>
    /// 班次
    /// </summary>
    [ImporterHeader(Name = "班次")]
    [ExporterHeader("班次", Format = "", Width = 25, IsBold = true)]
    public string? Classes { get; set; }
    
    /// <summary>
    /// 交接班时间
    /// </summary>
    [ImporterHeader(Name = "交接班时间")]
    [ExporterHeader("交接班时间", Format = "", Width = 25, IsBold = true)]
    public DateTime? Time { get; set; }
    
}



/// <summary>
/// 带班交接班记录分页查询输入参数
/// </summary>
public class PageLeadingchangeshiftsNewInput : BasePageInput
{

    /// <summary>
    /// 计划名称
    /// </summary>
    public string? ShiftName { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    public string? Classes { get; set; }


    /// <summary>
    /// 接班人姓名
    /// </summary>
    public string? TakeUserName { get; set; }

    /// <summary>
    /// 交班人姓名
    /// </summary>
    public string? UserName { get; set; }


    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }
}
