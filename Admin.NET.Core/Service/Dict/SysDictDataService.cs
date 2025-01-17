// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using DocumentFormat.OpenXml;

namespace Admin.NET.Core.Service;

/// <summary>
/// 系统字典值服务 🧩
/// </summary>
    [AllowAnonymous]
[ApiDescriptionSettings(Order = 420)]
public class SysDictDataService : IDynamicApiController, ITransient
{
    private static readonly SysDictTypeService SysDictTypeService = App.GetService<SysDictTypeService>();
    private readonly SqlSugarRepository<SysDictData> _sysDictDataRep;
    private readonly SysCacheService _sysCacheService;
    private readonly UserManager _userManager;

    public SysDictDataService(
        SqlSugarRepository<SysDictData> sysDictDataRep,
        SysCacheService sysCacheService,
        UserManager userManager)
    {
        _userManager = userManager;
        _sysDictDataRep = sysDictDataRep;
        _sysCacheService = sysCacheService;
    }

    /// <summary>
    /// 获取字典值分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取字典值分页列表")]
    public async Task<SqlSugarPagedList<SysDictData>> Page(PageDictDataInput input, StatusEnum? state, string? value)
    {
        var ids = GetTenantIdList();
        return await _sysDictDataRep.AsQueryable()
            .Where(u => u.DictTypeId == input.DictTypeId)
            .WhereIF(!_userManager.SuperAdmin, u => ids.Contains(u.TenantId.Value))
            .WhereIF(!string.IsNullOrEmpty(input.Label?.Trim()), u => u.Value.Contains(input.Label))
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!value.IsNullOrEmpty(), u => u.Value.Contains(value))
            .WhereIF(!state.IsNullOrEmpty(), u => u.Status == state)
            .OrderBy(u => new { u.OrderNo, Code = u.Value })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    [AllowAnonymous]
    [DisplayName("获取字典值分页列表")]
    public async Task<SqlSugarPagedList<SysDictData>> Page(PageDictDataInput input)
    {
        var ids = GetTenantIdList();
        return await _sysDictDataRep.AsQueryable()
            .Where(u => u.DictTypeId == input.DictTypeId)
            .WhereIF(!_userManager.SuperAdmin, u => ids.Contains(u.TenantId.Value))
            .WhereIF(!string.IsNullOrEmpty(input.Label?.Trim()), u => u.Value.Contains(input.Label))
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .OrderBy(u => new { u.OrderNo, Code = u.Value })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取字典值列表 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取字典值列表")]
    public async Task<List<SysDictData>> GetList([FromQuery] GetDataDictDataInput input)
    {
        return await GetDictDataListByDictTypeId(input.DictTypeId);
    }

    /// <summary>
    /// 增加字典值 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加字典值")]
    public async Task AddDictData(AddDictDataInput input)
    {
        var isExist = await _sysDictDataRep.AsQueryable().ClearFilter().AnyAsync(u => u.Value == input.Value && u.DictTypeId == input.DictTypeId);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D3003);

        var dictTypeCode = await _sysDictDataRep.AsQueryable().ClearFilter().Where(u => u.DictTypeId == input.DictTypeId).Select(u => u.DictType.Code).FirstAsync();
        _sysCacheService.Remove($"{CacheConst.KeyDict}{dictTypeCode}");

        await _sysDictDataRep.InsertAsync(input.Adapt<SysDictData>());
    }

    /// <summary>
    /// 更新字典值 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新字典值")]
    public async Task UpdateDictData(UpdateDictDataInput input)
    {
        var isExist = await _sysDictDataRep.AsQueryable().ClearFilter().AnyAsync(u => u.Id == input.Id);
        if (!isExist) throw Oops.Oh(ErrorCodeEnum.D3004);

        isExist = await _sysDictDataRep.AsQueryable().ClearFilter().AnyAsync(u => u.Value == input.Value && u.DictTypeId == input.DictTypeId && u.Id != input.Id);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D3003);

        var dictTypeCode = await _sysDictDataRep.AsQueryable().ClearFilter().Where(u => u.DictTypeId == input.DictTypeId).Select(u => u.DictType.Code).FirstAsync();
        _sysCacheService.Remove($"{CacheConst.KeyDict}{dictTypeCode}");

        await _sysDictDataRep.UpdateAsync(input.Adapt<SysDictData>());
    }

    /// <summary>
    /// 删除字典值 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除字典值")]
    public async Task DeleteDictData(DeleteDictDataInput input)
    {
        var dictData = await GetSysDictDataQueryable().FirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D3004);

        var dictTypeCode = await GetSysDictDataQueryable().Where(u => u.DictTypeId == dictData.Id).Select(u => u.DictType.Code).FirstAsync();
        _sysCacheService.Remove($"{CacheConst.KeyDict}{dictTypeCode}");

        await _sysDictDataRep.DeleteAsync(dictData);
    }

    /// <summary>
    /// 获取字典值详情 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取字典值详情")]
    public async Task<SysDictData> GetDetail([FromQuery] DictDataInput input)
    {
        return await _sysDictDataRep.GetByIdAsync(input.Id);
    }

    /// <summary>
    /// 修改字典值状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [UnitOfWork]
    [DisplayName("修改字典值状态")]
    public async Task SetStatus(DictDataInput input)
    {
        var dictData = await _sysDictDataRep.AsQueryable().FirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D3004);

        var dictTypeCode = await _sysDictDataRep.AsQueryable().ClearFilter().Where(u => u.DictTypeId == dictData.Id).Select(u => u.DictType.Code).FirstAsync();
        _sysCacheService.Remove($"{CacheConst.KeyDict}{dictTypeCode}");

        dictData.Status = input.Status;
        await _sysDictDataRep.AsUpdateable(dictData).UpdateColumns(u => new { u.Status }, true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据字典类型Id获取字典值集合
    /// </summary>
    /// <param name="dictTypeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [NonAction]
    public async Task<List<SysDictData>> GetDictDataListByDictTypeId(long dictTypeId)
    {
        var dictType = await SysDictTypeService.GetDetail(new DictTypeInput { Id = dictTypeId });
        var dictDataList = _sysCacheService.Get<List<SysDictData>>($"{CacheConst.KeyDict}{dictTypeId}");

        if (dictDataList == null)
        {
            dictDataList = await GetSysDictDataQueryable().Where(u => u.DictTypeId == dictTypeId).OrderBy(u => new { u.OrderNo, Code = u.Value }).ToListAsync();
            _sysCacheService.Set($"{CacheConst.KeyDict}{dictType.Code}", dictDataList);
        }
        return dictDataList;
    }

    /// <summary>
    /// 根据字典类型编码获取字典值集合 🔖
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("根据字典类型编码获取字典值集合")]
    public async Task<List<SysDictData>> GetDataList(string code)
    {
        var dictDataList = _sysCacheService.Get<List<SysDictData>>($"{CacheConst.KeyDict}{code}");
        if (dictDataList == null)
        {
            var ids = GetTenantIdList();
            dictDataList = await _sysDictDataRep.Context.Queryable<SysDictType>().ClearFilter()
                .LeftJoin<SysDictData>((u, a) => u.Id == a.DictTypeId)
                .Where((u, a) => u.Code == code && u.Status == StatusEnum.Enable && a.Status == StatusEnum.Enable)
                .WhereIF(!_userManager.SuperAdmin, (u, a) => ids.Contains(u.TenantId.Value) && ids.Contains(a.TenantId.Value))
                .OrderBy((u, a) => new { a.OrderNo, Code = a.Value })
                .Select((u, a) => a).ToListAsync();
            _sysCacheService.Set($"{CacheConst.KeyDict}{code}", dictDataList);
        }
        return dictDataList;
    }

    /// <summary>
    /// 根据查询条件获取字典值集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("根据查询条件获取字典值集合")]
    public async Task<List<SysDictData>> GetDataList([FromQuery] QueryDictDataInput input)
    {
        var ids = GetTenantIdList();
        return await _sysDictDataRep.Context.Queryable<SysDictType>()
            .LeftJoin<SysDictData>((u, a) => u.Id == a.DictTypeId)
            .Where((u, a) => u.Code == input.Code)
            .WhereIF(input.Status.HasValue, (u, a) => a.Status == (StatusEnum)input.Status.Value)
            .WhereIF(!_userManager.SuperAdmin, (u, a) => ids.Contains(u.TenantId.Value) && ids.Contains(a.TenantId.Value))
            .OrderBy((u, a) => new { a.OrderNo, Code = a.Value })
            .Select((u, a) => a).ToListAsync();
    }

    /// <summary>
    /// 获取租户Id列表
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [NonAction]
    public List<long> GetTenantIdList() => SysDictTypeService.GetTenantIdList();

    /// <summary>
    /// 获取SysDictData表查询实例
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [NonAction]
    public ISugarQueryable<SysDictData> GetSysDictDataQueryable()
    {
        var ids = GetTenantIdList();
        return _sysDictDataRep.AsQueryable().ClearFilter().WhereIF(!_userManager.SuperAdmin, u => ids.Contains(u.TenantId.Value));
    }

    /// <summary>
    /// 根据字典类型Id删除字典值
    /// </summary>
    /// <param name="dictTypeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [NonAction]
    public async Task DeleteDictData(long dictTypeId)
    {
        var dictTypeCode = await _sysDictDataRep.AsQueryable().Where(u => u.DictTypeId == dictTypeId).Select(u => u.DictType.Code).FirstAsync();
        _sysCacheService.Remove($"{CacheConst.KeyDict}{dictTypeCode}");

        await _sysDictDataRep.DeleteAsync(u => u.DictTypeId == dictTypeId);
    }
}