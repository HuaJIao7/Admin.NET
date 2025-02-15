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
using Admin.NET.Application.Service.InspectionRecordService.Dto;
using Microsoft.AspNetCore.Routing;

namespace Admin.NET.Application.Service.InspectionRecordService;

/// <summary>
/// 巡检记录
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class InspectionRecordService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<InspectionRecord> _InspectionRecord;
    private readonly SqlSugarRepository<InspectionRecordDto> _InspectionRecorddto;

    public InspectionRecordService(
        SqlSugarRepository<InspectionRecord> InspectionRecord, SqlSugarRepository<InspectionRecordDto> InspectionRecorddto)
    {
        _InspectionRecord = InspectionRecord;
        _InspectionRecorddto = InspectionRecorddto;
    }

    [DisplayName("巡检记录增加")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(InspectionRecordDto input)
    {
        try
        {
            var entity = input.Adapt<InspectionRecord>();
            entity.Name = input.Name;
            entity.UserInformationId = input.UserInformationId;
            entity.Shift = input.Shift;
            entity.Cycle = input.Cycle;
            entity.Route = input.Route;
            entity.HandleVideo = input.HandleVideo;
            entity.HandleMp3 = input.HandleMp3;
            entity.HandleImg = input.HandleImg;
            entity.leadershipplanId = input.leadershipplanId;
            entity.State = input.State;
            await _InspectionRecord.InsertAsync(entity);
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
    public async Task Delete(InspectionRecordDto input)
    {
        var entity = await _InspectionRecord.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _InspectionRecord.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksfileRep.DeleteAsync(entity);   //真删除
    }
    
    

    [DisplayName("巡检记录修改")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task UpdateProblemcentered(InspectionRecordDto input)
    {
        try
        {
            var entity = input.Adapt<Entity.InspectionRecord>();
            await _InspectionRecord.AsUpdateable(entity)
                .Where(u => u.Id == entity.Id)
                .ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [DisplayName("巡检记录查询")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<List<InspectionRecord>> Page(PageLeadingchangeshiftsInput input)
    {
        var entity = await _InspectionRecord.AsQueryable().ClearFilter().ToListAsync();
        return entity;
        //var query = _InspectionRecord.AsQueryable()
        //    .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
        //    .Select<InspectionRecordDto>();
        //return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    [DisplayName("巡检记录条件查询")]
    [ApiDescriptionSettings(Name = "ConditionalQuery"), HttpPost]
    public async Task<SqlSugarPagedList<InspectionRecord>> ConditionalQuery(InspectionRecordCon input)
    {
        var query = _InspectionRecord.AsQueryable()
            .LeftJoin<RouteSchedule>((a,b) => a.Id == b.InspectionRecordId)
            .Select((a,b) => new InspectionRecord
            {
               // InspectionRecordId = b.InspectionRecordId,
            });


        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

}
