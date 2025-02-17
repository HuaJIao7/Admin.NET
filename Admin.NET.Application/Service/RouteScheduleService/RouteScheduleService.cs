// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.NET.Application.Entity;
using Admin.NET.Application.Service.RouteScheduleService.Dto;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Admin.NET.Application.Service.RouteScheduleService;

/// <summary>
/// 路线表 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class RouteScheduleService: IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<RouteSchedule> _RouteSchedule;
    private readonly SqlSugarRepository<PointTable> _PointTable;
    
    private readonly SqlSugarRepository<RouteScheduleDto> _RouteScheduledto;

    public RouteScheduleService(
        SqlSugarRepository<RouteSchedule> RouteSchedule, SqlSugarRepository<RouteScheduleDto> RouteScheduledto, SqlSugarRepository<PointTable> pointTable)
    {
        _RouteSchedule = RouteSchedule;
        _RouteScheduledto = RouteScheduledto;
        _PointTable = pointTable;
    }

    [DisplayName("路线表增加")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(RouteScheduleDto input)
    {
        try
        {
            var entity = input.Adapt<RouteSchedule>();
            entity.InspectionRecordId = input.InspectionRecordId;
            entity.RouteName = input.RouteName;
            entity.PointName = input.PointName;
            await _RouteSchedule.InsertAsync(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="AppFriendlyException"></exception>
    [DisplayName("删除")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(RouteScheduleDto input)
    {
        var entity = await _RouteSchedule.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _RouteSchedule.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksfileRep.DeleteAsync(entity);   //真删除
    }
    
    

    [DisplayName("路线表修改")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task UpdateProblemcentered(RouteScheduleDto input)
    {
        try
        {
            var entity = input.Adapt<Entity.RouteSchedule>();
            await _RouteSchedule.AsUpdateable(entity)
                .Where(u => u.Id == entity.Id)
                .ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [DisplayName("路线表查询")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<RouteScheduleDto>> Page(RouteScheduleInput input)
    {

        var query = _RouteSchedule.AsQueryable()
        .Where(u => u.InspectionRecordId == input.InspectionRecordId || input.InspectionRecordId == null)
        .Where(u => u.RouteName == input.RouteName || input.RouteName == null)
        .Select(route => new RouteScheduleDto
        {
            InspectionRecordId = route.InspectionRecordId,
            RouteName = route.RouteName,
            PointName = route.PointName,
            // 使用 SqlFunc.Subquery 来进行子查询
            PointTables = SqlFunc.Subqueryable<PointTable>().Where(x => x.InspectionRecordId == input.QueryID).ToList()
        });



        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }
}
