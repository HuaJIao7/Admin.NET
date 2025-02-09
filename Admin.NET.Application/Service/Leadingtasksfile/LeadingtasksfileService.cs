// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Admin.NET.Core.Service;
using Aliyun.OSS.Util;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnceMi.AspNetCore.OSS;
using Yitter.IdGenerator;

namespace Admin.NET.Application;

/// <summary>
/// 带班任务上报文件服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class LeadingtasksfileService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Leadingtasksfile> _leadingtasksfileRep;
    private readonly ISqlSugarClient _sqlSugarClient;
    private readonly UploadOptions _uploadOptions;
    private readonly SqlSugarRepository<SysFile> _sysFileRep;
    private readonly OSSProviderOptions _OSSProviderOptions;
    private readonly IOSSService _OSSService;

    public LeadingtasksfileService(SqlSugarRepository<Leadingtasksfile> leadingtasksfileRep, ISqlSugarClient sqlSugarClient, IOptions<UploadOptions> uploadOptions, SqlSugarRepository<SysFile> sysFileRep, IOptions<OSSProviderOptions> oSSProviderOptions,
        IOSSServiceFactory ossServiceFactory)
    {
        _leadingtasksfileRep = leadingtasksfileRep;
        _sqlSugarClient = sqlSugarClient;

        _uploadOptions = uploadOptions.Value;

        _sysFileRep = sysFileRep;
        _OSSProviderOptions = oSSProviderOptions.Value;

        if (_OSSProviderOptions.Enabled)
            _OSSService = ossServiceFactory.Create(Enum.GetName(_OSSProviderOptions.Provider));

    }

    /// <summary>
    /// 分页查询带班任务上报文件 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("分页查询带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<LeadingtasksfileOutput>> Page(PageLeadingtasksfileInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _leadingtasksfileRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Type.Contains(input.Keyword) || u.Url.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Type), u => u.Type.Contains(input.Type.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Url), u => u.Url.Contains(input.Url.Trim()))
            .WhereIF(input.TaskId != null, u => u.TaskId == input.TaskId)
            .Select<LeadingtasksfileOutput>();
		return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取带班任务上报文件详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取带班任务上报文件详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<Leadingtasksfile> Detail([FromQuery] QueryByIdLeadingtasksfileInput input)
    {
        return await _leadingtasksfileRep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加带班任务上报文件 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add([FromForm]AddLeadingtasksfileInput input)
    {

        var AllowSuffix = ".jpeg.jpg.png.bmp.gif.tif";//允许文件上传格式类型 
        var SavePath = "1";


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
            if (sysFile != null)  throw Oops.Oh(ErrorCodeEnum.D1009);
        }

        // 验证文件类型
        if (!_uploadOptions.ContentType.Contains(input.File.ContentType)) throw Oops.Oh($"{ErrorCodeEnum.D8001}:{input.File.ContentType}");

        // 验证文件大小
        if (sizeKb > _uploadOptions.MaxSize) throw Oops.Oh($"{ErrorCodeEnum.D8002}，允许最大：{_uploadOptions.MaxSize}KB");

        // 获取文件后缀
        var suffix = Path.GetExtension(input.File.FileName).ToLower(); // 后缀
        if (string.IsNullOrWhiteSpace(suffix))
            suffix = string.Concat(".", input.File.ContentType.AsSpan(input.File.ContentType.LastIndexOf('/') + 1));
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            //var contentTypeProvider = FS.GetFileExtensionContentTypeProvider();
            //suffix = contentTypeProvider.Mappings.FirstOrDefault(u => u.Value == file.ContentType).Key;
            // 修改 image/jpeg 类型返回的 .jpeg、jpe 后缀
            if (suffix == ".jpeg" || suffix == ".jpe")
                suffix = ".jpg";
        }
        if (string.IsNullOrWhiteSpace(suffix)) throw Oops.Oh(ErrorCodeEnum.D8003);

        // 防止客户端伪造文件类型
        if (!string.IsNullOrWhiteSpace(AllowSuffix) && !AllowSuffix.Contains(suffix)) throw Oops.Oh(ErrorCodeEnum.D8003);
        //if (!VerifyFileExtensionName.IsSameType(file.OpenReadStream(), suffix))
        //    throw Oops.Oh(ErrorCodeEnum.D8001);

        // 文件存储位置
        var path = string.IsNullOrWhiteSpace(SavePath) ? _uploadOptions.Path : SavePath;
        path = path.ParseToDateTimeForRep();

        var newFile = input.Adapt<SysFile>();
        newFile.Id = YitIdHelper.NextId();
        newFile.BucketName = _OSSProviderOptions.Enabled ? _OSSProviderOptions.Bucket : "Local"; // 阿里云对bucket名称有要求，1.只能包括小写字母，数字，短横线（-）2.必须以小写字母或者数字开头  3.长度必须在3-63字节之间
        newFile.FileName = Path.GetFileNameWithoutExtension(input.File.FileName);
        newFile.Suffix = suffix;
        newFile.SizeKb = sizeKb;
        newFile.FilePath = path;
        newFile.FileMd5 = fileMd5;

        var finalName = newFile.Id + suffix; // 文件最终名称
        if (_OSSProviderOptions.Enabled)
        {
            newFile.Provider = Enum.GetName(_OSSProviderOptions.Provider);
            var filePath = string.Concat(path, "/", finalName);
            await _OSSService.PutObjectAsync(newFile.BucketName, filePath, input.File.OpenReadStream());
            //  http://<你的bucket名字>.oss.aliyuncs.com/<你的object名字>
            //  生成外链地址 方便前端预览
            switch (_OSSProviderOptions.Provider)
            {
                case OSSProvider.Aliyun:
                    newFile.Url = $"{(_OSSProviderOptions.IsEnableHttps ? "https" : "http")}://{newFile.BucketName}.{_OSSProviderOptions.Endpoint}/{filePath}";
                    break;

                case OSSProvider.QCloud:
                    newFile.Url = $"{(_OSSProviderOptions.IsEnableHttps ? "https" : "http")}://{newFile.BucketName}-{_OSSProviderOptions.Endpoint}.cos.{_OSSProviderOptions.Region}.myqcloud.com/{filePath}";
                    break;

                case OSSProvider.Minio:
                    // 获取Minio文件的下载或者预览地址
                    // newFile.Url = await GetMinioPreviewFileUrl(newFile.BucketName, filePath);// 这种方法生成的Url是有7天有效期的，不能这样使用
                    // 需要在MinIO中的Buckets开通对 Anonymous 的readonly权限
                    var customHost = _OSSProviderOptions.CustomHost;
                    if (string.IsNullOrWhiteSpace(customHost))
                        customHost = _OSSProviderOptions.Endpoint;
                    newFile.Url = $"{(_OSSProviderOptions.IsEnableHttps ? "https" : "http")}://{customHost}/{newFile.BucketName}/{filePath}";
                    break;
            }
        }
        else if (App.Configuration["SSHProvider:Enabled"].ToBoolean())
        {
            var fullPath = string.Concat(path.StartsWith('/') ? path : "/" + path, "/", finalName);
            using SSHHelper helper = new(App.Configuration["SSHProvider:Host"],
                App.Configuration["SSHProvider:Port"].ToInt(), App.Configuration["SSHProvider:Username"], App.Configuration["SSHProvider:Password"]);
            helper.UploadFile(input.File.OpenReadStream(), fullPath);
        }
        else
        {
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
        }
        await _sysFileRep.AsInsertable(newFile).ExecuteCommandAsync();

        var entity = input.Adapt<Leadingtasksfile>();
        entity.TaskId = input.TaskId;
        entity.Type = newFile.Suffix;
        entity.Url = newFile.Url;
        

        return await _leadingtasksfileRep.InsertAsync(entity) ? entity.Id : 0;
    }




    /// <summary>
    /// 删除带班任务上报文件 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除带班任务上报文件")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteLeadingtasksfileInput input)
    {
        var entity = await _leadingtasksfileRep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _leadingtasksfileRep.FakeDeleteAsync(entity);   //假删除
        //await _leadingtasksfileRep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 批量删除带班任务上报文件 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("批量删除带班任务上报文件")]
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    public async Task<int> BatchDelete([Required(ErrorMessage = "主键列表不能为空")]List<DeleteLeadingtasksfileInput> input)
    {
        var exp = Expressionable.Create<Leadingtasksfile>();
        foreach (var row in input) exp = exp.Or(it => it.Id == row.Id);
        var list = await _leadingtasksfileRep.AsQueryable().Where(exp.ToExpression()).ToListAsync();
        return await _leadingtasksfileRep.FakeDeleteAsync(list);   //假删除
        //return await _leadingtasksfileRep.DeleteAsync(list);   //真删除
    }

    ///// <summary>
    ///// 更新带班任务上报文件 ✏️
    ///// </summary>
    ///// <param name="input"></param>
    ///// <returns></returns>
    //[DisplayName("更新带班任务上报文件")]
    //[ApiDescriptionSettings(Name = "Update"), HttpPost]
    //public async Task Update(UpdateLeadingtasksfileInput input)
    //{
    //    var entity = input.Adapt<Leadingtasksfile>();
    //    await _leadingtasksfileRep.AsUpdateable(entity)
    //    .ExecuteCommandAsync();
    //}

    ///// <summary>
    ///// 导出带班任务上报文件记录 🔖
    ///// </summary>
    ///// <param name="input"></param>
    ///// <returns></returns>
    //[DisplayName("导出带班任务上报文件记录")]
    //[ApiDescriptionSettings(Name = "Export"), HttpPost, NonUnify]
    //public async Task<IActionResult> Export(PageLeadingtasksfileInput input)
    //{
    //    var list = (await Page(input)).Items?.Adapt<List<ExportLeadingtasksfileOutput>>() ?? new();
    //    if (input.SelectKeyList?.Count > 0) list = list.Where(x => input.SelectKeyList.Contains(x.Id)).ToList();
    //    return ExcelHelper.ExportTemplate(list, "带班任务上报文件导出记录");
    //}
    
    ///// <summary>
    ///// 下载带班任务上报文件数据导入模板 ⬇️
    ///// </summary>
    ///// <returns></returns>
    //[DisplayName("下载带班任务上报文件数据导入模板")]
    //[ApiDescriptionSettings(Name = "Import"), HttpGet, NonUnify]
    //public IActionResult DownloadTemplate()
    //{
    //    return ExcelHelper.ExportTemplate(new List<ExportLeadingtasksfileOutput>(), "带班任务上报文件导入模板");
    //}
    
    ///// <summary>
    ///// 导入带班任务上报文件记录 💾
    ///// </summary>
    ///// <returns></returns>
    //[DisplayName("导入带班任务上报文件记录")]
    //[ApiDescriptionSettings(Name = "Import"), HttpPost, NonUnify, UnitOfWork]
    //public IActionResult ImportData([Required] IFormFile file)
    //{
    //    lock (this)
    //    {
    //        var stream = ExcelHelper.ImportData<ImportLeadingtasksfileInput, Leadingtasksfile>(file, (list, markerErrorAction) =>
    //        {
    //            _sqlSugarClient.Utilities.PageEach(list, 2048, pageItems =>
    //            {
                    
    //                // 校验并过滤必填基本类型为null的字段
    //                var rows = pageItems.Where(x => {
    //                    return true;
    //                }).Adapt<List<Leadingtasksfile>>();
                    
    //                var storageable = _leadingtasksfileRep.Context.Storageable(rows)
    //                    .SplitError(it => it.Item.Type?.Length > 32, "文件类型长度不能超过32个字符")
    //                    .SplitError(it => it.Item.Url?.Length > 500, "文件路径长度不能超过500个字符")
    //                    .SplitInsert(_ => true)
    //                    .ToStorage();
                    
    //                storageable.BulkCopy();
    //                storageable.BulkUpdate();
                    
    //                // 标记错误信息
    //                markerErrorAction.Invoke(storageable, pageItems, rows);
    //            });
    //        });
            
    //        return stream;
    //    }
    //}
}
