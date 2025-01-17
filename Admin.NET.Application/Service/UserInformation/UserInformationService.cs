// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.UserInformation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.UserInformation;

/// <summary>
/// 用户信息服务 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class UserInformationService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.UserInformation> _userinformetion;

    public UserInformationService(SqlSugarRepository<Entity.UserInformation> userinformetion)
    {
        _userinformetion = userinformetion;
    }

    /// <summary>
    /// 查询用户信息表
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询用户信息表")]
    [ApiDescriptionSettings(Name = "GetUserInformation"), HttpPost]
    public async Task<List<Entity.UserInformation>> GetUserInformation(UserInformationInput input)
    {
        var quert = _userinformetion.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.PersonnelCardCode), u => u.PersonnelCardCode.Contains(input.PersonnelCardCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.JobTypes), u => u.JobTypes.Contains(input.JobTypes.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Post), u => u.Post.Contains(input.Post.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Education), u => u.Education.Contains(input.Education.Trim()))
            .WhereIF(input.DepartmentId != null, u => u.DepartmentId == input.DepartmentId)
            .WhereIF(input.BirthDate != null, u => u.BirthDate == input.BirthDate)
            .WhereIF(input.IsItLeader != null, u => u.IsItLeader == input.IsItLeader)
            .WhereIF(input.IsItSpecialPersonnel != null, u => u.IsItSpecialPersonnel == input.IsItSpecialPersonnel);
        return await quert.OrderBuilder(input).ToPageListAsync(input.Page, input.PageSize);



        var enttity = await _userinformetion.AsQueryable().ClearFilter().ToListAsync();
        return enttity;
    }

    /// <summary>
    /// 查询用户信息表
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询用户信息表")]
    [ApiDescriptionSettings(Name = "GetConditionUserInformation"), HttpPost]
    public async Task<SqlSugarPagedList<Entity.UserInformation>> GetConditionUserInformation(UserInformationInput input, string name ,string PersonnelCardCode ,string JobTypes)
    {
        var query = _userinformetion.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.PersonnelCardCode), u => u.PersonnelCardCode.Contains(input.PersonnelCardCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.JobTypes), u => u.JobTypes.Contains(input.JobTypes.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Post), u => u.Post.Contains(input.Post.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Education), u => u.Education.Contains(input.Education.Trim()))
            .WhereIF(input.DepartmentId != null, u => u.DepartmentId == input.DepartmentId)
            .WhereIF(input.BirthDate != null, u => u.BirthDate == input.BirthDate)
            .WhereIF(input.IsItLeader != null, u => u.IsItLeader == input.IsItLeader)
            .WhereIF(input.IsItSpecialPersonnel != null, u => u.IsItSpecialPersonnel == input.IsItSpecialPersonnel)
            .WhereIF(!name.IsNullOrEmpty(), u => u.Name == name)
            .WhereIF(!name.IsNullOrEmpty(), u => u.PersonnelCardCode == PersonnelCardCode)
            .WhereIF(!name.IsNullOrEmpty(), u => u.JobTypes == JobTypes);
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);

        //query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);

        //var enttity = await _userinformetion.AsQueryable()
        //    .ClearFilter()
        //    .WhereIF(!name.IsNullOrEmpty(), u => u.Name == name || u.PersonnelCardCode == PersonnelCardCode || u.JobTypes == JobTypes)
        //    .ToListAsync();
        //return enttity;
    }
}
