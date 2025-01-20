// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Service.BaseStationInformation.Dto;
using Admin.NET.Application.Service.Problemcentered.Dto;
using Aop.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using Aliyun.OSS.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnceMi.AspNetCore.OSS;
using Yitter.IdGenerator;
using Admin.NET.Application.Service.Problemcentered.Dto;
namespace Admin.NET.Application.Service.Problemcentered;
/// <summary>
/// 问题中心服务
/// </summary>
[AllowAnonymous]
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class ProblemcenteredService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Entity.Problemcentered> _problemcenteredRepository;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly SqlSugarRepository<ProblemcenteredInput> _problemcenteredInput;
    private readonly SqlSugarRepository<ProblemcenteredDto> _problemcenteredDto;
    private readonly SqlSugarRepository<DelectBaseStationInformationInput> _delectBaseStationInformationInput;
    private readonly SqlSugarRepository<UpdateBaseStationInformationInput> _updateBaseStationInformationInput;
    private readonly SqlSugarRepository<Entity.LikeList> _likeListRepository;
    private readonly SqlSugarRepository<SysUser> _userRep;
    private readonly SqlSugarRepository<SysOrg> _orgRep;
    

    private readonly OSSProviderOptions _OSSProviderOptions;
    private readonly UploadOptions _uploadOptions;

    private readonly IOSSService _OSSService;
    private readonly SqlSugarRepository<SysFile> _sysFileRep;
    private readonly string _imageType = ".jpeg.jpg.png.bmp.gif.tif";

    #region 实例化
    public ProblemcenteredService(
        SqlSugarRepository<Entity.Problemcentered> problemcenteredRepository,
        ISqlSugarClient sqlSugarClient, 
        SqlSugarRepository<ProblemcenteredInput> problemcenteredInput, 
        SqlSugarRepository<ProblemcenteredDto> problemcenteredDto, 
        SqlSugarRepository<UpdateBaseStationInformationInput> updateBaseStationInformationInput, 
        SqlSugarRepository<DelectBaseStationInformationInput> delectBaseStationInformationInput,
        SqlSugarRepository<SysUser> userRep, SqlSugarRepository<SysOrg> orgRep,
        //UploadOptions uploadOptions
        IOptions<UploadOptions> uploadOptions, SqlSugarRepository<SysFile> sysFileRep,
        SqlSugarRepository<Entity.LikeList> likeListRepository
        )
    {
        _problemcenteredRepository = problemcenteredRepository;
        _sqlSugarClient = sqlSugarClient;
        _problemcenteredInput = problemcenteredInput;
        _problemcenteredDto = problemcenteredDto;
        _updateBaseStationInformationInput = updateBaseStationInformationInput;
        _delectBaseStationInformationInput = delectBaseStationInformationInput;
        _userRep = userRep;
        _orgRep = orgRep;
        _uploadOptions = uploadOptions.Value;
        _sysFileRep = sysFileRep;
        _likeListRepository = likeListRepository;
    }
    #endregion

    #region 查询问题中心列表
    /// <summary>
    /// 查询问题中心列表
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询问题中心列表")]
    [ApiDescriptionSettings(Name = "GetProblemcentered"), HttpGet]
    public async Task<List<Entity.Problemcentered>> GetProblemcentered ()
    {
        var entity = await _problemcenteredRepository.AsQueryable()
            .ClearFilter().ToListAsync();
        return entity;
    }

    #endregion
    
    #region 查询问题中心列表主键
    /// <summary>
    /// 查询问题中心列表主键
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("查询问题中心列表主键")]
    [ApiDescriptionSettings(Name = "ProblemcenteredId"), HttpGet]
    public async Task<List<Entity.Problemcentered>> ProblemcenteredId(long id)
    {
        var entity = await _problemcenteredRepository.AsQueryable()
            .Where(u => u.Id == id)
            .ClearFilter().ToListAsync();
        return entity;
    }
    #endregion
    
    #region 查看是否点赞
    /// <summary>
    /// 查看是否点赞
    /// </summary>
    /// <param name="input"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [DisplayName("查看是否点赞")]
    [ApiDescriptionSettings(Name = "GetAllProblemcentered"), HttpPost]
    public async Task<SqlSugarPagedList<ProblemcenteredInput>> GetAllProblemcentered(ProblemcenteredDto input, long userId)
    {
        try
        {
            // 获取所有的 Problemcentered 数据
            var query = _problemcenteredRepository.AsQueryable().Select<Entity.Problemcentered>();

            // 获取当前用户的所有点赞的 ProblemcenteredId
            var likedProblemIds = await _likeListRepository.AsQueryable()
                .Where(l => l.UserId == userId)//查询当前用户点赞的记录
                .Select(l => l.ProblemId)//获取全部
                .ToListAsync(); //获取点赞的 ProblemcenteredId 列表

            // 获取查询的结果，并映射到 ProblemcenteredInput
            var problemcenteredInputs = await query
                .OrderBuilder(input)
                .Select(it => new ProblemcenteredInput
                {
                    Id = it.Id,
                    PlanId = it.PlanId,
                    PlaceName = it.PlaceName,
                    PlaceId = it.PlaceId,
                    PlanName = it.PlanName,
                    UserId = it.UserId,
                    UserName = it.UserName,
                    ReportContent = it.ReportContent,
                    Source = it.Source,
                    UserDeptId = it.UserDeptId,
                    UserDeptName = it.UserDeptName,
                    ReportImg = it.ReportImg,
                    ReportVideo = it.ReportVideo,
                    ReportMp3 = it.ReportMp3,
                    ReportTime = it.ReportTime,
                    Status = it.Status,
                    HandleUserId = it.HandleUserId,
                    HandleUserName = it.HandleUserName,
                    HandleDeptId = it.HandleDeptId,
                    HandleDeptName = it.HandleDeptName,
                    HandleContent = it.HandleContent,
                    HandleImg = it.HandleImg,
                    HandleVideo = it.HandleVideo,
                    HandleMp3 = it.HandleMp3,
                    HandleTime = it.HandleTime,
                    GiveUpCount = it.GiveUpCount,
                    Like =  likedProblemIds.Contains(it.Id) // 检查当前 id 是否在点赞列表中
                })
                .ToPagedListAsync(input.Page, input.PageSize);
            return problemcenteredInputs;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region 模糊查询问题中心列表
    /// <summary>
    /// 模糊查询问题中心列表
    /// </summary>
    /// <returns></returns>
    // [AllowAnonymous]
    [DisplayName("模糊查询问题中心列表")]
    [ApiDescriptionSettings(Name = "GetConditionProblemcentered"), HttpPost]
    public async Task<SqlSugarPagedList<Entity.Problemcentered>> GetConditionProblemcentered(ProblemcenteredDto input, string? name,string? handleUserName,string? status)
    {
        try
        { 
            var query = _problemcenteredRepository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.PlanName), u => u.PlanName.Contains(input.PlanName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.PlaceName), u => u.PlaceName.Contains(input.PlaceName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ReportContent), u => u.ReportContent.Contains(input.ReportContent.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Source), u => u.Source.Contains(input.Source.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UserDeptName), u => u.UserDeptName.Contains(input.UserDeptName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ReportImg), u => u.ReportImg.Contains(input.ReportImg.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ReportVideo), u => u.ReportVideo.Contains(input.ReportVideo.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ReportMp3), u => u.ReportMp3.Contains(input.ReportMp3.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Status), u => u.Status.Contains(input.Status.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleUserName), u => u.HandleUserName.Contains(input.HandleUserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleDeptName), u => u.HandleDeptName.Contains(input.HandleDeptName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleContent), u => u.HandleContent.Contains(input.HandleContent.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleImg), u => u.HandleImg.Contains(input.HandleImg.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleVideo), u => u.HandleVideo.Contains(input.HandleVideo.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.HandleMp3), u => u.HandleMp3.Contains(input.HandleMp3.Trim()))
            .WhereIF(input.PlanId != null, u => u.PlanId == input.PlanId)
            .WhereIF(input.PlaceId != null, u => u.PlaceId == input.PlaceId)
            .WhereIF(input.UserId != null, u => u.UserId == input.UserId)
            .WhereIF(input.UserDeptId != null, u => u.UserDeptId == input.UserDeptId)
            .WhereIF(input.ReportTime != null, u => u.ReportTime == input.ReportTime)
            .WhereIF(input.HandleUserId != null, u => u.HandleUserId == input.HandleUserId)
            .WhereIF(input.HandleDeptId != null, u => u.HandleDeptId == input.HandleDeptId)
            .WhereIF(input.HandleTime != null, u => u.HandleTime == input.HandleTime)
            .WhereIF(input.GiveUpCount != null, u => u.GiveUpCount == input.GiveUpCount)
            .WhereIF(!name.IsNullOrEmpty(), u =>
                u.PlanName.Contains(name) ||
                u.PlaceName.Contains(name) ||
                u.UserName.Contains(name))
            .WhereIF(!string.IsNullOrEmpty(handleUserName), u => u.HandleUserName.Contains(handleUserName))
            .WhereIF(!string.IsNullOrEmpty(status), u => u.Status.Contains(status)) // 假设 Status 是字段名
                                                                                    //.Where(u => u.ReportTime >= dateTime.ReportTimeMinimum && u.ReportTime <= dateTime.ReportTimeMax) // 筛选报送时间
            .Where(u =>
                (input.ReportTimeMinimum == null && input.ReportTimeMax == null) ||
                (u.ReportTime >= input.ReportTimeMinimum && u.ReportTime == null) ||
                (u.ReportTime == null && u.ReportTime <= input.ReportTimeMax) ||
                (u.ReportTime >= input.ReportTimeMinimum && u.ReportTime <= input.ReportTimeMax)
                )
            .Where(u =>
                (input.HandleTimeMinimum == null && input.HandleTimeMax == null) ||
                (u.HandleTime >= input.HandleTimeMinimum && u.HandleTime == null) ||
                (u.HandleTime == null && u.HandleTime <= input.HandleTimeMax) ||
                (u.HandleTime >= input.HandleTimeMinimum && u.HandleTime <= input  .HandleTimeMax)
                );

        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        //var entity = await _problemcenteredRepository.AsQueryable()
        //    .ToListAsync();
        //return entity.Adapt<List<Entity.Problemcentered>>();
    }
    #endregion
    
    #region 增加问题中心
    /// <summary>
    /// 增加问题中心 ➕
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("增加问题中心")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task Add(ProblemcenteredInput input)
    {
        try
        {
            var entity = input.Adapt<Entity.Problemcentered>();
            //var repUser = await _userRep.AsQueryable().ClearFilter().Where(x => x.Id == entity.UserId).FirstAsync();
            //var repUserDept = await _orgRep.AsQueryable().ClearFilter().Where(x => x.Id == repUser.OrgId).FirstAsync();
            //entity.UserName = repUser == null ? "" : repUser.RealName;
            //entity.UserDeptName = repUserDept == null ? "" : repUserDept.Name;
            //entity.ReportTime = DateTime.Now;
            //entity.Status = "待派单";
            //entity.GiveUpCount = 0;
            //return await _problemcenteredRepository.InsertAsync(entity) ? entity.Id : 0;
            entity.PlanId = input.PlanId;
            entity.PlanName = input.PlanName;
            entity.PlaceId = input.PlaceId;
            entity.PlaceName = input.PlaceName;
            entity.UserId = input.UserId;
            entity.UserName = input.UserName;
            entity.ReportContent = input.ReportContent;
            entity.Source = input.Source;
            entity.UserDeptId = input.UserDeptId;
            entity.UserDeptName = input.UserDeptName;
            entity.ReportImg = input.ReportImg;
            entity.ReportVideo = input.ReportVideo;
            entity.ReportMp3 = input.ReportMp3;
            entity.ReportTime = DateTime.Now;
            entity.Status = "待派单";
            entity.HandleUserId = input.HandleUserId;
            entity.HandleUserName = input.HandleUserName;
            entity.HandleDeptId = input.HandleDeptId;
            entity.HandleDeptName = input.HandleDeptName;
            entity.HandleContent = input.HandleContent;
            entity.HandleImg = input.HandleImg;
            entity.HandleVideo = input.HandleVideo;
            entity.HandleMp3 = input.HandleMp3;
            entity.HandleTime = input.HandleTime;
            entity.GiveUpCount = 0;
            await _problemcenteredRepository.InsertAsync(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    #endregion

    #region 修改问题中心内容
    /// <summary>
    /// 修改问题中心内容
    /// </summary>
    /// <param name="input"></param>
    // [AllowAnonymous]
    [DisplayName("修改问题中心内容")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task UpdateProblemcentered(UpdateProblemcentered input)
    {
        try
        {
            var entity = input.Adapt<Entity.Problemcentered>();
            await _problemcenteredRepository.AsUpdateable(entity)
                .Where(u => u.Id == entity.Id)
                .ExecuteCommandAsync() ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        //修改全部字段
        
    }
    #endregion

    #region 上传文件
    /// <summary>
    /// 上传文件 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("上传文件")]
    public async Task<SysFile> UploadFile([FromForm] UploadFileInput input)
    {
        if (input.File == null) throw Oops.Oh(ErrorCodeEnum.D8000);

        // 判断是否重复上传的文件
        var sizeKb = input.File.Length / 1024; // 大小KB
        var fileMd5 = string.Empty;
        if (_uploadOptions.EnableMd5)
        {
            await using (var fileStream = input.File.OpenReadStream())
            {
                fileMd5 = OssUtils.ComputeContentMd5(fileStream, fileStream.Length);
            }
            // Mysql8 中如果使用了 utf8mb4_general_ci 之外的编码会出错，尽量避免在条件里使用.ToString()
            // 因为 Squsugar 并不是把变量转换为字符串来构造SQL语句，而是构造了CAST(123 AS CHAR)这样的语句，这样这个返回值是utf8mb4_general_ci，所以容易出错。
            var sysFile = await _sysFileRep.GetFirstAsync(u => u.FileMd5 == fileMd5 && u.SizeKb == sizeKb);
            if (sysFile != null) return sysFile;
        }

        // 验证文件类型
        // if (!_uploadOptions.ContentType.Contains(input.File.ContentType)) throw Oops.Oh($"{ErrorCodeEnum.D8001}:{input.File.ContentType}");

        // 验证文件大小
        if (sizeKb > _uploadOptions.MaxSize) throw Oops.Oh($"{ErrorCodeEnum.D8002}，允许最大：{_uploadOptions.MaxSize}KB");

        // 获取文件后缀
        var suffix = Path.GetExtension(input.File.FileName).ToLower(); // 后缀
        if (string.IsNullOrWhiteSpace(suffix))
            suffix = string.Concat(".", input.File.ContentType.AsSpan(input.File.ContentType.LastIndexOf('/') + 1));
       
        if (string.IsNullOrWhiteSpace(suffix)) throw Oops.Oh(ErrorCodeEnum.D8003);

        // 防止客户端伪造文件类型
        if (!string.IsNullOrWhiteSpace(input.AllowSuffix) && !input.AllowSuffix.Contains(suffix)) throw Oops.Oh(ErrorCodeEnum.D8003);
        //if (!VerifyFileExtensionName.IsSameType(file.OpenReadStream(), suffix))
        //    throw Oops.Oh(ErrorCodeEnum.D8001);

        // 文件存储位置
        var path = string.IsNullOrWhiteSpace(input.SavePath) ? _uploadOptions.Path : input.SavePath;
        path = path.ParseToDateTimeForRep();

        var newFile = input.Adapt<SysFile>();
        newFile.Id = YitIdHelper.NextId();
        newFile.BucketName = "Local";// 阿里云对bucket名称有要求，1.只能包括小写字母，数字，短横线（-）2.必须以小写字母或者数字开头  3.长度必须在3-63字节之间
        newFile.FileName = Path.GetFileNameWithoutExtension(input.File.FileName);
        newFile.Suffix = suffix;
        newFile.SizeKb = sizeKb;
        newFile.FilePath = path;
        newFile.FileMd5 = fileMd5;

        var finalName = newFile.Id + suffix; // 文件最终名称
       
            newFile.Provider = ""; // 本地存储 Provider 显示为空
            var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var realFile = Path.Combine(filePath, finalName);
            await using (var stream = File.Create(realFile))
            {
                await input.File.CopyToAsync(stream);
            }

            newFile.Url = $"{newFile.FilePath}/{newFile.Id + newFile.Suffix}";
        await _sysFileRep.AsInsertable(newFile).ExecuteCommandAsync();
        return newFile;
    }
    #endregion
}
