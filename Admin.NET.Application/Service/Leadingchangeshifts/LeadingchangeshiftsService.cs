// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using Aliyun.OSS.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnceMi.AspNetCore.OSS;
using Yitter.IdGenerator;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinExpressIntracityUpdateStoreRequest.Types;

namespace Admin.NET.Application;

/// <summary>
/// 带班计划交接班服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadingchangeshiftsService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadingchangeshifts> _leadingchangeshiftsRep;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly UploadOptions _uploadOptions;
    private readonly SqlSugarRepository<SysFile> _sysFileRep;
    private readonly OSSProviderOptions _OSSProviderOptions;
    private readonly IOSSService _OSSService;

    public LeadingchangeshiftsService(SqlSugarRepository<Leadingchangeshifts> leadingchangeshiftsRep,
        ISqlSugarClient sqlSugarClient, IOptions<UploadOptions> uploadOptions, SqlSugarRepository<SysFile> sysFileRep, IOptions<OSSProviderOptions> oSSProviderOptions,
        IOSSServiceFactory ossServiceFactory)
    {
        _leadingchangeshiftsRep = leadingchangeshiftsRep;
        _sqlSugarClient = sqlSugarClient;
    }

    /// <summary>
    /// 带班交接班记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("带班交接班记录详情")]
    [ApiDescriptionSettings(Name = "ChangeShiftsDetail"), HttpPost]
    public async Task<LeadingchangeshiftsChangeShiftsOutput> ChangeShiftsDetail(long pid)
    {
        var query = _leadingchangeshiftsRep.AsQueryable()
            .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
           .WhereIF(pid != null, o => o.Id == pid)
            .Select((o, cus) => new LeadingchangeshiftsChangeShiftsOutput
            {
                Id = o.Id,
                ShiftName = cus.ShiftName,
                Classes = o.Classes,
                TakeUserId = o.TakeUserId,
                TakeUserName = o.TakeUserName,
                UserId = o.UserId,
                UserName = o.UserName,
                Time = o.Time,
                Status = o.Status,
                imgFile =o.ImgUrl,//图片
                videoFile = o.VideoUrl,//视频
                Content=o.Content
            }).OrderBy("o.Id");
        return await query.FirstAsync();
    }


    /// <summary>
    /// 分页查询带班计划交接班 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班计划交接班")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingchangeshiftsOutput>> Page(PageLeadingchangeshiftsInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadingchangeshiftsRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Status.Contains(input.Keyword) || u.UserName.Contains(input.Keyword) || u.DeptName.Contains(input.Keyword) || u.TakeUserName.Contains(input.Keyword) || u.TakeDeptName.Contains(input.Keyword) || u.Content.Contains(input.Keyword) || u.ImgUrl.Contains(input.Keyword) || u.VideoUrl.Contains(input.Keyword) || u.Classes.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Status), u => u.Status.Contains(input.Status.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeptName), u => u.DeptName.Contains(input.DeptName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.TakeUserName), u => u.TakeUserName.Contains(input.TakeUserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.TakeDeptName), u => u.TakeDeptName.Contains(input.TakeDeptName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Content), u => u.Content.Contains(input.Content.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ImgUrl), u => u.ImgUrl.Contains(input.ImgUrl.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.VideoUrl), u => u.VideoUrl.Contains(input.VideoUrl.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Classes), u => u.Classes.Contains(input.Classes.Trim()))
            .WhereIF(input.PlanId != null, u => u.PlanId == input.PlanId)
            .WhereIF(input.UserId != null, u => u.UserId == input.UserId)
            .WhereIF(input.Deptid != null, u => u.Deptid == input.Deptid)
            .WhereIF(input.TakeUserId != null, u => u.TakeUserId == input.TakeUserId)
            .WhereIF(input.TakeDeptid != null, u => u.TakeDeptid == input.TakeDeptid)
            .WhereIF(input.TimeRange?.Length == 2, u => u.Time >= input.TimeRange[0] && u.Time <= input.TimeRange[1])
            .Select<LeadingchangeshiftsOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班计划交接班详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班计划交接班详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadingchangeshifts> Detail([FromQuery] QueryByIdLeadingchangeshiftsInput input)
    {
        return await _leadingchangeshiftsRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加带班计划交接班 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班计划交接班")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddLeadingchangeshiftsInput input)
    {
        var entity = input.Adapt<Leadingchangeshifts>();
        return await _leadingchangeshiftsRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新带班计划交接班 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新带班计划交接班")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateLeadingchangeshiftsInput input)
    {
        var entity = input.Adapt<Leadingchangeshifts>();
        await _leadingchangeshiftsRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除带班计划交接班 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班计划交接班")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadingchangeshiftsInput input)
    {
        var entity = await _leadingchangeshiftsRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadingchangeshiftsRep.FakeDeleteAsync(entity);   //假删除
        //await _leadingchangeshiftsRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班计划交接班 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班计划交接班")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteLeadingchangeshiftsInput> input)
    {
        var exp = Expressionable.Create<Leadingchangeshifts>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadingchangeshiftsRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadingchangeshiftsRep.FakeDeleteAsync(list);   //假删除
        //return await _leadingchangeshiftsRep.DeleteAsync(list);   //真删除
    }
    
    /// <summary>
    /// 导出带班计划交接班记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出带班计划交接班记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageLeadingchangeshiftsInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportLeadingchangeshiftsOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "带班计划交接班导出记录");
    }
    
    /// <summary>
    /// 下载带班计划交接班数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载带班计划交接班数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportLeadingchangeshiftsOutput>(), "带班计划交接班导入模板");
    }
    
    /// <summary>
    /// 导入带班计划交接班记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入带班计划交接班记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (this)
        {
            var stream = ExcelHelper.ImportData<ImportLeadingchangeshiftsInput, Leadingchangeshifts>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Adapt<List<Leadingchangeshifts>>();

                    Thread.Sleep(1000);
                    var storageable = _leadingchangeshiftsRep.Context.Storageable(rows)
                        .SplitError(it => it.Item.Status?.Length > 10, "状态长度不能超过10个字符")
                        .SplitError(it => it.Item.UserName?.Length > 32, "交班人姓名长度不能超过32个字符")
                        .SplitError(it => it.Item.DeptName?.Length > 32, "交班人部门名称长度不能超过32个字符")
                        .SplitError(it => it.Item.TakeUserName?.Length > 32, "接班人姓名长度不能超过32个字符")
                        .SplitError(it => it.Item.TakeDeptName?.Length > 32, "接班人部门名称长度不能超过32个字符")
                        .SplitError(it => it.Item.Content?.Length > 500, "交接班内容长度不能超过500个字符")
                        .SplitError(it => it.Item.ImgUrl?.Length > 500, "交接班图片长度不能超过500个字符")
                        .SplitError(it => it.Item.VideoUrl?.Length > 500, "交接班视频长度不能超过500个字符")
                        .SplitError(it => it.Item.Classes?.Length > 32, "班次长度不能超过32个字符")
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
