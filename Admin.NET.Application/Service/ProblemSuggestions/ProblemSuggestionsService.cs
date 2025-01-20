// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.ProblemSuggestions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.ProblemSuggestions;
/// <summary>
/// 问题建议表服务 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class ProblemSuggestionsService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.ProblemSuggestions> _problemSuggestionsRep;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<SysUser> _userRep;
    private readonly SqlSugarRepository<SysOrg> _orgRep;
    private readonly SqlSugarRepository<ProblemSuggestionsDto> _problemSuggestionsDtoRep;
    public ProblemSuggestionsService(SqlSugarRepository<Entity.ProblemSuggestions> problemSuggestionsRep, ISqlSugarClient sqlSugarClient, SqlSugarRepository<SysUser> userRep, SqlSugarRepository<SysOrg> orgRep, SqlSugarRepository<ProblemSuggestionsDto> problemSuggestionsDtoRep)
    {
        _problemSuggestionsRep = problemSuggestionsRep;
        _sqlSugarClient = sqlSugarClient;
        _userRep = userRep;
        _orgRep = orgRep;
        _problemSuggestionsDtoRep = problemSuggestionsDtoRep;
    }

    /// <summary>
    /// 查询问题建议表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询问题建议表")]
    [ApiDescriptionSettings(Name = "GetProblemSuggestions"), HttpGet]
    public async Task<List<Entity.ProblemSuggestions>> GetProblemSuggestions()
    {
        var entity = await _problemSuggestionsRep.AsQueryable().ClearFilter().ToListAsync();
        return entity;
    }

    /// <summary>
    /// 查询问题建议表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询问题建议表")]
    [ApiDescriptionSettings(Name = "GetConditionProblemSuggestions"), HttpGet]
    public async Task<List<Entity.ProblemSuggestions>> GetConditionProblemSuggestions(long id)
    {
        var entity = await _problemSuggestionsRep.AsQueryable()
            .Where(x => x.ProbleId == id)
            .ToListAsync();
        return entity.Adapt<List<Entity.ProblemSuggestions>>();
    }

    /// <summary>
    /// 获取问题建议表详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("获取问题建议表详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<List<Entity.ProblemSuggestions>> Detail([FromQuery] QueryByIdProblemSuggestionsInput input)
    {
        var entity = await _problemSuggestionsRep.AsQueryable()
            .Where(u => u.ProbleId == input.Id)
            .ClearFilter().ToListAsync();
        return entity;
    }

    /// <summary>
    /// 增加问题建议表 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("增加问题建议表")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(AddProblemSuggestionsInput input)
     {
        var e = await _problemSuggestionsRep.AsQueryable().ClearFilter().ToListAsync();
        int count = e.Count + 1;

        var entitys = input.Adapt<Entity.ProblemSuggestions>();
        //var user = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entitys.UserId).FirstAsync();
        //var userDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == user.OrgId).FirstAsync();
        //if (user == null || userDept == null) {
        //    return;
        //}
        entitys.Id = count;
        entitys.ProbleId = input.ProbleId;
        entitys.UserId = input.UserId;
        entitys.UserName = input.UserName;
        entitys.UserDeptId = input.UserDeptId;
        entitys.UserDeptName = input.UserDeptName;
        entitys.Content = input.Content;
        entitys.Status = input.Status;
        entitys.ProblemId = input.ProblemId;
        entitys.DeptId = input.DeptId;
        entitys.DeptName = input.DeptName;
        entitys.Floag = input.Floag;
        entitys.PublishTime = DateTime.Now;
        await _problemSuggestionsRep.InsertAsync(entitys);


        //var entity = input.Adapt<Entity.ProblemSuggestions>();
        //var User = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.UserId).FirstAsync();
        //var UserDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == User.OrgId).FirstAsync();

        //entity.UserName = User == null ? "" : User.RealName;
        //entity.DeptName = UserDept == null ? "" : UserDept.Name;
        //entity.Floag = "";
        //entity.PublishTime = DateTime.Now;
        //return await _problemSuggestionsRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 是否采纳建议 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("是否采纳建议")]
    [ApiDescriptionSettings(Name = "Adopt"), HttpPost]
    public async Task Adopt(UpdateProblemSuggestionsInput input)
    {
        var model = await _problemSuggestionsRep.AsQueryable().ClearFilter().Where(x => x.Id == input.Id).FirstAsync();
        if (model == null)
            throw Oops.Oh(ErrorCodeEnum.D1002);

        var entity = input.Adapt<Entity.ProblemSuggestions>();
        await _problemSuggestionsRep.AsUpdateable(entity)
            .Where(it => it.Id == entity.Id).ExecuteCommandAsync();

       // model.Status = input.Status;
       // await _problemSuggestionsRep.AsUpdateable(model)
       //.ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新问题建议表 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("更新问题建议表")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateProblemSuggestionsInput input)
    {
        var entity = input.Adapt<Entity.ProblemSuggestions>();
        await _problemSuggestionsRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除问题建议表 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("删除问题建议表")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteProblemSuggestionsInput input)
    {
        var entity = await _problemSuggestionsRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _problemSuggestionsRep.FakeDeleteAsync(entity);   //假删除
        //await _problemSuggestionsRep.DeleteAsync(entity);   //真删除
    }
}
