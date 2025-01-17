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
/// 基站信息
/// </summary>
public class BaseStationInformationDto : Template
{
    /// <summary>
    /// 基站编码
    /// </summary>
    public   string? BaseStationCode { get; set; }

    /// <summary>
    /// 基站名称
    /// </summary>
    public   string? BaseStationName { get; set; }

    /// <summary>
    /// X坐标
    /// </summary>
    public   int? X_Coordinate { get; set; }

    /// <summary>
    /// Y坐标
    /// </summary>
    public   int? Y_Coordinate { get; set; }

    /// <summary>
    /// Z坐标
    /// </summary>
    public   int? Z_Coordinate { get; set; }

    /// <summary>
    /// 基站类型
    /// </summary>
    public   string? LocationAnnotation { get; set; }


}
