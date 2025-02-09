// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 用户证件表基础输入参数
/// </summary>
public class UserCardBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public virtual long? Id { get; set; }
    
    /// <summary>
    /// 用户id
    /// </summary>
    public virtual long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    public virtual string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    public virtual string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    public virtual string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    public virtual string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    public virtual string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    public virtual DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    public virtual string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    public virtual string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>
    public virtual DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    public virtual string? ManagementNumber { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    public virtual string? Type { get; set; }

}

/// <summary>
/// 用户证件表分页查询输入参数
/// </summary>
public class PageUserCardInput : BasePageInput
{
    /// <summary>
    /// 用户id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    public string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    public string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    public string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    public string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期范围
    /// </summary>
     public DateTime?[] BirthDateRange { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    public string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    public string? Major { get; set; }
    
    /// <summary>
    /// 批准日期范围
    /// </summary>
     public DateTime?[] ApprovalDateRange { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    public string? ManagementNumber { get; set; }
    
    /// <summary>
    /// 选中主键列表
    /// </summary>
     public List<long> SelectKeyList { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    public virtual string? Type { get; set; }
}

/// <summary>
/// 用户证件表增加输入参数
/// </summary>
public class AddUserCardInput
{
    /// <summary>
    /// 用户id
    /// </summary>
    public long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    [MaxLength(32, ErrorMessage = "用户姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    [MaxLength(200, ErrorMessage = "图片正面字符长度不能超过200")]
    public string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    [MaxLength(200, ErrorMessage = "图片背面字符长度不能超过200")]
    public string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    [MaxLength(32, ErrorMessage = "性别字符长度不能超过32")]
    public string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    [MaxLength(50, ErrorMessage = "证件号字符长度不能超过50")]
    public string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    [MaxLength(32, ErrorMessage = "级别字符长度不能超过32")]
    public string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    [MaxLength(32, ErrorMessage = "专业字符长度不能超过32")]
    public string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    [MaxLength(50, ErrorMessage = "管理号字符长度不能超过50")]
    public string? ManagementNumber { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    public virtual string? Type { get; set; }

}

/// <summary>
/// 用户证件表删除输入参数
/// </summary>
public class DeleteUserCardInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
}

/// <summary>
/// 用户证件表更新输入参数
/// </summary>
public class UpdateUserCardInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    [Required(ErrorMessage = "主键Id不能为空")]
    public long? Id { get; set; }
    
    /// <summary>
    /// 用户id
    /// </summary>    
    public long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>    
    [MaxLength(32, ErrorMessage = "用户姓名字符长度不能超过32")]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>    
    [MaxLength(200, ErrorMessage = "图片正面字符长度不能超过200")]
    public string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>    
    [MaxLength(200, ErrorMessage = "图片背面字符长度不能超过200")]
    public string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>    
    [MaxLength(32, ErrorMessage = "性别字符长度不能超过32")]
    public string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>    
    [MaxLength(50, ErrorMessage = "证件号字符长度不能超过50")]
    public string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>    
    public DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>    
    [MaxLength(32, ErrorMessage = "级别字符长度不能超过32")]
    public string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>    
    [MaxLength(32, ErrorMessage = "专业字符长度不能超过32")]
    public string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>    
    public DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>    
    [MaxLength(50, ErrorMessage = "管理号字符长度不能超过50")]
    public string? ManagementNumber { get; set; }
    /// <summary>
    /// 证件类型
    /// </summary>
    public virtual string? Type { get; set; }

}

/// <summary>
/// 用户证件表主键查询输入参数
/// </summary>
public class QueryByIdUserCardInput : DeleteUserCardInput
{
}

/// <summary>
/// 用户证件表数据导入实体
/// </summary>
[ExcelImporter(SheetIndex = 1, IsOnlyErrorRows = true)]
public class ImportUserCardInput : BaseImportInput
{
    /// <summary>
    /// 用户id
    /// </summary>
    [ImporterHeader(Name = "用户id")]
    [ExporterHeader("用户id", Format = "", Width = 25, IsBold = true)]
    public long? UserId { get; set; }
    
    /// <summary>
    /// 用户姓名
    /// </summary>
    [ImporterHeader(Name = "用户姓名")]
    [ExporterHeader("用户姓名", Format = "", Width = 25, IsBold = true)]
    public string? UserName { get; set; }
    
    /// <summary>
    /// 图片正面
    /// </summary>
    [ImporterHeader(Name = "图片正面")]
    [ExporterHeader("图片正面", Format = "", Width = 25, IsBold = true)]
    public string? ImgFront { get; set; }
    
    /// <summary>
    /// 图片背面
    /// </summary>
    [ImporterHeader(Name = "图片背面")]
    [ExporterHeader("图片背面", Format = "", Width = 25, IsBold = true)]
    public string? ImgBack { get; set; }
    
    /// <summary>
    /// 性别
    /// </summary>
    [ImporterHeader(Name = "性别")]
    [ExporterHeader("性别", Format = "", Width = 25, IsBold = true)]
    public string? Sex { get; set; }
    
    /// <summary>
    /// 证件号
    /// </summary>
    [ImporterHeader(Name = "证件号")]
    [ExporterHeader("证件号", Format = "", Width = 25, IsBold = true)]
    public string? IDNumber { get; set; }
    
    /// <summary>
    /// 出生日期
    /// </summary>
    [ImporterHeader(Name = "出生日期")]
    [ExporterHeader("出生日期", Format = "", Width = 25, IsBold = true)]
    public DateTime? BirthDate { get; set; }
    
    /// <summary>
    /// 级别
    /// </summary>
    [ImporterHeader(Name = "级别")]
    [ExporterHeader("级别", Format = "", Width = 25, IsBold = true)]
    public string? Level { get; set; }
    
    /// <summary>
    /// 专业
    /// </summary>
    [ImporterHeader(Name = "专业")]
    [ExporterHeader("专业", Format = "", Width = 25, IsBold = true)]
    public string? Major { get; set; }
    
    /// <summary>
    /// 批准日期
    /// </summary>
    [ImporterHeader(Name = "批准日期")]
    [ExporterHeader("批准日期", Format = "", Width = 25, IsBold = true)]
    public DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// 管理号
    /// </summary>
    [ImporterHeader(Name = "管理号")]
    [ExporterHeader("管理号", Format = "", Width = 25, IsBold = true)]
    public string? ManagementNumber { get; set; }
    
}
