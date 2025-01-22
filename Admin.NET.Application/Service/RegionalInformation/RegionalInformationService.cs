// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.RegionalInformation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.RegionalInformation;
/// <summary>
/// 区域信息服务 🧩
/// </summary>
// [AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class RegionalInformationService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.RegionalInformation> _regionalInformation;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<RegionalInformationDto> _regionalInformationDto;
    private readonly SqlSugarRepository<UpdateRegionalInformationInput> _updateRegionalInformationInput;
    private readonly SqlSugarRepository<DelectRegionalInformationInput> _delectRegionalInformationInput;
    private readonly SqlSugarRepository<RegionalInformationInput> _regionalInformationInput;

    public RegionalInformationService(
        SqlSugarRepository<Entity.RegionalInformation> regionalInformation,
        ISqlSugarClient sqlSugarClient,
        SqlSugarRepository<RegionalInformationDto> regionalInformationDto,
        SqlSugarRepository<UpdateRegionalInformationInput> updateRegionalInformationInput,
        SqlSugarRepository<DelectRegionalInformationInput> delectRegionalInformationInput,
        SqlSugarRepository<RegionalInformationInput> regionalInformationInput)
    {
        _regionalInformation = regionalInformation; 
        _sqlSugarClient = sqlSugarClient;
        _regionalInformationDto = regionalInformationDto;
        _updateRegionalInformationInput = updateRegionalInformationInput;
        _delectRegionalInformationInput = delectRegionalInformationInput;
        _regionalInformationInput = regionalInformationInput;
    }

    #region 添加区域信息
    /// <summary>
    /// 添加区域信息
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("添加区域信息")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(RegionalInformationInput input)
    {
        try
        {
            var entity = input.Adapt<Entity.RegionalInformation>();
            entity.RegionalType = input.RegionalType;
            entity.RegionalCode = input.RegionalCode;
            entity.AuthorizedPersonnel = input.AuthorizedPersonnel;
            entity.Country = input.Country;
            await _regionalInformation.InsertAsync(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    #endregion

    #region 查询全部区域信息
    /// <summary>
    /// 查询全部区域信息
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询全部区域信息")]
    [ApiDescriptionSettings(Name = "GetRegionalInformation"), HttpPost]
    public async Task<List<Entity.RegionalInformation>> GetRegionalInformation(RegionalInformationInput input)
    {
        var query = _regionalInformation.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.RegionalType), u => u.RegionalType.Contains(input.RegionalType.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.RegionalCode), u => u.RegionalCode.Contains(input.RegionalCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Country), u => u.Country.Contains(input.Country.Trim()))
            .WhereIF(input.AuthorizedPersonnel != null, u => u.AuthorizedPersonnel == input.AuthorizedPersonnel);
        return await query.OrderBuilder(input).ToPageListAsync(input.Page, input.PageSize);

        //var entity = await _regionalInformation.AsQueryable().ClearFilter().ToListAsync();
        //return entity;
    }
    
    #endregion
    
    #region 模糊查询区域信息
    /// <summary>
    /// 模糊查询区域信息
    /// </summary>
    /// <param name="input"></param>
    /// <param name="name"></param>
    /// <param name="regionalType"></param>
    /// <param name="regionalCode"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("模糊查询区域信息")]
    [ApiDescriptionSettings(Name = "GetConditionRegionalInformation"), HttpPost]
    public async Task<SqlSugarPagedList<Entity.RegionalInformation>> GetConditionRegionalInformation(RegionalInformationInput input, string name,string regionalType,string regionalCode)
    {
        var query = _regionalInformation.AsQueryable()
            .WhereIF(!string.IsNullOrEmpty(name), u => u.Country.Contains(name))
            .WhereIF(!string.IsNullOrEmpty(regionalType), u => u.RegionalType.Contains(regionalType))
            .WhereIF(!string.IsNullOrEmpty(regionalCode), u => u.RegionalCode.Contains(regionalCode))
            .WhereIF(!string.IsNullOrWhiteSpace(input.RegionalType), u => u.RegionalType.Contains(input.RegionalType.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.RegionalCode), u => u.RegionalCode.Contains(input.RegionalCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Country), u => u.Country.Contains(input.Country.Trim()))
            .WhereIF(input.AuthorizedPersonnel != null, u => u.AuthorizedPersonnel == input.AuthorizedPersonnel);
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }
    
    #endregion

    #region 修改区域信息
    /// <summary>
    /// 修改区域信息
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("修改区域信息")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateRegionalInformationInput input)
    {
        try
        {
            //修改部分字段
            // var entity = await _regionalInformation.AsQueryable().FirstAsync(u => u.Id == input.Id);
            // entity.BaseStationCode = input.BaseStationCode;
            //修改全部字段
            var entity = input.Adapt<Entity.RegionalInformation>();
            await _regionalInformation.AsUpdateable(entity)
                .ExecuteCommandAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion
    
    #region 删除区域信息
    /// <summary>
    /// 删除区域信息
    /// </summary>
    // [AllowAnonymous]
    [DisplayName("删除区域信息")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DelectRegionalInformationInput input)
    {
        try
        {
            var entity = await _regionalInformation.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
            await _regionalInformation.FakeDeleteAsync(entity);   //假删除
            //await _baseStationInformation.DeleteAsync(u => u.Id == id);  //真删除
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    #endregion

    #region 批量删除区域信息
    /// <summary>
    /// 批量删除区域信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("批量删除区域信息")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DelectRegionalInformationInput> input)
    {
        try
        {
            var exp = Expressionable.Create<Entity.RegionalInformation>();
            foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
            var list = await _regionalInformation.AsQueryable().Where(exp.ToExpression()).ToListAsync();
            return await _regionalInformation.FakeDeleteAsync(list);   //假删除
            //return await _leadershipplanuserRep.DeleteAsync(list);   //真删除
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    #endregion




}
