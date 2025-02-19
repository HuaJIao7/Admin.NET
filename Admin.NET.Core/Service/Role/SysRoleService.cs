﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using static SKIT.FlurlHttpClient.Wechat.Api.Models.ComponentTCBBatchCreateContainerServiceVersionRequest.Types;

namespace Admin.NET.Core.Service;

/// <summary>
/// 系统角色服务 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(Order = 480)]
public class SysRoleService : IDynamicApiController, ITransient
{
    private readonly UserManager _userManager;
    private readonly SqlSugarRepository<SysRole> _sysRoleRep;
    private readonly SysRoleMenuService _sysRoleMenuService;
    private readonly SysUserRoleService _sysUserRoleService;
    private readonly SysRoleOrgService _sysRoleOrgService;
    private readonly SysMenuService _sysMenuService;
    private readonly SysOrgService _sysOrgService;

    public SysRoleService(UserManager userManager,
        SysOrgService sysOrgService,
        SysMenuService sysMenuService,
        SysRoleOrgService sysRoleOrgService,
        SqlSugarRepository<SysRole> sysRoleRep,
        SysRoleMenuService sysRoleMenuService,
        SysUserRoleService sysUserRoleService)
    {
        _userManager = userManager;
        _sysRoleRep = sysRoleRep;
        _sysOrgService = sysOrgService;
        _sysMenuService = sysMenuService;
        _sysRoleOrgService = sysRoleOrgService;
        _sysRoleMenuService = sysRoleMenuService;
        _sysUserRoleService = sysUserRoleService;
    }

    /// <summary>
    /// 获取角色分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("获取角色分页列表")]
    public async Task<SqlSugarPagedList<SysRole>> Page(PageRoleInput input)
    {
        // 当前用户已拥有的角色集合
        var roleIdList = _userManager.SuperAdmin ? new List<long>() : await _sysUserRoleService.GetUserRoleIdList(_userManager.UserId);
        return await _sysRoleRep.AsQueryable()
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!_userManager.SuperAdmin, u => u.TenantId == _userManager.TenantId) // 若非超管，则只能操作本租户的角色
            .WhereIF(!_userManager.SuperAdmin && !_userManager.SysAdmin, u => u.CreateUserId == _userManager.UserId || roleIdList.Contains(u.Id)) // 若非超管且非系统管理员，则只能操作自己创建的角色|自己拥有的角色
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code))
            .OrderBy(u => new { u.OrderNo, u.Id })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    // [AllowAnonymous]
    [DisplayName("获取角色分页列表name")]
    public async Task<List<SysRole>> PageName(string name,string code)
    {
        var entity = await _sysRoleRep.AsQueryable()
            .WhereIF(!string.IsNullOrEmpty(name),u => u.Name.Contains(name))
            .WhereIF(!string.IsNullOrEmpty(code), u => u.Code.Contains(code))
            .ClearFilter().ToListAsync();
        return entity;
    }

    // [AllowAnonymous]
    [DisplayName("获取角色分页列表name")]
    public async Task<List<SysRole>> PageNamejsonID(string name)
    {
        var entity = await _sysRoleRep.AsQueryable()
            .WhereIF(!string.IsNullOrEmpty(name), u => u.Name == name)
            .ClearFilter().ToListAsync();
        return entity;
    }


    /// <summary>
    /// 获取角色列表 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取角色列表")]
    public async Task<List<RoleOutput>> GetList()
    {
        // 当前用户已拥有的角色集合
        var roleIdList = _userManager.SuperAdmin ? new List<long>() : await _sysUserRoleService.GetUserRoleIdList(_userManager.UserId);

        return await _sysRoleRep.AsQueryable()
            .WhereIF(!_userManager.SuperAdmin, u => u.TenantId == _userManager.TenantId) // 若非超管，则只能操作本租户的角色
            .WhereIF(!_userManager.SuperAdmin && !_userManager.SysAdmin, u => u.CreateUserId == _userManager.UserId || roleIdList.Contains(u.Id)) // 若非超管且非系统管理员，则只显示自己创建和已拥有的角色
            .Where(u => u.Status != StatusEnum.Disable) // 非禁用的
            .OrderBy(u => new { u.OrderNo, u.Id }).Select<RoleOutput>().ToListAsync();
    }

    /// <summary>
    /// 增加角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加角色")]
    public async Task AddRole(AddRoleInput input)
    {
        var Access = await _sysRoleRep.AsQueryable().AnyAsync(u => u.Name == input.Name);
        if (Access) {
            throw Oops.Oh(ErrorCodeEnum.D1006); }
        if (await _sysRoleRep.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code))
            throw Oops.Oh(ErrorCodeEnum.D1006);

        var newRole = await _sysRoleRep.AsInsertable(input.Adapt<SysRole>()).ExecuteReturnEntityAsync();
        input.Id = newRole.Id;

        await UpdateRoleMenu(input);
    }

    /// <summary>
    /// 更新角色菜单权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    private async Task UpdateRoleMenu(AddRoleInput input)
    {
        if (input.MenuIdList == null || input.MenuIdList.Count < 1) return;
        await GrantMenu(new RoleMenuInput()
        {
            Id = input.Id,
            MenuIdList = input.MenuIdList.ToList()
        });
    }

    /// <summary>
    /// 更新角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新角色")]
    public async Task UpdateRole(UpdateRoleInput input)
    {
        if (await _sysRoleRep.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code && u.Id != input.Id))
            throw Oops.Oh(ErrorCodeEnum.D1006);

        await _sysRoleRep.AsUpdateable(input.Adapt<SysRole>()).IgnoreColumns(true)
            .IgnoreColumns(u => new { u.DataScope }).ExecuteCommandAsync();

        await UpdateRoleMenu(input);
    }

    /// <summary>
    /// 删除角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除角色")]
    public async Task DeleteRole(DeleteRoleInput input)
    {
        // 若角色有用户则禁止删除
        var userIds = await _sysUserRoleService.GetUserIdList(input.Id);
        if (userIds != null && userIds.Count > 0) throw Oops.Oh(ErrorCodeEnum.D1025);

        // 若有绑定注册方案则禁止删除
        var hasUserRegWay = await _sysRoleRep.Context.Queryable<SysUserRegWay>().AnyAsync(u => u.RoleId == input.Id);
        if (hasUserRegWay) throw Oops.Oh(ErrorCodeEnum.D1033);

        var sysRole = await _sysRoleRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _sysRoleRep.DeleteAsync(sysRole);

        // 级联删除角色机构数据
        await _sysRoleOrgService.DeleteRoleOrgByRoleId(sysRole.Id);

        // 级联删除用户角色数据
        await _sysUserRoleService.DeleteUserRoleByRoleId(sysRole.Id);

        // 级联删除角色菜单数据
        await _sysRoleMenuService.DeleteRoleMenuByRoleId(sysRole.Id);
    }

    /// <summary>
    /// 授权角色菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [UnitOfWork]
    [DisplayName("授权角色菜单")]
    public async Task GrantMenu(RoleMenuInput input)
    {
        await _sysRoleMenuService.GrantRoleMenu(input);
    }

    /// <summary>
    /// 授权角色数据范围 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [UnitOfWork]
    [DisplayName("授权角色数据范围")]
    public async Task GrantDataScope(RoleOrgInput input)
    {
        // 删除与该角色相关的用户机构缓存
        var userIdList = await _sysUserRoleService.GetUserIdList(input.Id);
        foreach (var userId in userIdList)
        {
            SqlSugarFilter.DeleteUserOrgCache(userId, _sysRoleRep.Context.CurrentConnectionConfig.ConfigId.ToString());
        }

        var role = await _sysRoleRep.GetFirstAsync(u => u.Id == input.Id);
        var dataScope = input.DataScope;
        if (!_userManager.SuperAdmin)
        {
            switch (dataScope)
            {
                // 非超级管理员没有全部数据范围权限
                case (int)DataScopeEnum.All: throw Oops.Oh(ErrorCodeEnum.D1016);
                // 若数据范围自定义，则判断授权数据范围是否有权限
                case (int)DataScopeEnum.Define:
                    {
                        var grantOrgIdList = input.OrgIdList;
                        if (grantOrgIdList.Count > 0)
                        {
                            var orgIdList = await _sysOrgService.GetUserOrgIdList();
                            if (orgIdList.Count < 1)
                                throw Oops.Oh(ErrorCodeEnum.D1016);
                            if (!grantOrgIdList.All(u => orgIdList.Any(c => c == u)))
                                throw Oops.Oh(ErrorCodeEnum.D1016);
                        }

                        break;
                    }
            }
        }
        role.DataScope = (DataScopeEnum)dataScope;
        await _sysRoleRep.AsUpdateable(role).UpdateColumns(u => new { u.DataScope }).ExecuteCommandAsync();
        await _sysRoleOrgService.GrantRoleOrg(input);
    }

    /// <summary>
    /// 根据角色Id获取菜单Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("根据角色Id获取菜单Id集合")]
    public async Task<List<long>> GetOwnMenuList([FromQuery] RoleInput input)
    {
        var menuIds = await _sysRoleMenuService.GetRoleMenuIdList(new List<long> { input.Id });
        return await _sysMenuService.ExcludeParentMenuOfFullySelected(menuIds);
    }

    /// <summary>
    /// 根据角色Id获取机构Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("根据角色Id获取机构Id集合")]
    public async Task<List<long>> GetOwnOrgList([FromQuery] RoleInput input)
    {
        return await _sysRoleOrgService.GetRoleOrgIdList(new List<long> { input.Id });
    }

    /// <summary>
    /// 设置角色状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("设置角色状态")]
    public async Task<int> SetStatus(RoleInput input)
    {
        if (!Enum.IsDefined(typeof(StatusEnum), input.Status)) throw Oops.Oh(ErrorCodeEnum.D3005);

        return await _sysRoleRep.AsUpdateable()
            .SetColumns(u => u.Status == input.Status)
            .Where(u => u.Id == input.Id)
            .ExecuteCommandAsync();
    }
}