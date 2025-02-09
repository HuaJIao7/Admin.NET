// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http;
using NewLife.Reflection;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;

namespace Admin.NET.Application;

/// <summary>
/// 带班计划服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadershipplanService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadershipplan> _leadershipplanRep;
    private readonly SqlSugarRepository<Leadershipplanuser> _leadershipplanuserRep;
    private readonly ISqlSugarClient _sqlSugarClient;

    public LeadershipplanService(SqlSugarRepository<Leadershipplan> leadershipplanRep, ISqlSugarClient sqlSugarClient, SqlSugarRepository<Leadershipplanuser> leadershipplanuserRep)
    {
        _leadershipplanRep = leadershipplanRep;
        _sqlSugarClient = sqlSugarClient;
        _leadershipplanuserRep = leadershipplanuserRep;
    }

    /// <summary>
    /// 分页查询带班计划 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班计划")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadershipplanOutput>> Page(PageLeadershipplanInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadershipplanRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Shift.Contains(input.Keyword) || u.Status.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Shift), u => u.Shift.Contains(input.Shift.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Status), u => u.Status.Contains(input.Status.Trim()))
            //.WhereIF(input.ShiftTimeRange?.Length == 2, u => u.ShiftTime >= input.ShiftTimeRange[0] && u.ShiftTime <= input.ShiftTimeRange[1])
            .Select<LeadershipplanOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }


    /// <summary>
    /// 分页查询全天带班计划 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询全天带班计划")]
    [ApiDescriptionSettings(Name = "OneDayPage"), HttpPost]
    public async Task<SqlSugarPagedList<LeadershipplanOneDayOutput>> OneDayPage(PageLeadershipplanOneDayInput input)
    {
        input.Keyword = input.Keyword?.Trim();

        var query = _leadershipplanRep.AsQueryable()
            .LeftJoin<Leadershipplan>((o, l1) => o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") == l1.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") && l1.Shift=="早班" )
            .LeftJoin<Leadershipplanuser>((o, l1, r1) => l1.Id == r1.PlanId)
            .LeftJoin<Leadershipplan>((o, l1, r1, l2) => o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") == l2.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") && l2.Shift == "中班")
            .LeftJoin<Leadershipplanuser>((o, l1, r1, l2, r2) => l2.Id == r2.PlanId)
            .LeftJoin<Leadershipplan>((o, l1, r1, l2, r2, l3) => o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") == l3.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") && l3.Shift == "晚班")
            .LeftJoin<Leadershipplanuser>((o, l1, r1, l2, r2, l3, r3) => l3.Id == r3.PlanId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ShiftName), (o, l1, r1, l2, r2, l3, r3) => o.ShiftName.Contains(input.ShiftName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), (o, l1, r1, l2, r2, l3, r3) => r1.UserName.Contains(input.UserName.Trim()) || r2.UserName.Contains(input.UserName.Trim()) || r3.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Status), (o, l1, r1, l2, r2, l3, r3) => o.Status.Contains(input.Status.Trim()))
            .WhereIF(input.ShiftTimeRange.HasValue, (o, l1, r1, l2, r2, l3, r3) => o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") == input.ShiftTimeRange.ToDateTime().ToString("yyyy-MM-dd"))
            .GroupBy(o => o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd"))
            .Select((o, l1,r1,l2,r2,l3,r3) => new LeadershipplanOneDayOutput
            {
                ShiftName = o.ShiftName,//计划名称
                ShiftMorning = l1.Shift,//班次（早班）
                StaffMorning = r1.UserName,//人员（早班）
                ShiftNoon = l2.Shift,//班次（中班）
                StaffNoon = r2.UserName,//人员（中班）
                Shiftevening = l3.Shift,//班次（晚班）
                Staffevening = r3.UserName,//人员（晚班）
                ShiftTime = o.ShiftTime.ToDateTime().ToString("yyyy-MM-dd"),//带班时间
                Status = o.Status  //状态
            });
        return await query.ToPagedListAsync(input.Page, input.PageSize);

    }


    /// <summary>
    /// 增加带班计划 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班计划")]
    [ApiDescriptionSettings(Name = "AddOneDay"), HttpPost]
    public async Task<long> AddOneDay(AddLeadershipplanOneDayInput input)
    {
        var entity = input.Adapt<Leadershipplan>();
        Leadershipplan leadershipplan = new Leadershipplan();
        leadershipplan.ShiftName = input.ShiftName;
        leadershipplan.ShiftTime = input.ShiftTime;
        leadershipplan.Shift = input.ShiftMorning;
        leadershipplan.Status = "正常";
        await _leadershipplanRep.InsertAsync(leadershipplan);//新增早班信息
        //新增早班人员信息  先通过姓名查询出人员信息和部门信息


        Leadershipplanuser leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassLeaderMorning;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassLeaderMorning;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增早班带班领导信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyLeaderMorning;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyLeaderMorning;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增早班值班领导信息


        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyMorning;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyMorning;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增早班值班人员信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassrMorning;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassrMorning;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增早班带班人员信息


        leadershipplan = new Leadershipplan();
        leadershipplan.ShiftName = input.ShiftName;
        leadershipplan.ShiftTime = input.ShiftTime;
        leadershipplan.Shift = input.ShiftNoon;
        leadershipplan.Status = "正常";
        await _leadershipplanRep.InsertAsync(leadershipplan);//新增中班信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassLeaderNoon;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassLeaderNoon;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增中班带班领导信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyLeaderNoon;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyLeaderNoon;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增中班值班领导信息


        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyNoon;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyNoon;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增中班值班人员信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassrNoon;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassrNoon;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增中班带班人员信息



        leadershipplan = new Leadershipplan();
        leadershipplan.ShiftName = input.ShiftName;
        leadershipplan.ShiftTime = input.ShiftTime;
        leadershipplan.Shift = input.ShiftEvening;
        leadershipplan.Status = "正常";
        await _leadershipplanRep.InsertAsync(leadershipplan);//新增晚班信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassLeadertEvening;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassLeadertEvening;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增晚班带班领导信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班领导";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyLeaderEvening;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyLeaderEvening;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增晚班值班领导信息


        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "值班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.DutyEvening;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.DutyEvening;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增晚班值班人员信息

        leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.PlanId = leadershipplan.Id;
        leadershipplanuser.Type = "带班人员";
        leadershipplanuser.UserId = 1;//领班领导id查询
        leadershipplanuser.UserName = input.ClassrEvening;
        leadershipplanuser.DeptId = 1;//领班领导id查询出部门名称和部门id
        leadershipplanuser.DeptName = input.ClassrEvening;
        await _leadershipplanuserRep.InsertAsync(leadershipplanuser);//新增晚班带班人员信息

        return leadershipplan.Id;
    }





    /// <summary>
    /// 通过班次查找调休人员 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("通过班次查找调休人员")]
    [ApiDescriptionSettings(Name = "UserDetail"), HttpPost]
    public async Task<SqlSugarPagedList<LeadershipplanuserOutput>> UserDetail(PageLeadershipplannOneDayShiftInput input)
    {
        input.Keyword = input.Keyword?.Trim();

        var query = _leadershipplanuserRep.AsQueryable()
             .LeftJoin<Leadershipplan>((o, l1) => o.PlanId == l1.Id)
             .WhereIF(!string.IsNullOrWhiteSpace(input.Shift), (o, l1) => l1.Shift.Contains(input.Shift.Trim()))
             .WhereIF(input.ShiftTime.HasValue, (o, l1) => l1.ShiftTime.ToDateTime().ToString("yyyy-MM-dd") == input.ShiftTime.ToDateTime().ToString("yyyy-MM-dd"))

             .Select<LeadershipplanuserOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);

    }




    /// <summary>
    /// 修改替班 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("修改替班")]
    [ApiDescriptionSettings(Name = "UpdateDay"), HttpPost]
    public async Task UpdateDay(UpdateLeadershipplanUserDayInput input)
    {
        var lu = _leadershipplanuserRep.AsQueryable().Where(x => x.Id == input.Id);
        var luPlanId = lu.Select(x => x.PlanId).ToList().FirstOrDefault();
        var luType = lu.Select(x => x.Type).ToList().FirstOrDefault();
        var luUserId = lu.Select(x => x.UserId).ToList().FirstOrDefault();
        var luDeptId = lu.Select(x => x.DeptId).ToList().FirstOrDefault();
        var luDeptName = lu.Select(x => x.DeptName).ToList().FirstOrDefault();
        //查人员表，查出替班人的人员id及部门id名称


        Leadershipplanuser leadershipplanuser = new Leadershipplanuser();
        leadershipplanuser.Id = input.Id.ToLong();
        leadershipplanuser.PlanId = luPlanId;
        leadershipplanuser.Type = luType;
        leadershipplanuser.UserId = luUserId;
        leadershipplanuser.UserName = input.reliefUser;
        leadershipplanuser.DeptId = luDeptId;
        leadershipplanuser.DeptName = luDeptName;
        await _leadershipplanuserRep.AsUpdateable(leadershipplanuser)
        .ExecuteCommandAsync();
    }



    /// <summary>
    /// 获取带班计划详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班计划详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadershipplan> Detail([FromQuery] QueryByIdLeadershipplanInput input)
    {
        return await _leadershipplanRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加带班计划 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班计划")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddLeadershipplanInput input)
    {
        var entity = input.Adapt<Leadershipplan>();
        return await _leadershipplanRep.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新带班计划 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新带班计划")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateLeadershipplanInput input)
    {
        var entity = input.Adapt<Leadershipplan>();
        await _leadershipplanRep.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除带班计划 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班计划")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadershipplanInput input)
    {
        var entity = await _leadershipplanRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadershipplanRep.FakeDeleteAsync(entity);   //假删除
        //await _leadershipplanRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班计划 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班计划")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteLeadershipplanInput> input)
    {
        var exp = Expressionable.Create<Leadershipplan>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadershipplanRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadershipplanRep.FakeDeleteAsync(list);   //假删除
        //return await _leadershipplanRep.DeleteAsync(list);   //真删除
    }
    
    /// <summary>
    /// 导出带班计划记录 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("导出带班计划记录")]
    [ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    public async Task<IActionResult> Export(PageLeadershipplanInput input)
    {
        var list = (await Page(input)).Items?.Adapt<List<ExportLeadershipplanOutput>>() ?? new();
        if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
        return ExcelHelper.ExportTemplate(list, "带班计划导出记录");
    }
    
    /// <summary>
    /// 下载带班计划数据导入模板 ⬇️
    /// </summary>
    /// <returns></returns>
    [DisplayName("下载带班计划数据导入模板")]
    [ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    public IActionResult DownloadTemplate()
    {
        return ExcelHelper.ExportTemplate(new List<ExportLeadershipplanOutput>(), "带班计划导入模板");
    }
    
    /// <summary>
    /// 导入带班计划记录 💾
    /// </summary>
    /// <returns></returns>
    [DisplayName("导入带班计划记录")]
    [ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    public IActionResult ImportData([Required] IFormFile file)
    {
        lock (this)
        {
            var stream = ExcelHelper.ImportData<ImportLeadershipplanInput, Leadershipplan>(file, (list, markerErrorAction) =>
            {
                _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
                {
                    
                    // 校验并过滤必填基本类型为null的字段
                    var rows = pageItems.Adapt<List<Leadershipplan>>();
                    Thread.Sleep(1000);

                    var storageable = _leadershipplanRep.Context.Storageable(rows)
                        .SplitError(it => it.Item.Shift?.Length > 32, "班次长度不能超过32个字符")
                        .SplitError(it => it.Item.Status?.Length > 32, "状态长度不能超过32个字符")
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
