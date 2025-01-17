// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.BaseStationInformation.Dto;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.BaseStationInformation;
/// <summary>
/// 基站信息服务 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class BaseStationInformationService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.BaseStationInformation> _baseStationInformation;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<UpdateBaseStationInformationInput> _updateBaseStationInformationInput;
    private readonly SqlSugarRepository<BaseStationInformationInput> _baseStationInformationInput;
    private readonly SqlSugarRepository<BaseStationInformationDto> _baseStationInformationdto;

    public BaseStationInformationService(
        SqlSugarRepository<Entity.BaseStationInformation> baseStationInformation,
        ISqlSugarClient sqlSugarClient,
        SqlSugarRepository<UpdateBaseStationInformationInput> updateBaseStationInformationInput,
        SqlSugarRepository<BaseStationInformationInput> baseStationInformationInput,
        SqlSugarRepository<BaseStationInformationDto> baseStationInformationdto)
    {
        _baseStationInformation = baseStationInformation;
        _sqlSugarClient = sqlSugarClient;
        _updateBaseStationInformationInput = updateBaseStationInformationInput;
        _baseStationInformationInput = baseStationInformationInput;
        _baseStationInformationdto = baseStationInformationdto;
    }

    /// <summary>
    /// 添加基站信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("添加基站信息")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(BaseStationInformationInput input)
    {
        var e = await _baseStationInformation.AsQueryable().ClearFilter().ToListAsync();
        int count = e.Count+1;

        var entity = input.Adapt<Entity.BaseStationInformation>();
        entity.Id = count;
        entity.BaseStationCode = input.BaseStationCode;
        entity.BaseStationName = input.BaseStationName;
        entity.X_Coordinate = input.X_Coordinate;
        entity.Y_Coordinate = input.Y_Coordinate;
        entity.Z_Coordinate = input.Z_Coordinate;
        entity.LocationAnnotation = input.LocationAnnotation;
        await _baseStationInformation.InsertAsync(entity);
    }

    /// <summary>
    /// 查询全部基站信息
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询全部基站信息")]
    [ApiDescriptionSettings(Name = "GetBaseStationInformation"), HttpPost]
    public async Task<List<Entity.BaseStationInformation>> GetBaseStationInformation(BaseStationInformationInputlist input)
    {
        var query = _baseStationInformation.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationCode), u => u.BaseStationCode.Contains(input.BaseStationCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationName), u => u.BaseStationName.Contains(input.BaseStationName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.X_Coordinate), u => u.X_Coordinate.Contains(input.X_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Y_Coordinate), u => u.Y_Coordinate.Contains(input.Y_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Z_Coordinate), u => u.Z_Coordinate.Contains(input.Z_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.LocationAnnotation), u => u.LocationAnnotation.Contains(input.LocationAnnotation.Trim()))
            .Select<Entity.BaseStationInformation>();
        return await query.OrderBuilder(input).ToPageListAsync(input.Page, input.PageSize);

        var entity = await _baseStationInformation.AsQueryable().ClearFilter().ToListAsync();
        return entity;
    }


    /// <summary>
    /// 模糊查询基站信息
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("模糊查询基站信息")]
    [ApiDescriptionSettings(Name = "GetConditionBaseStationInformation"), HttpPost]
    public async Task<SqlSugarPagedList<Entity.BaseStationInformation>> GetConditionBaseStationInformation(BaseStationInformationInputlist input,string Name,string BaseStationCode ,string x)
    {
        var query = _baseStationInformation.AsQueryable()
            .WhereIF(!Name.IsNullOrEmpty(), u => u.BaseStationName.Contains(Name))
            .WhereIF(!BaseStationCode.IsNullOrEmpty(), u => u.BaseStationCode.Contains(BaseStationCode))
            .WhereIF(!x.IsNullOrEmpty(), u => u.X_Coordinate.Contains(x))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationCode), u => u.BaseStationCode.Contains(input.BaseStationCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.BaseStationName), u => u.BaseStationName.Contains(input.BaseStationName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.X_Coordinate), u => u.X_Coordinate.Contains(input.X_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Y_Coordinate), u => u.Y_Coordinate.Contains(input.Y_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Z_Coordinate), u => u.Z_Coordinate.Contains(input.Z_Coordinate.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.LocationAnnotation), u => u.LocationAnnotation.Contains(input.LocationAnnotation.Trim()))
            .Select<Entity.BaseStationInformation>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);

        //var entity = await _baseStationInformation.AsQueryable().ClearFilter()
        //    .WhereIF(!Name.IsNullOrEmpty(), u => u.BaseStationName ==Name || u.BaseStationCode == BaseStationCode || u.X_Coordinate == x)
        //    .ToListAsync();
        //return entity;
    }

    /// <summary>
    /// 修改基站信息
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("修改基站信息")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateBaseStationInformationInput input)
    {
        //修改全部字段
        var entity = input.Adapt<Entity.BaseStationInformation>();
        await _baseStationInformation.AsUpdateable(entity)
        .Where(it => it.Id == entity.Id)
        .ExecuteCommandAsync();
    }








}
