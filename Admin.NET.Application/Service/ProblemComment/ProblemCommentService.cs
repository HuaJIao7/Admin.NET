// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.ProblemComment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.ProblemComment;
/// <summary>
/// 问题评论服务 🧩
/// </summary>
[AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class ProblemCommentService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.ProblemComment> _problemCommentRepository;
    private readonly SqlSugarRepository<ProblemCommentDto> _problemCommentDto;
    private readonly SqlSugarRepository<SysUser> _userRep;
    private readonly SqlSugarRepository<SysOrg> _orgRep;
    private readonly SqlSugarRepository<Entity.Problemcentered> _problemcenteredRep;
    private readonly SqlSugarRepository<ProblemCommentBaseInput> _problemCommentBaseInput;

    public ProblemCommentService(
        SqlSugarRepository<Entity.ProblemComment> problemCommentRepository,
        SqlSugarRepository<ProblemCommentDto> problemCommentDto, SqlSugarRepository<SysOrg> orgRep, SqlSugarRepository<SysUser> userRep, SqlSugarRepository<Entity.Problemcentered> problemcenteredRep)
    {
        _problemCommentRepository = problemCommentRepository;
        _problemCommentDto = problemCommentDto;
        _orgRep = orgRep;
        _userRep = userRep;
        _problemcenteredRep = problemcenteredRep;
    }

    /// <summary>
    /// 添加评论
    /// </summary>
    /// <param name="input"></param>
    [AllowAnonymous]
    [DisplayName("添加评论")]
    [ApiDescriptionSettings(Name = "AddProblemComment"), HttpPost]
    public async Task Add(UpdateProblemCommentInput input)
    {
        if (input.DeptId == null) return;
        try
        {
            var entity = input.Adapt<Entity.ProblemComment>();
            var User = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.UserId).FirstAsync() ?? throw Oops.Oh(ErrorCodeEnum.YZ0001);
            var UserDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == User.OrgId).FirstAsync() ?? throw Oops.Oh(ErrorCodeEnum.YZ0004);
            var ProblemId = await _problemcenteredRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.ProblemId).FirstAsync() ?? throw Oops.Oh(ErrorCodeEnum.YZ0004);
            //用户id
            entity.UserId = User.Id;
            //用户名
            entity.UserName = User.RealName;
            //部门id
            entity.DeptId = UserDept.Id;
            //部门名称
            entity.DeptName = UserDept.Name;
            //评论内容
            entity.Content = input.Content;
            //评论时间
            entity.CommentTime = DateTime.Now;
            //问题id
            entity.ProblemId = ProblemId.Id;
            await _problemCommentRepository.InsertAsync(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    /// <summary>
    /// 查询全部评论信息
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("查询全部评论信息")]
    [ApiDescriptionSettings(Name = "GetProblemComment"), HttpGet]
    public async Task<List<Entity.ProblemComment>> GetProblemComment()
    {
        var entity = await _problemCommentRepository.AsQueryable().ClearFilter().ToListAsync();
        return entity;
    }

    /// <summary>
    /// 根据用户名查询评论信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("根据用户名查询评论信息")]
    [ApiDescriptionSettings(Name = "GetConditionProblemComment"), HttpGet]
    public async Task<List<ProblemCommentDto>> GetConditionProblemComment(long id)
    {
        var entity = await _problemCommentRepository.AsQueryable()
            .Where(u => u.ProblemId == id)
            .ToListAsync();
        return entity.Adapt<List<ProblemCommentDto>>();
    }

    /// <summary>
    /// 修改评论信息
    /// </summary>
    /// <param name="input"></param>
    [AllowAnonymous]
    [DisplayName("修改评论信息")]
    [ApiDescriptionSettings(Name = "GetProblemCommentById"), HttpPost]
    public async Task UpdateProblemComment(Entity.ProblemComment input)
    {
        var entity = input.Adapt<Entity.ProblemComment>();
        await _problemCommentRepository.AsUpdateable(entity).ExecuteCommandAsync();
        //await _problemCommentRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 删除评论信息
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="AppFriendlyException"></exception>
    [AllowAnonymous]
    [DisplayName("删除评论信息")]
    [ApiDescriptionSettings(Name = "DeleteProblemComment"), HttpPost]
    public async Task DeleteProblemComment(DeleteProblemCommentInput input)
    {
        var entity = await _problemCommentRepository.GetFirstAsync(x => x.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _problemCommentRepository.FakeDeleteAsync(entity); //假删除
        //await _problemCommentRepository.DeleteAsync(entity); // 假删除
    }

    /// <summary>
    /// 批量删除评论信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("批量删除评论信息")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DeleteProblemCommentInput> input)
    {
        var entity = Expressionable.Create<Entity.ProblemComment>();
        foreach (var x in input)
        {
            entity = entity.Or(it => it.Id == x.Id);
        }
        var list = await _problemCommentRepository.AsQueryable().Where(entity.ToExpression()).ToListAsync();
        return await _problemCommentRepository.FakeDeleteAsync(list);
    }
}
