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
using Admin.NET.Application.Service.PointTableService.Dto;

namespace Admin.NET.Application.Service.PointTableService;

/// <summary>
/// 点位表 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class PointTableService: IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<PointTable> _PointTable;
    private readonly SqlSugarRepository<PointTableDto> _PointTabledto;
    private readonly SqlSugarRepository<PointTableInput> _PointTableinput;

    public PointTableService(
        SqlSugarRepository<PointTable> PointTable, SqlSugarRepository<PointTableDto> PointTabledto , SqlSugarRepository<PointTableInput> pointTableinput)
    {
        _PointTable = PointTable;
        _PointTabledto = PointTabledto;
        _PointTableinput = pointTableinput;
    }

    [DisplayName("点位表增加")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(PointTableDto input)
    {
        try
        {
            var entity = input.Adapt<PointTable>();
            entity.InspectionRecordId = input.InspectionRecordId;
            entity.PointName = input.PointName;
            await _PointTable.InsertAsync(entity);
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
    public async Task Delete(PointTableDto input)
    {
        var entity = await _PointTable.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _PointTable.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksfileRep.DeleteAsync(entity);   //真删除
    }
    
    

    [DisplayName("点位表修改")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task UpdateProblemcentered(PointTableDto input)
    {
        try
        {
            var entity = input.Adapt<Entity.PointTable>();
            await _PointTable.AsUpdateable(entity)
                .Where(u => u.Id == entity.Id)
                .ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [DisplayName("点位表查询")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<PointTableDto>> Page(PageLeadingchangeshiftsInput input)
    {
        var query = _PointTable.AsQueryable()
            .Where(u => u.InspectionRecordId == input.leadershipplanId)
            .Select<PointTableDto>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }
}
