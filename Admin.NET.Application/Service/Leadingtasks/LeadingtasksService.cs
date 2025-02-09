// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core;
using Admin.NET.Core.Service;
using Elastic.Clients.Elasticsearch.Snapshot;
using Flurl;
using Furion.ClayObject.Extensions;
using Mapster;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

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
    /// 分页查询带班检查结果 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("带班检查结果")]
    [ApiDescriptionSettings(Name = "InspectPage"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingtasksInspectOutput>> InspectPage(PageInspectLeadingtasksInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadingtasksRep.AsQueryable()
            .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ShiftName), (o, cus) => cus.ShiftName.Contains(input.ShiftName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Location), (o, cus) => o.Location.Contains(input.Location.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Shift), (o, cus) => cus.Shift.Contains(input.Shift.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), (o, cus) => o.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Content), (o, cus) => o.Content.Contains(input.Content.Trim()))

            .Select((o, cus) => new LeadingtasksInspectOutput
            {
                Id = o.Id,
                ShiftName = cus.ShiftName,
                Location = o.Location,
                Shift = cus.Shift,
                UserId = o.UserId,
                UserName = o.UserName,
                Content = o.Content
            }).OrderBy("o.Id");
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班检查结果详情 ℹ️
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    [DisplayName("带班检查结果详情")]
    [ApiDescriptionSettings(Name = "InspectDetail"), HttpGet]
    public async Task<LeadingtasksInspectOutput> InspectDetail(long pid)
    {
        //查询  带班检查结果   上传的文件之后按不同类型进行分类后赋值传出
        var file = _leadingtasksfileRep.AsQueryable().Where(x => x.TaskId == pid);
        var file_img = file.Where(x => x.Type == "图片").Select(x => x.Url).ToList();
        var file_videoFile = file.Where(x => x.Type == "视频").Select(x => x.Url).ToList();
        var file_voiceFile = file.Where(x => x.Type == "语音").Select(x => x.Url).ToList();


        var query = _leadingtasksRep.AsQueryable()
                .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
                .WhereIF(pid != null, o => o.Id == pid)
            .Select((o, cus) => new LeadingtasksInspectOutput
            {
                Id = o.Id,
                ShiftName = cus.ShiftName,
                Location = o.Location,
                Shift = cus.Shift,
                UserId = o.UserId,
                UserName = o.UserName,
                Content = o.Content,
                //imgFile = repUser.Url,
                imgFile = string.Join(", ", file_img),
                videoFile = string.Join(", ", file_videoFile),
                voiceFile = string.Join(", ", file_voiceFile)
            }).FirstAsync();
        return await query;

    }


    /// <summary>
    /// 分页查询带班任务结果 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班任务结果")]
    [ApiDescriptionSettings(Name = "LeadingTasksPage"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingtasksLeadingTasksOutput>> LeadingTasksPage(PageInspectLeadingtasksInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadingtasksRep.AsQueryable()
            .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ShiftName), (o, cus) => cus.ShiftName.Contains(input.ShiftName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Location), (o, cus) => o.Location.Contains(input.Location.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Shift), (o, cus) => cus.Shift.Contains(input.Shift.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), (o, cus) => o.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleUserName), (o, cus) => o.UserName.Contains(input.HandleUserName.Trim()))

            //计划名称、地点、班次、处理人员、下达人
            .Select((o, cus) => new LeadingtasksLeadingTasksOutput
            {
                //处理人员id 查询好了告诉  李姝函
                // _leadingtasksfileRep.AsQueryable()

                Id = o.Id,
                ShiftName = cus.ShiftName,  //计划名称
                Location = o.Location,  //地点
                Shift = cus.Shift,      //班次
                HandleUserId = o.UserId,//处理人员id  重新查询带班计划人员   
                HandleUserName = o.UserName,//处理人员姓名  重新查询带班计划人员
                UserId = o.UserId,  //下达人id
                UserName = o.UserName,//下达人姓名
                Time = o.Time,
                Content = o.Content,//任务内容
                Status = o.Status,//状态
                Description = o.Description,//处理结果
                UpprocessingTime = o.UpdateTime,//处理时间
            }).OrderBy("o.Id");
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班任务结果详情 ℹ️
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    [DisplayName("获取带班任务结果详情")]
    [ApiDescriptionSettings(Name = "LeadingTasksDetail"), HttpGet]
    public async Task<LeadingtasksLeadingTasksOutput> LeadingTasksDetail(long pid)
    {

        //查询  带班检查结果   上传的文件之后按不同类型进行分类后赋值传出
        var file = _leadingtasksfileRep.AsQueryable().Where(x => x.TaskId == pid);
        var file_img = file.Where(x => x.Type == "图片").Select(x => x.Url).ToList();
        var file_videoFile = file.Where(x => x.Type == "视频").Select(x => x.Url).ToList();
        var file_voiceFile = file.Where(x => x.Type == "语音").Select(x => x.Url).ToList();

        var query = _leadingtasksRep.AsQueryable()
                .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
                .WhereIF(pid != null, o => o.Id == pid)
            .Select((o, cus) => new LeadingtasksLeadingTasksOutput
            {
                Id = o.Id,
                ShiftName = cus.ShiftName,  //计划名称
                Location = o.Location,  //地点
                Shift = cus.Shift,      //班次
                HandleUserId = o.UserId,//处理人员id  重新查询带班计划人员
                HandleUserName = o.UserName,//处理人员姓名  重新查询带班计划人员
                UserId = o.UserId,  //下达人id
                UserName = o.UserName,//下达人姓名
                Time = o.Time,//下达时间
                Content = o.Content,//任务内容
                Status = o.Status,//状态
                Description = o.Description,//处理结果
                UpprocessingTime = o.UpdateTime,//处理时间
                imgFile = string.Join(", ", file_img),
                videoFile = string.Join(", ", file_videoFile),
                voiceFile = string.Join(", ", file_voiceFile)
            });

        return await query.FirstAsync();

    }




    /// <summary>
    /// 分页查询带班检查结果测试 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("带班检查结果测试")]
    [ApiDescriptionSettings(Name = "InspectPageCs"), HttpPost]
    public async Task<LeadingtasksInspectOutput> InspectPageCs(long pid)
    {
        //查询  带班检查结果   上传的文件之后按不同类型进行分类后赋值传出
        var file = _leadingtasksfileRep.AsQueryable().Where(x => x.TaskId == pid);
        var file_img = file.Where(x => x.Type == "图片").Select(x => x.Url).ToList();
        var file_videoFile = file.Where(x => x.Type == "视频").Select(x => x.Url).ToList();
        var file_voiceFile = file.Where(x => x.Type == "语音").Select(x => x.Url).ToList();


        var query = _leadingtasksRep.AsQueryable()
                .LeftJoin<Leadershipplan>((o, cus) => o.PlanId == cus.Id)
                .WhereIF(pid != null, o => o.Id == pid)
            .Select((o, cus) => new LeadingtasksInspectOutput
            {
                Id = o.Id,
                ShiftName = cus.ShiftName,
                Location = o.Location,
                Shift = cus.Shift,
                UserId = o.UserId,
                UserName = o.UserName,
                Content = o.Content,
                //imgFile = repUser.Url,
                imgFile = string.Join(", ", file_img),
                videoFile = string.Join(", ", file_videoFile),
                voiceFile = string.Join(", ", file_voiceFile)
            }).FirstAsync();
        return await query;

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
        // entity.files = await _leadingtasksfileRep.AsQueryable().ClearFilter().Where(x => x.TaskId == entity.Id).ToListAsync();
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
        entity.PlanId = input.PlanId;
        entity.UserId = input.UserId;
        entity.UserName = input.UserName;
        entity.DeptId = input.DeptId;
        entity.DeptName = input.DeptName;
        entity.Location = input.Location;
        entity.Content = input.Content;
        entity.Time = input.Time;
        entity.Description = input.Description;
        entity.Status = input.Status;
        entity.Type = input.Type;
        await _leadingtasksRep.InsertAsync(entity);
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
        entity.PlanId = input.PlanId;
        entity.UserId = input.UserId;
        entity.UserName = input.UserName;
        entity.DeptId = input.DeptId;
        entity.DeptName = input.DeptName;
        entity.Location = input.Location;
        entity.Content = input.Content;
        entity.Time = input.Time;
        entity.Description = input.Description;
        entity.Status = input.Status;
        entity.Type = input.Type;

        await _leadingtasksRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }


    /// <summary>
    /// 删除带班任务上报 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班任务上报")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadingtasksInput input)
    {
        var entity = await _leadingtasksRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadingtasksRep.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班任务上报 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班任务上报")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DeleteLeadingtasksInput> input)
    {
        var exp = Expressionable.Create<Leadingtasks>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadingtasksRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadingtasksRep.FakeDeleteAsync(list);   //假删除
        //return await _leadingtasksRep.DeleteAsync(list);   //真删除
    }

    /// <summary>
    /// 导出带班任务上报记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出带班任务上报记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageLeadingtasksInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportLeadingtasksOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "带班任务上报导出记录");
    }

    /// <summary>
    /// 下载带班任务上报数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载带班任务上报数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportLeadingtasksOutput>(), "带班任务上报导入模板");
    }


}
