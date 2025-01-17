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

namespace Admin.NET.Application.Service.RegionalInformation.Dto;
/// <summary>
/// 添加区域信息
/// </summary>
public class RegionalInformationInput : BasePageInput
{
    /// <summary>
    /// 区域类型
    /// </summary>
    [MaxLength(20, ErrorMessage = "区域内容字符长度不能超过20")]
    public string? RegionalType { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [MaxLength(30, ErrorMessage = "区域编码字符长度不能超过30")]
    public string? RegionalCode { get; set; }

    /// <summary>
    /// 区域核定人数
    /// </summary>
    public int? AuthorizedPersonnel { get; set; }

    /// <summary>
    /// 区域名称
    /// </summary>
    [MaxLength(50, ErrorMessage = "区域内容字符长度不能超过50")]
    public string? Country { get; set; }
}

/// <summary>
/// 修改区域信息
/// </summary>
public class UpdateRegionalInformationInput : RegionalInformationInput
{

}

/// <summary>
/// 删除区域信息
/// </summary>
public class DelectRegionalInformationInput
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }
}
