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
/// 带班计划人员服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadershipplanuserService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadershipplanuser> _leadershipplanuserRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public LeadershipplanuserService(SqlSugarRepository<Leadershipplanuser> leadershipplanuserRep, ISqlSugarClient sqlSugarClient)
    {
        _leadershipplanuserRep = leadershipplanuserRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询带班计划人员 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班计划人员")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadershipplanuserOutput>> Page(PageLeadershipplanuserInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadershipplanuserRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Type.Contains(input.Keyword) || u.UserName.Contains(input.Keyword) || u.DeptName.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Type), u => u.Type.Contains(input.Type.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeptName), u => u.DeptName.Contains(input.DeptName.Trim()))
            .WhereIF(input.PlanId != null, u => u.PlanId == input.PlanId)
            .WhereIF(input.UserId != null, u => u.UserId == input.UserId)
            .WhereIF(input.DeptId != null, u => u.DeptId == input.DeptId)
            .Select<LeadershipplanuserOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班计划人员详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班计划人员详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadershipplanuser> Detail([FromQuery] QueryByIdLeadershipplanuserInput input)
    {
        return await _leadershipplanuserRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加带班计划人员 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班计划人员")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddLeadershipplanuserInput input)
    {
        var entity = input.Adapt<Leadershipplanuser>();
        return await _leadershipplanuserRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新带班计划人员 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新带班计划人员")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateLeadershipplanuserInput input)
    {
        var entity = input.Adapt<Leadershipplanuser>();
        await _leadershipplanuserRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除带班计划人员 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班计划人员")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadershipplanuserInput input)
    {
        var entity = await _leadershipplanuserRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadershipplanuserRep.FakeDeleteAsync(entity);   //假删除
        //await _leadershipplanuserRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班计划人员 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班计划人员")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteLeadershipplanuserInput> input)
    {
        var exp = Expressionable.Create<Leadershipplanuser>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadershipplanuserRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadershipplanuserRep.FakeDeleteAsync(list);   //假删除
        //return await _leadershipplanuserRep.DeleteAsync(list);   //真删除
    }
    
    /// <summary>
    /// 导出带班计划人员记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出带班计划人员记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageLeadershipplanuserInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportLeadershipplanuserOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "带班计划人员导出记录");
    }
    
    /// <summary>
    /// 下载带班计划人员数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载带班计划人员数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportLeadershipplanuserOutput>(), "带班计划人员导入模板");
    }
    
    /// <summary>
    /// 导入带班计划人员记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入带班计划人员记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (this)
        {
            var stream = ExcelHelper.ImportData<ImportLeadershipplanuserInput, Leadershipplanuser>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Where(x => {
                        return true;
                    }).Adapt<List<Leadershipplanuser>>();
                    
                    var storageable = _leadershipplanuserRep.Context.Storageable(rows)
                        .SplitError(it => it.Item.Type?.Length > 32, "人员类型长度不能超过32个字符")
                        .SplitError(it => it.Item.UserName?.Length > 32, "人员姓名长度不能超过32个字符")
                        .SplitError(it => it.Item.DeptName?.Length > 32, "部门名称长度不能超过32个字符")
                        .SplitInsert(_ => true)
                        .ToStorage();
                    
                    storageable.BulkCopy();
                    storageable.BulkUpdate();
                    
                    // 标记错误信息
                    markerErrorAction.Invoke(storageable, pageItems, rows);
                });
            });
            
            return stream;
        }
    }
}
