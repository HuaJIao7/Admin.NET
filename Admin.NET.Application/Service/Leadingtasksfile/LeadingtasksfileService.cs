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
/// 带班任务上报文件服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadingtasksfileService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadingtasksfile> _leadingtasksfileRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public LeadingtasksfileService(SqlSugarRepository<Leadingtasksfile> leadingtasksfileRep, ISqlSugarClient sqlSugarClient)
    {
        _leadingtasksfileRep = leadingtasksfileRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 分页查询带班任务上报文件 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingtasksfileOutput>> Page(PageLeadingtasksfileInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadingtasksfileRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Type.Contains(input.Keyword) || u.Url.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Type), u => u.Type.Contains(input.Type.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Url), u => u.Url.Contains(input.Url.Trim()))
            .WhereIF(input.TaskId != null, u => u.TaskId == input.TaskId)
            .Select<LeadingtasksfileOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班任务上报文件详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班任务上报文件详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadingtasksfile> Detail([FromQuery] QueryByIdLeadingtasksfileInput input)
    {
        return await _leadingtasksfileRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加带班任务上报文件 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddLeadingtasksfileInput input)
    {
        var entity = input.Adapt<Leadingtasksfile>();
        return await _leadingtasksfileRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新带班任务上报文件 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateLeadingtasksfileInput input)
    {
        var entity = input.Adapt<Leadingtasksfile>();
        await _leadingtasksfileRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除带班任务上报文件 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadingtasksfileInput input)
    {
        var entity = await _leadingtasksfileRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadingtasksfileRep.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksfileRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班任务上报文件 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班任务上报文件")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteLeadingtasksfileInput> input)
    {
        var exp = Expressionable.Create<Leadingtasksfile>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadingtasksfileRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadingtasksfileRep.FakeDeleteAsync(list);   //假删除
        //return await _leadingtasksfileRep.DeleteAsync(list);   //真删除
    }
    
    /// <summary>
    /// 导出带班任务上报文件记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出带班任务上报文件记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageLeadingtasksfileInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportLeadingtasksfileOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "带班任务上报文件导出记录");
    }
    
    /// <summary>
    /// 下载带班任务上报文件数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载带班任务上报文件数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportLeadingtasksfileOutput>(), "带班任务上报文件导入模板");
    }
    
    /// <summary>
    /// 导入带班任务上报文件记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入带班任务上报文件记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (this)
        {
            var stream = ExcelHelper.ImportData<ImportLeadingtasksfileInput, Leadingtasksfile>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Where(x => {
                        return true;
                    }).Adapt<List<Leadingtasksfile>>();
                    
                    var storageable = _leadingtasksfileRep.Context.Storageable(rows)
                        .SplitError(it => it.Item.Type?.Length > 32, "文件类型长度不能超过32个字符")
                        .SplitError(it => it.Item.Url?.Length > 500, "文件路径长度不能超过500个字符")
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
