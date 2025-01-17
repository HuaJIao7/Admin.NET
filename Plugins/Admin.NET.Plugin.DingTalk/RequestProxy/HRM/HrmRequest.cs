// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.HRM.DTO;

using NewLife;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.HRM;

public class HrmRequest : IScoped
{
    private readonly IHrmRequestProxy _request;

    public HrmRequest(IHrmRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 获取在职员工列表
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="statusList">在职员工状态筛选，可以查询多个状态。2：试用期、3：正式、5：待离职、-1：无状态</param>
    /// <param name="size">分页大小，最大50</param>
    /// <param name="offset">分页游标，从0开始。根据返回结果里的next_cursor是否为空来判断是否还有下一页，且再次调用时offset设置成next_cursor的值</param>
    /// <returns></returns>
    public async Task<EmployeeQueryOnJobResponse> EmployeeQueryOnJob(string accessToken, List<string> statusList, int size = 50, int offset = 0)
    {
        var resStr = await _request.EmployeeQueryonjob(accessToken, new EmployeeQueryOnJobRequest
        {
            StatusList = statusList.Join(),
            Size = size,
            Offset = offset
        });
        return resStr.ToObject<EmployeeQueryOnJobResponse>();
    }

    /// <summary>
    /// 获取员工花名册字段信息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="useridList">员工的 userId 列表，一次最多支持传100个值</param>
    /// <param name="fieldFilterList">需要获取的花名册字段field_code值列表，一次最多支持传100个值</param>
    /// <param name="agentId">应用的AgentId</param>
    /// <returns></returns>
    public async Task<RosterListsQueryResponse> RosterListsQuery(string accessToken, List<string> useridList, List<string> fieldFilterList, long agentId)
    {
        var resStr = await _request.RosterListsQuery(accessToken, new RosterListsQueryRequest
        {
            UserIdList = useridList.Join(),
            FieldFilterList = fieldFilterList.Join(),
            AgentId = agentId
        });
        return resStr.ToObject<RosterListsQueryResponse>();
    }

    /// <summary>
    /// 获取花名册元数据
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="agentId"></param>
    /// <returns></returns>
    public async Task<GetRosterMetaResponse> GetRosterMeta(string accessToken, long agentId)
    {
        var resStr = await _request.GetRosterMeta(accessToken, new GetRosterMetaRequest { AgentId = agentId });
        return resStr.ToObject<GetRosterMetaResponse>();
    }
}