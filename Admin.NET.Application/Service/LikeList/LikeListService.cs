// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.LikeList.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.LikeList;
/// <summary>
/// 点赞表服务 🧩
/// </summary>
[AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LikeListSerivce : IDynamicApiController, ITransient
{
    /// <summary>
    /// 点赞源表
    /// </summary>
    private readonly SqlSugarRepository<Entity.LikeList> _likeList;
    private readonly SqlSugarRepository<LikeListDto> _likeListDto;
    private readonly SqlSugarRepository<LikeListInput> _likeListInput;
    private readonly SqlSugarRepository<Entity.Problemcentered> _problemcentered;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<SysUser> _userRep;

    public LikeListSerivce(
        SqlSugarRepository<Entity.LikeList> likeList, SqlSugarRepository<LikeListDto> likeListDto, SqlSugarRepository<LikeListInput> likeListInput, SqlSugarRepository<Entity.Problemcentered> problemcentered, ISqlSugarClient sqlSugarClient, SqlSugarRepository<SysUser> userRep)
    {
        _likeList = likeList;
        _likeListDto = likeListDto;
        _likeListInput = likeListInput;
        _problemcentered = problemcentered;
        _sqlSugarClient = sqlSugarClient;
        _userRep = userRep;
    }

    /// <summary>
    /// 添加点赞
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="AppFriendlyException"></exception>
    [AllowAnonymous]
    [DisplayName("添加点赞")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task GiveUp(LikeListInput input)
    {
        var asd = await _userRep.AsQueryable().Where(x => x.Id == input.UserId).ClearFilter().FirstAsync();
        var dsa = await _problemcentered.AsQueryable().Where(x => x.Id == input.ProblemId).ClearFilter().FirstAsync();
        if( asd == null || dsa == null) return;

        //var user = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == input.UserId).FirstAsync();
        var entity = input.Adapt<Entity.LikeList>();
        entity.ProblemId = input.ProblemId;
        entity.UserId = input.UserId;
        await _likeList.InsertAsync(entity);

        var entitys = await _problemcentered.AsQueryable().ClearFilter().Where(x => x.Id == input.ProblemId).FirstAsync();
        entitys.GiveUpCount += 1;
        await _problemcentered.AsUpdateable(entitys).ExecuteCommandAsync();



        //if (entity == null)
        //    throw Oops.Oh(ErrorCodeEnum.D1002);

        //var count = await _likeList.AsQueryable().ClearFilter().Where(x => x.ProblemId == input.ProblemId && x.UserId == input.UserId).CountAsync();

        //var user = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == input.UserId).FirstAsync();

        //if (count == 0)
        //{
        //}
    }

    /// <summary>
    /// 取消点赞
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("取消点赞")]
    [ApiDescriptionSettings(Name = "CancelLikes"), HttpPost]
    public async Task CancelLikes(LikeListInput input)
    {
        var entity = await _likeList.AsQueryable().ClearFilter().Where(x => x.ProblemId == input.ProblemId && x.UserId == input.UserId).FirstAsync();

        var problem = await _problemcentered.AsQueryable().ClearFilter().Where(x => x.Id == entity.ProblemId).FirstAsync();
        problem.GiveUpCount -= 1;
        await _problemcentered.AsUpdateable(problem)
            .ExecuteCommandAsync();
        await _likeList.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 获取点赞列表
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取点赞列表")]
    [ApiDescriptionSettings(Name = "GetList"), HttpGet]
    public async Task<List<Entity.LikeList>> GetLikeList()
    {
        var entity = await _likeList.AsQueryable().ClearFilter().ToListAsync();
        return entity;
    }

}
