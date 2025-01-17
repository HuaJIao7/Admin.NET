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

namespace Admin.NET.Application.Service.ProblemSuggestions.Dto;
/// <summary>
/// 问题建议表基础输入参数
/// </summary>
public class ProblemSuggestionsBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public virtual long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>
    public virtual long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>
    public virtual string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>
    public virtual long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>
    public virtual string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>
    public virtual string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    public virtual string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public virtual long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>
    public virtual long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>
    public virtual string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    public virtual string? Floag { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public virtual DateTime? PublishTime { get; set; }

}

/// <summary>
/// 问题建议表分页查询输入参数
/// </summary>
public class PageProblemSuggestionsInput : BasePageInput
{
    /// <summary>
    /// 问题id
    /// </summary>
    public long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>
    public long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>
    public string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    public string? Floag { get; set; }

    /// <summary>
    /// 发表时间范围
    /// </summary>
    public DateTime?[] PublishTimeRange { get; set; }

    /// <summary>
    /// 选中主键列表
    /// </summary>
    public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 问题建议表增加输入参数
/// </summary>
public class AddProblemSuggestionsInput
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "发表人姓名字符长度不能超过32")]
    public string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>
    public long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "建议人部门名称字符长度不能超过32")]
    public string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>
    [MaxLength(500, ErrorMessage = "发表内容字符长度不能超过500")]
    public string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [MaxLength(32, ErrorMessage = "是否采纳字符长度不能超过32")]
    public string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "发表人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [MaxLength(1, ErrorMessage = "是否采纳字符长度不能超过1")]
    public string? Floag { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

}

/// <summary>
/// 问题建议表删除输入参数
/// </summary>
public class DeleteProblemSuggestionsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }

}

/// <summary>
/// 问题建议表更新输入参数
/// </summary>
public class UpdateProblemSuggestionsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>    
    public long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>    
    public long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "发表人姓名字符长度不能超过32")]
    public string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>    
    public long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "建议人部门名称字符长度不能超过32")]
    public string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>    
    [MaxLength(500, ErrorMessage = "发表内容字符长度不能超过500")]
    public string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>    
    [MaxLength(32, ErrorMessage = "是否采纳字符长度不能超过32")]
    public string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>    
    public long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>    
    public long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "发表人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>    
    [MaxLength(1, ErrorMessage = "是否采纳字符长度不能超过1")]
    public string? Floag { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>    
    public DateTime? PublishTime { get; set; }

}

/// <summary>
/// 问题建议表主键查询输入参数
/// </summary>
public class QueryByIdProblemSuggestionsInput : DeleteProblemSuggestionsInput
{
}

/// <summary>
/// 问题建议表数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportProblemSuggestionsInput : BaseImportInput
{
    /// <summary>
    /// 问题id
    /// </summary>
    [ImporterHeader(Name = "问题id")]
    [ExporterHeader("问题id", Format = "", Width = 25, IsBold = true)]
    public long? ProbleId { get; set; }

    /// <summary>
    /// 发表人
    /// </summary>
    [ImporterHeader(Name = "发表人")]
    [ExporterHeader("发表人", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }

    /// <summary>
    /// 发表人姓名
    /// </summary>
    [ImporterHeader(Name = "发表人姓名")]
    [ExporterHeader("发表人姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }

    /// <summary>
    /// 建议人部门
    /// </summary>
    [ImporterHeader(Name = "建议人部门")]
    [ExporterHeader("建议人部门", Format = "", Width = 25, IsBold = true)]
    public long? UserDeptId { get; set; }

    /// <summary>
    /// 建议人部门名称
    /// </summary>
    [ImporterHeader(Name = "建议人部门名称")]
    [ExporterHeader("建议人部门名称", Format = "", Width = 25, IsBold = true)]
    public string? UserDeptName { get; set; }

    /// <summary>
    /// 发表内容
    /// </summary>
    [ImporterHeader(Name = "发表内容")]
    [ExporterHeader("发表内容", Format = "", Width = 25, IsBold = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [ImporterHeader(Name = "是否采纳")]
    [ExporterHeader("是否采纳", Format = "", Width = 25, IsBold = true)]
    public string? Status { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    [ImporterHeader(Name = "问题id")]
    [ExporterHeader("问题id", Format = "", Width = 25, IsBold = true)]
    public long? ProblemId { get; set; }

    /// <summary>
    /// 发表人部门id
    /// </summary>
    [ImporterHeader(Name = "发表人部门id")]
    [ExporterHeader("发表人部门id", Format = "", Width = 25, IsBold = true)]
    public long? DeptId { get; set; }

    /// <summary>
    /// 发表人部门名称
    /// </summary>
    [ImporterHeader(Name = "发表人部门名称")]
    [ExporterHeader("发表人部门名称", Format = "", Width = 25, IsBold = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 是否采纳
    /// </summary>
    [ImporterHeader(Name = "是否采纳")]
    [ExporterHeader("是否采纳", Format = "", Width = 25, IsBold = true)]
    public string? Floag { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    [ImporterHeader(Name = "发表时间")]
    [ExporterHeader("发表时间", Format = "", Width = 25, IsBold = true)]
    public DateTime? PublishTime { get; set; }

}