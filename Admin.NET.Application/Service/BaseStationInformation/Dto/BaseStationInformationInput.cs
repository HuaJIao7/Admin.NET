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

namespace Admin.NET.Application.Service.BaseStationInformation.Dto;

/// <summary>
/// 基站信息输入参数
/// </summary>
public class BaseStationInformationInput : Template
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    public long? Id { get; set; }

    /// <summary>
    /// 基站编码
    /// </summary>
    [MaxLength(32, ErrorMessage = "基站编码字符长度不能超过32")]
    public string? BaseStationCode { get; set; }

    /// <summary>
    /// 基站名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "基站名称字符长度不能超过32")]
    public string? BaseStationName { get; set; }

    /// <summary>
    /// X坐标
    /// </summary>
    [Range(-90, 90, ErrorMessage = "X坐标范围必须在-90到90之间")]
    public string? X_Coordinate { get; set; }

    /// <summary>
    /// Y坐标
    /// </summary>
    [Range(-180, 180, ErrorMessage = "Y坐标范围必须在-180到180之间")]
    public string? Y_Coordinate { get; set; }

    /// <summary>
    /// Z坐标
    /// </summary>
    [Range(-90, 90, ErrorMessage = "Z坐标范围必须在-90到90之间")]
    public string? Z_Coordinate { get; set; }

    /// <summary>
    /// 位置注释
    /// </summary>
    [MaxLength(256, ErrorMessage = "位置注释字符长度不能超过256")]
    public string? LocationAnnotation { get; set; }
}


public class BaseStationInformationInputlist : BasePageInput
{
    /// <summary>
    /// 主键Id
    /// </summary>    
    public long? Id { get; set; }

    /// <summary>
    /// 基站编码
    /// </summary>
    [MaxLength(32, ErrorMessage = "基站编码字符长度不能超过32")]
    public string? BaseStationCode { get; set; }

    /// <summary>
    /// 基站名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "基站名称字符长度不能超过32")]
    public string? BaseStationName { get; set; }

    /// <summary>
    /// X坐标
    /// </summary>
    [Range(-90, 90, ErrorMessage = "X坐标范围必须在-90到90之间")]
    public string? X_Coordinate { get; set; }

    /// <summary>
    /// Y坐标
    /// </summary>
    [Range(-180, 180, ErrorMessage = "Y坐标范围必须在-180到180之间")]
    public string? Y_Coordinate { get; set; }

    /// <summary>
    /// Z坐标
    /// </summary>
    [Range(-90, 90, ErrorMessage = "Z坐标范围必须在-90到90之间")]
    public string? Z_Coordinate { get; set; }

    /// <summary>
    /// 位置注释
    /// </summary>
    [MaxLength(256, ErrorMessage = "位置注释字符长度不能超过256")]
    public string? LocationAnnotation { get; set; }
}
/// <summary>
/// 删除基站信息输入参数
/// </summary>
public class DelectBaseStationInformationInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 更新基站信息输入参数
/// </summary>
public class UpdateBaseStationInformationInput : BaseStationInformationInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
}
