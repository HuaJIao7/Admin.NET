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

namespace Admin.NET.Application.Entity;
/// <summary>
/// 用户信息表
/// </summary>
[Tenant("1300000000001")]
[SugarTable(null, "用户信息表")]
public class UserInformation : EntityBaseData
{
    /// <summary>
    /// 人员卡编码
    /// </summary>
    public virtual string? PersonnelCardCode { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// 工种
    /// </summary>
    public virtual string? JobTypes { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    public virtual string? Post { get; set; }

    /// <summary>
    /// 部门Id
    /// </summary>
    public virtual long? DepartmentId { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public virtual DateTime? BirthDate { get; set; }

    /// <summary>
    /// 学历
    /// </summary>
    public virtual string? Education { get; set; }

    /// <summary>
    /// 是否矿领导
    /// </summary>
    public virtual bool? IsItLeader { get; set; }

    /// <summary>
    /// 是否特种人员
    /// </summary>
    public virtual bool? IsItSpecialPersonnel { get; set; }
}