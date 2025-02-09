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
/// 用户证件表服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class UserCardService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<UserCard> _userCardRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public UserCardService(SqlSugarRepository<UserCard> userCardRep, ISqlSugarClient sqlSugarClient)
    {
        _userCardRep = userCardRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询用户证件表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询用户证件表")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<UserCardOutput>> Page(PageUserCardInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _userCardRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.UserName.Contains(input.Keyword) || u.ImgFront.Contains(input.Keyword) || u.ImgBack.Contains(input.Keyword) || u.Sex.Contains(input.Keyword) || u.IDNumber.Contains(input.Keyword) || u.Level.Contains(input.Keyword) || u.Major.Contains(input.Keyword) || u.ManagementNumber.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ImgFront), u => u.ImgFront.Contains(input.ImgFront.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ImgBack), u => u.ImgBack.Contains(input.ImgBack.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Sex), u => u.Sex.Contains(input.Sex.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.IDNumber), u => u.IDNumber.Contains(input.IDNumber.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Level), u => u.Level.Contains(input.Level.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Major), u => u.Major.Contains(input.Major.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Type),u=>u.Type.Contains(input.Type.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ManagementNumber), u => u.ManagementNumber.Contains(input.ManagementNumber.Trim()))
            .WhereIF(input.UserId != null, u => u.UserId == input.UserId)
            .Select<UserCardOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取用户证件表详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取用户证件表详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<UserCard> Detail([FromQuery] QueryByIdUserCardInput input)
    {
        return await _userCardRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加用户证件表 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加用户证件表")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddUserCardInput input)
    {
        var entity = input.Adapt<UserCard>();
        return await _userCardRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新用户证件表 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新用户证件表")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateUserCardInput input)
    {
        var entity = input.Adapt<UserCard>();
        await _userCardRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除用户证件表 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除用户证件表")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteUserCardInput input)
    {
        var entity = await _userCardRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _userCardRep.FakeDeleteAsync(entity);   //假删除
        //await _userCardRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除用户证件表 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除用户证件表")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteUserCardInput> input)
    {
        var exp = Expressionable.Create<UserCard>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _userCardRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _userCardRep.FakeDeleteAsync(list);   //假删除
        //return await _userCardRep.DeleteAsync(list);   //真删除
    }
}
