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

namespace Admin.NET.Application.Service.UserInformation.Dto;

/// <summary>
/// 添加用户账户
/// </summary>
public class UserInformationInput : BasePageInput
{
    /// <summary>
    /// 人员卡编码
    /// </summary>
    public string? PersonnelCardCode { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 工种
    /// </summary>
    public string? JobTypes { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    public string? Post { get; set; }

    /// <summary>
    /// 部门Id
    /// </summary>
    public long? DepartmentId { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public string? Education { get; set; }

    /// <summary>
    /// 是否矿领导
    /// </summary>
    public bool? IsItLeader { get; set; }

    /// <summary>
    /// 是否特种人员
    /// </summary>
    public bool? IsItSpecialPersonnel { get; set; }
}

/// <summary>
/// 修改用户账户
/// </summary>
public class UpdateUserInformationInput : UserInformationInput
{

}

/// <summary>
/// 查询用户账户
/// </summary>
public class DeleteUserInformationInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public int Id { get; set; }
}
