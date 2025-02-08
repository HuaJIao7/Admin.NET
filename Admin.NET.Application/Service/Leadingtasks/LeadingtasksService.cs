// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using Microsoft.AspNetCore.Http;

namespace Admin.NET.Application;

/// <summary>
/// 带班任务上报服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadingtasksService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadingtasks> _leadingtasksRep;
    private readonly SqlSugarRepository<Leadingtasksfile> _leadingtasksfileRep;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<SysUser> _userRep;
    private readonly SqlSugarRepository<SysOrg> _orgRep;
    public LeadingtasksService(SqlSugarRepository<Leadingtasks> leadingtasksRep, ISqlSugarClient sqlSugarClient, SqlSugarRepository<Leadingtasksfile> leadingtasksfileRep, SqlSugarRepository<SysUser> userRep, SqlSugarRepository<SysOrg> orgRep)
    {
        _leadingtasksRep = leadingtasksRep;
        _sqlSugarClient = sqlSugarClient;
        _leadingtasksfileRep = leadingtasksfileRep;
        _userRep = userRep;
        _orgRep = orgRep;
    }

    /// <summary>
    /// 分页查询带班任务上报 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班任务上报")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingtasksOutput>> Page(PageLeadingtasksInput input)
    {
        var query = _leadingtasksRep.AsQueryable()
            .WhereIF(input.PlanId != null, u => u.PlanId == input.PlanId)
            .Select<LeadingtasksOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班任务上报详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班任务上报详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadingtasks> Detail([FromQuery] QueryByIdLeadingtasksInput input)
    {
        var entity = await _leadingtasksRep.GetFirstAsync(u => u.Id == input.Id);
        entity.files = await _leadingtasksfileRep.AsQueryable().ClearFilter().Where(x => x.TaskId == entity.Id).ToListAsync();
        return entity;
    }

    /// <summary>
    /// 增加带班任务上报 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班任务上报")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(AddLeadingtasksInput input)
    {
        var entity = input.Adapt<Leadingtasks>();
        var repUser = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.UserId).FirstAsync();
        var repUserDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == repUser.OrgId).FirstAsync();
        entity.UserId = repUser.Id;
        entity.UserName = repUser.RealName;
        entity.DeptId = repUserDept.Id;
        entity.DeptName = repUserDept.Name;
        entity.Time = DateTime.Now;
        await _leadingtasksRep.InsertAsync(entity);
        input.files.ForEach(data => {
            Leadingtasksfile model = new Leadingtasksfile();
            model.TaskId = entity.Id;
            model.Url = data.Url;
            model.Type = data.Type;
            _leadingtasksfileRep.Insert(model);
        });
    }

    /// <summary>
    /// 更新带班任务上报 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新带班任务上报")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateLeadingtasksInput input)
    {
        var entity = input.Adapt<Leadingtasks>();
        await _leadingtasksfileRep.DeleteAsync(x=>x.TaskId == entity.Id);
        var repUser = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.UserId).FirstAsync();
        var repUserDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == repUser.OrgId).FirstAsync();
        entity.UserId = repUser.Id;
        entity.UserName = repUser.RealName;
        entity.DeptId = repUserDept.Id;
        entity.DeptName = repUserDept.Name;
        entity.Time = DateTime.Now;
        input.files.ForEach(data => {
            Leadingtasksfile model = new Leadingtasksfile();
            model.TaskId = entity.Id;
            model.Url = data.Url;
            model.Type = data.Type;
            _leadingtasksfileRep.Insert(model);
        });
        await _leadingtasksRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    
}
