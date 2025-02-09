// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Admin.NET.Application;

/// <summary>
/// 值班计划服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class DutyScheduleService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<DutySchedule> _dutyScheduleRep;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<SysUser> _user;
    private readonly SqlSugarRepository<SysOrg> _org;
    private readonly SqlSugarRepository<Leadershipplan> _leadershipplanRep;
    private readonly SqlSugarRepository<Leadershipplanuser> _leadershipplanuserRep;
    private readonly SqlSugarRepository<SysDictData> _dictDataRep;
    public DutyScheduleService(SqlSugarRepository<DutySchedule> dutyScheduleRep, ISqlSugarClient sqlSugarClient, SqlSugarRepository<SysUser> user, SqlSugarRepository<SysOrg> org, SqlSugarRepository<Leadershipplan> leadershipplanRep, SqlSugarRepository<Leadershipplanuser> leadershipplanuserRep, SqlSugarRepository<SysDictData> dictDataRep)
    {
        _dutyScheduleRep = dutyScheduleRep;
        _sqlSugarClient = sqlSugarClient;
        _user = user;
        _org = org;
        _leadershipplanRep = leadershipplanRep;
        _leadershipplanuserRep = leadershipplanuserRep;
        _dictDataRep = dictDataRep;
    }

    /// <summary>
    /// 分页查询值班计划 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询值班计划")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<DutyScheduleOutput>> Page(PageDutyScheduleInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _dutyScheduleRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeptName), u => u.DeptName.Contains(input.DeptName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Type), u => u.Type.Contains(input.Type.Trim()))
            .WhereIF(input.UserId != null, u => u.UserId == input.UserId)
            .WhereIF(input.DeptId != null, u => u.DeptId == input.DeptId)
            .Select<DutyScheduleOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取值班计划详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取值班计划详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<DutySchedule> Detail([FromQuery] QueryByIdDutyScheduleInput input)
    {
        return await _dutyScheduleRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加值班计划 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加值班计划")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public long Add(List<AddDutyScheduleInput> input)
    {
        //var dicData = _dictDataRep.AsQueryable().ClearFilter().Where(x => x.DictTypeId == 628234625433669).ToList();
        //int count = _dutyScheduleRep.AsQueryable().ClearFilter().Where(x => x.DutyDate.Value.Date == input[0].DutyDate.Value.Date).Count();
        //if (count > 0)
        //    throw Oops.Oh(ErrorCodeEnum.P1000);
        int i = 0;

        //#region 添加带班计划
        //dicData.ForEach(dic => {
        //    Leadershipplan plan = new Leadershipplan();
        //    plan.ShiftTime = input[0].DutyDate;
        //    plan.Shift = dic.Value;
        //    plan.ShiftName = input[0].DeptName + input[0].Flights;
        //    plan.Status = "未接班";
        //    _leadershipplanRep.Insert(plan);
        //    //添加带班计划人员
        //    input.ForEach(item =>
        //    {
        //        if (item.Flights.Equals(dic.Value) || item.Type == "值班领导" || item.Type == "带班领导")
        //        {
        //            Leadershipplanuser planUser = new Leadershipplanuser();
        //            planUser.Type = item.Type;
        //            planUser.UserId = item.UserId;
        //            planUser.DeptId = item.DeptId;
        //            var User = _user.AsQueryable().ClearFilter().Where(x => x.Id == item.UserId).First();
        //            var UserDept = _org.AsQueryable().ClearFilter().Where(x => x.Id == User.OrgId).First();
        //            planUser.PlanId = plan.Id;
        //            planUser.DeptName = UserDept.Name;
        //            planUser.UserName = User.RealName;
        //            _leadershipplanuserRep.Insert(planUser);
        //        }
        //    });
        //});
        //#endregion

        input.ForEach(item => {
            DutySchedule duty = new DutySchedule();
            duty.UserId = item.UserId;
            duty.UserName = item.UserName;
            duty.DeptId = item.DeptId;
            duty.DeptName = item.DeptName;
            duty.Flights = item.Flights;
            duty.DutyDate = item.DutyDate;
            duty.Type = item.Type;
            _dutyScheduleRep.Insert(duty);
            i++;
        });

        return i;
    }

    /// <summary>
    /// 更新值班计划 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新值班计划")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(List<UpdateDutyScheduleInput> input)
    {

        await _dutyScheduleRep.AsDeleteable().Where(x => x.DutyDate == input[0].DutyDate).ExecuteCommandAsync();
        input.ForEach(async item => {
            var duty = await _dutyScheduleRep.AsQueryable().ClearFilter().Where(x => x.Id == item.Id).FirstAsync();
            if (duty == null)
                throw Oops.Oh(ErrorCodeEnum.D1002);
            duty.UserId = item.UserId;
            duty.UserName = item.UserName;
            duty.DeptId = item.DeptId;
            duty.DeptName = item.DeptName;
            duty.Flights = item.Flights;
            duty.DutyDate = item.DutyDate;
            duty.Type = item.Type;
            await _dutyScheduleRep.AsUpdateable(duty).ExecuteCommandAsync();
        });
    }

    /// <summary>
    /// 删除值班计划 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除值班计划")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteDutyScheduleInput input)
    {
        var entity = await _dutyScheduleRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _dutyScheduleRep.FakeDeleteAsync(entity);   //假删除
        //await _dutyScheduleRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除值班计划 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除值班计划")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteDutyScheduleInput> input)
    {
        var exp = Expressionable.Create<DutySchedule>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _dutyScheduleRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _dutyScheduleRep.FakeDeleteAsync(list);   //假删除
        //return await _dutyScheduleRep.DeleteAsync(list);   //真删除
    }
}
