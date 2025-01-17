// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.Real_timeData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.Real_timeData;
/// <summary>
/// 人员定位实时数据 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class Real_timeDataService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.Real_timeData> _real_timeData;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<Real_timeDataInput> _real_timedatainputinput;
    private readonly SqlSugarRepository<Real_timeDataDto> _real_timedatadto;
    private readonly SqlSugarRepository<DeleteReal_timeDataInput> _DeleteReal_timeDataInput;
    private readonly SqlSugarRepository<UpdateReal_timeDataInput> _updateRealTimeDataInput;

    public Real_timeDataService(
        SqlSugarRepository<Entity.Real_timeData> real_timeData,
        ISqlSugarClient sqlSugarClient,
        SqlSugarRepository<Real_timeDataInput> real_timedatainputinput,
        SqlSugarRepository<Real_timeDataDto> real_timedatadto,
        SqlSugarRepository<DeleteReal_timeDataInput> deleteRealTimeDataInput,
        SqlSugarRepository<UpdateReal_timeDataInput> updateRealTimeDataInput)
    {
        _real_timeData = real_timeData;
        _sqlSugarClient = sqlSugarClient;
        _real_timedatainputinput = real_timedatainputinput;
        _real_timedatadto = real_timedatadto;
        _DeleteReal_timeDataInput = deleteRealTimeDataInput;
        _updateRealTimeDataInput = updateRealTimeDataInput;
    }

    /// <summary>
    /// 添加实时数据表
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("添加实时数据表")]
    [ApiDescriptionSettings(Name = "AddReal_timeData"), HttpPost]
    public async Task Add(Real_timeDataInput input)
    {
        var entity = input.Adapt<Entity.Real_timeData>();
        entity.PersonnelCardCode = input.PersonnelCardCode;
        entity.Name = input.Name;
        entity.EntranceExitSigns = input.EntranceExitSigns;
        entity.TimeEnteringWell = input.TimeEnteringWell;
        entity.ExitTime = input.ExitTime;
        entity.RegionalCode = input.RegionalCode;
        entity.TimeEnteringCurrentArea = input.TimeEnteringCurrentArea;
        entity.BaseStationCode = input.BaseStationCode;
        entity.EnterCurrentBaseStationTime = input.EnterCurrentBaseStationTime;
        entity.LaborOrganizationMode = input.LaborOrganizationMode;
        entity.DistanceFromBaseStation = input.DistanceFromBaseStation;
        entity.PersonnelWorkStatus = input.PersonnelWorkStatus;
        entity.IsItLeader = input.IsItLeader;
        entity.BaseStationInformations = input.BaseStationInformations;
        await _real_timeData.InsertAsync(entity);
    }

    /// <summary>
    /// 查询实时数据
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("获取实时数据")]
    [ApiDescriptionSettings(Name = "GatReal_timeData"), HttpPost]
    public async Task<List<Entity.Real_timeData>> GatReal_timeData(Real_timeDataInput input)
    {
        var query = _real_timeData.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.PersonnelCardCode), u => u.PersonnelCardCode.Contains(input.PersonnelCardCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.EntranceExitSigns), u => u.EntranceExitSigns.Contains(input.EntranceExitSigns.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.TimeEnteringCurrentArea), u => u.TimeEnteringCurrentArea.Contains(input.TimeEnteringCurrentArea.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationCode), u => u.BaseStationCode.Contains(input.BaseStationCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.EnterCurrentBaseStationTime), u => u.EnterCurrentBaseStationTime.Contains(input.EnterCurrentBaseStationTime.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.LaborOrganizationMode), u => u.LaborOrganizationMode.Contains(input.LaborOrganizationMode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DistanceFromBaseStation), u => u.DistanceFromBaseStation.Contains(input.DistanceFromBaseStation.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(input.TimeEnteringWell != null, u => u.TimeEnteringWell == input.TimeEnteringWell)
            .WhereIF(input.ExitTime != null, u => u.ExitTime == input.ExitTime)
            .WhereIF(input.RegionalCode != null, u => u.RegionalCode == input.RegionalCode)
            .WhereIF(input.PersonnelWorkStatus != null, u => u.PersonnelWorkStatus == input.PersonnelWorkStatus)
            .WhereIF(input.IsItLeader != null, u => u.IsItLeader == input.IsItLeader)
            .WhereIF(input.IsItSpecialPersonnel != null, u => u.IsItSpecialPersonnel == input.IsItSpecialPersonnel);
        return await query.OrderBuilder(input).ToPageListAsync(input.Page, input.PageSize);


        var entity = await _real_timeData.AsQueryable().ClearFilter().ToListAsync();
        return entity;
    }
    
    /// <summary>
    /// 模糊查询实时数据
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("模糊查询实时数据")]
    [ApiDescriptionSettings(Name = "GatConditionReal_timeData"), HttpPost]
    public async Task<SqlSugarPagedList<Entity.Real_timeData>> GatConditionReal_timeData(Real_timeDataInput input,string name,string PersonnelCardCode, string EntranceExitSigns)
    {
        var query = _real_timeData.AsQueryable()
            .WhereIF(!name.IsNullOrEmpty(), x => x.Name.Contains(name))
            .WhereIF(!PersonnelCardCode.IsNullOrEmpty(), x => x.PersonnelCardCode == PersonnelCardCode)
            .WhereIF(!EntranceExitSigns.IsNullOrEmpty(), x => x.EntranceExitSigns == EntranceExitSigns)

            .WhereIF(!string.IsNullOrWhiteSpace(input.PersonnelCardCode), u => u.PersonnelCardCode.Contains(input.PersonnelCardCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.EntranceExitSigns), u => u.EntranceExitSigns.Contains(input.EntranceExitSigns.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.TimeEnteringCurrentArea), u => u.TimeEnteringCurrentArea.Contains(input.TimeEnteringCurrentArea.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationCode), u => u.BaseStationCode.Contains(input.BaseStationCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.EnterCurrentBaseStationTime), u => u.EnterCurrentBaseStationTime.Contains(input.EnterCurrentBaseStationTime.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.LaborOrganizationMode), u => u.LaborOrganizationMode.Contains(input.LaborOrganizationMode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DistanceFromBaseStation), u => u.DistanceFromBaseStation.Contains(input.DistanceFromBaseStation.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(input.TimeEnteringWell != null, u => u.TimeEnteringWell == input.TimeEnteringWell)
            .WhereIF(input.ExitTime != null, u => u.ExitTime == input.ExitTime)
            .WhereIF(input.RegionalCode != null, u => u.RegionalCode == input.RegionalCode)
            .WhereIF(input.PersonnelWorkStatus != null, u => u.PersonnelWorkStatus == input.PersonnelWorkStatus)
            .WhereIF(input.IsItLeader != null, u => u.IsItLeader == input.IsItLeader)
            .WhereIF(input.IsItSpecialPersonnel != null, u => u.IsItSpecialPersonnel == input.IsItSpecialPersonnel);
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);

        //var entity = await _real_timeData.AsQueryable()
        //    .WhereIF(!name.IsNullOrEmpty(), x => x.Name == name || x.PersonnelCardCode == PersonnelCardCode || x.EntranceExitSigns == EntranceExitSigns)
        //    .ClearFilter()
        //    .ToListAsync();
        //return entity;
    }

    /// <summary>
    /// 修改实时数据
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("修改实时数据")]
    [ApiDescriptionSettings(Name = "UpdateReal_timeData"), HttpPost]
    public async Task UpdateReal_timeData(UpdateReal_timeDataInput input)
    {
        //修改部分字段
        // var entity = await _baseStationInformation.AsQueryable().FirstAsync(u => u.Id == input.Id);
        // entity.BaseStationCode = input.BaseStationCode;

        //修改全部字段
        var entity = input.Adapt<Entity.Real_timeData>();
        await _real_timeData.AsUpdateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除实时数据
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("删除实时数据")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteReal_timeDataInput input)
    {
        var entity = await _real_timeData.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _real_timeData.FakeDeleteAsync(entity);   //假删除
        //await _baseStationInformation.DeleteAsync(u => u.Id == id);  //真删除
    }

    /// <summary>
    /// 批量删除实时数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("批量删除实时数据")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DeleteReal_timeDataInput> input)
    {
        var exp = Expressionable.Create<Entity.Real_timeData>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _real_timeData.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _real_timeData.FakeDeleteAsync(list);   //假删除
        //return await _leadershipplanuserRep.DeleteAsync(list);   //真删除
    }
}
