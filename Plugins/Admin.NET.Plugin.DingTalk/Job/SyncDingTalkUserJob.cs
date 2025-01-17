// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk;
using Admin.NET.Plugin.DingTalk.RequestProxy.HRM;
using Admin.NET.Plugin.DingTalk.RequestProxy.HRM.DTO;
using Admin.NET.Plugin.DingTalk.Service;

using Furion.Schedule;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Admin.NET.Plugin.Job;

/// <summary>
/// 同步钉钉用户job
/// </summary>
[JobDetail("SyncDingTalkUserJob", Description = "同步钉钉用户", GroupName = "default", Concurrent = false)]
[Daily(TriggerId = "SyncDingTalkUserTrigger", Description = "同步钉钉用户")]
public class SyncDingTalkUserJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public SyncDingTalkUserJob(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory)
    {
        _scopeFactory = scopeFactory;
        _logger = loggerFactory.CreateLogger(CommonConst.SysLogCategoryName);
    }

    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        using var serviceScope = _scopeFactory.CreateScope();
        var sysUserRepo = serviceScope.ServiceProvider.GetRequiredService<SqlSugarRepository<SysUser>>();
        var dingTalkUserRepo = serviceScope.ServiceProvider.GetRequiredService<SqlSugarRepository<DingTalkUser>>();
        var dingTalkOptions = serviceScope.ServiceProvider.GetRequiredService<IOptions<DingTalkOptions>>().Value;
        var dingTalkService = serviceScope.ServiceProvider.GetRequiredService<DingTalkService>();
        var hrmRequest = serviceScope.ServiceProvider.GetRequiredService<HrmRequest>();

        // 获取Token
        var token = await dingTalkService.GetDingTalkToken();
        ArgumentNullException.ThrowIfNull(token);

        var dingTalkUserList = new List<RosterListResultDomain>();
        var offset = 0;
        while (offset >= 0)
        {
            // 获取用户Id列表
            var userIdsRes = await hrmRequest.EmployeeQueryOnJob(token,
                new List<string> { "2", "3", "5", "-1" }, 50, offset);
            if (!userIdsRes.Success)
            {
                _logger.LogError(userIdsRes.ErrMsg);
                break;
            }
            // 根据用户Id获取花名册
            var rosterRes = await hrmRequest.RosterListsQuery(token,
                userIdsRes.Result.DataList,
                new List<string> { DingTalkConst.NameField, DingTalkConst.JobNumberField, DingTalkConst.MobileField, DingTalkConst.DeptField, DingTalkConst.DeptIdField, DingTalkConst.PositionField },
                dingTalkOptions.AgentId);
            if (!rosterRes.Success)
            {
                _logger.LogError(rosterRes.ErrMsg);
                break;
            }
            dingTalkUserList.AddRange(rosterRes.Result);
            if (userIdsRes.Result.NextCursor == null)
            {
                break;
            }
            // 保存分页游标
            offset = (int)userIdsRes.Result.NextCursor;
        }

        // 判断新增还是更新
        var sysDingTalkUserIdList = await dingTalkUserRepo.AsQueryable().Select(u => new
        {
            u.Id,
            u.DingTalkUserId
        }).ToListAsync();

        var uDingTalkUser = dingTalkUserList.Where(u => sysDingTalkUserIdList.Any(m => m.DingTalkUserId == u.UserId)); // 需要更新的用户Id
        var iDingTalkUser = dingTalkUserList.Where(u => sysDingTalkUserIdList.All(m => m.DingTalkUserId != u.UserId)); // 需要新增的用户Id

        // 新增钉钉用户
        var iUser = iDingTalkUser.Select(res => new DingTalkUser
        {
            DingTalkUserId = res.UserId,
            Name = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.NameField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Mobile = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.MobileField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            JobNumber = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.JobNumberField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            DeptId = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.DeptIdField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Dept = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.DeptField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Position = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.PositionField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            TenantId = dingTalkOptions.TenantId
        }).ToList();
        if (iUser.Count > 0)
        {
            await dingTalkUserRepo.CopyNew().AsInsertable(iUser).ExecuteCommandAsync();
        }

        // 更新钉钉用户
        var uUser = uDingTalkUser.Select(res => new DingTalkUser
        {
            Id = sysDingTalkUserIdList.Where(u => u.DingTalkUserId == res.UserId).Select(u => u.Id).FirstOrDefault(),
            DingTalkUserId = res.UserId,
            Name = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.NameField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Mobile = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.MobileField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            JobNumber = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.JobNumberField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            DeptId = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.DeptIdField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Dept = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.DeptField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            Position = res.FieldDataList.Where(u => u.FieldCode == DingTalkConst.PositionField).Select(u => u.FieldValueList.Select(m => m.Value).FirstOrDefault()).FirstOrDefault(),
            TenantId = dingTalkOptions.TenantId,
        }).ToList();
        if (uUser.Count > 0)
        {
            await dingTalkUserRepo.CopyNew().AsUpdateable(uUser).UpdateColumns(u => new
            {
                u.DingTalkUserId,
                u.Name,
                u.Mobile,
                u.JobNumber,
                u.DeptId,
                u.Dept,
                u.Position,
                u.UpdateTime,
                u.UpdateUserName,
                u.UpdateUserId,
                u.TenantId
            }).ExecuteCommandAsync();
        }

        //获取没有对应SysUser的DingTalkUser
        var dingTalkWithoutSysuser = await dingTalkUserRepo.AsQueryable()
           .LeftJoin<SysUser>((dUser, sUser) => dUser.DingTalkUserId == sUser.Account)
           .GroupBy((dUser, sUser) => dUser.Id)
           .Having((dUser, sUser) => SqlFunc.AggregateCount(sUser.Id) == 0)
           .Select((dUser, sUser) => dUser)
           .ToListAsync();

        //插入对应的SysUser
        if (dingTalkWithoutSysuser.Count > 0)
        {
            var iSysUser = dingTalkWithoutSysuser.Select(dUser => new SysUser
            {
                Account = dUser.DingTalkUserId,
                Password = CryptogramUtil.Encrypt(dUser.DingTalkUserId),
                RealName = dUser.Name ?? "",
                TenantId = dingTalkOptions.TenantId,
            }).ToList();
            await sysUserRepo.InsertRangeAsync(iSysUser);
        }

        //将SysUser.Id回填到对应DingTalkUser.SysUserId
        await dingTalkUserRepo.AsUpdateable()
            .SetColumns(dUser => new DingTalkUser
            {
                SysUserId = SqlFunc.Subqueryable<SysUser>()
                                    .Where(sUser => sUser.Account == dUser.DingTalkUserId)
                                    .Select(sUser => sUser.Id)
            })
            .ExecuteCommandAsync();

        var originColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("【" + DateTime.Now + "】同步钉钉用户");
        Console.ForegroundColor = originColor;
    }
}