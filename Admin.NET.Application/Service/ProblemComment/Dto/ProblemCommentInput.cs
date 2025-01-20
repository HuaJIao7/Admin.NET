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

namespace Admin.NET.Application.Service.ProblemComment.Dto;
/// <summary>
/// 问题评论基础输入参数
/// </summary>
public class ProblemCommentBaseInput:Template
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public   long? Id { get; set; }

    /// <summary>
    /// 评论人id
    /// </summary>
    public   long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    public   string? UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>
    public   long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>
    public   string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public   string? Content { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public   DateTime? CommentTime { get; set; }

}

/// <summary>
/// 问题评论分页查询输入参数
/// </summary>
public class PageProblemCommentInput : BasePageInput
{
    /// <summary>
    /// 评论人id
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 评论时间范围
    /// </summary>
    public DateTime?[] CommentTimeRange { get; set; }

    /// <summary>
    /// 选中主键列表
    /// </summary>
    public List<long> SelectKeyList { get; set; }
}

/// <summary>
/// 问题评论增加输入参数
/// </summary>
public class AddProblemCommentInput
{
    /// <summary>
    /// 评论人id
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "评论人姓名字符长度不能超过32")]
    public string UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "评论人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [MaxLength(500, ErrorMessage = "评论内容字符长度不能超过500")]
    public string? Content { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime? CommentTime { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public   long ProblemId { get; set; }

}

/// <summary>
/// 问题评论删除输入参数
/// </summary>
public class DeleteProblemCommentInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }

}

/// <summary>
/// 问题评论更新输入参数
/// </summary>
public class UpdateProblemCommentInput
{

    /// <summary>
    /// 评论人id
    /// </summary>    
    public long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "评论人姓名字符长度不能超过32")]
    public string? UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>    
    public long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>    
    [MaxLength(32, ErrorMessage = "评论人部门名称字符长度不能超过32")]
    public string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>    
    [MaxLength(500, ErrorMessage = "评论内容字符长度不能超过500")]
    public string? Content { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>    
    public DateTime? CommentTime { get; set; }

    /// <summary>
    /// 问题id
    /// </summary>
    public   long ProblemId { get; set; }

}

/// <summary>
/// 问题评论主键查询输入参数
/// </summary>
public class QueryByIdProblemCommentInput : DeleteProblemCommentInput
{
}

/// <summary>
/// 问题评论数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportProblemCommentInput : BaseImportInput
{
    /// <summary>
    /// 评论人id
    /// </summary>
    [ImporterHeader(Name = "评论人id")]
    [ExporterHeader("评论人id", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    [ImporterHeader(Name = "评论人姓名")]
    [ExporterHeader("评论人姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>
    [ImporterHeader(Name = "评论人部门id")]
    [ExporterHeader("评论人部门id", Format = "", Width = 25, IsBold = true)]
    public long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>
    [ImporterHeader(Name = "评论人部门名称")]
    [ExporterHeader("评论人部门名称", Format = "", Width = 25, IsBold = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [ImporterHeader(Name = "评论内容")]
    [ExporterHeader("评论内容", Format = "", Width = 25, IsBold = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    [ImporterHeader(Name = "评论时间")]
    [ExporterHeader("评论时间", Format = "", Width = 25, IsBold = true)]
    public DateTime? CommentTime { get; set; }

}