// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using NewLife;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Checkin;

public class CheckinRequest : IScoped
{
    private readonly ICheckinRequestProxy _request;

    public CheckinRequest(ICheckinRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 获取用户签到记录
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="useridList">需要查询的用户列表，最大列表长度为10</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">截止时间。如果是取1个人的数据，时间范围最大10天。如果是取多个人的数据，时间范围最大1天。</param>
    /// <param name="cursor">分页查询的游标，最开始可以传0</param>
    /// <param name="size">分页查询的每页大小，最大100</param>
    /// <returns></returns>
    public async Task<DTO.GetUserRecordResponse> GetUserRecord(string accessToken, List<string> useridList, DateTime startTime, DateTime endTime, long cursor = 0, long size = 100)
    {
        var resStr = await _request.GetUserRecord(accessToken, new DTO.GetUserRecordRequest
        {
            UserIdList = useridList.Join(),
            StartTime = new DateTimeOffset(startTime).ToUnixTimeMilliseconds(),
            EndTime = new DateTimeOffset(endTime).ToUnixTimeMilliseconds(),
            Cursor = cursor,
            Size = size
        });
        var res = resStr.ToObject<DTO.GetUserRecordResponse>();
        return res;
    }

    /// <summary>
    /// 获取部门用户签到记录
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">部门ID，1表示根部门</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="offset">支持分页查询，与size参数同时设置时才生效，此参数代表偏移量，从0开始</param>
    /// <param name="size">支持分页查询，与offset 参数同时设置时才生效，此参数代表分页大小，最大100</param>
    /// <param name="order">排序。asc：正序，desc：倒序</param>
    /// <returns></returns>
    public async Task<DTO.GetDeptRecordResponse> GetDeptRecord(string accessToken, long deptId, DateTime startTime, DateTime endTime, long offset = 0, long size = 100, string order = "asc")
    {
        var resStr = await _request.GetDeptRecord(accessToken, deptId.ToString(), new DateTimeOffset(startTime).ToUnixTimeMilliseconds(), new DateTimeOffset(endTime).ToUnixTimeMilliseconds(), offset, size, order);
        var res = resStr.ToObject<DTO.GetDeptRecordResponse>();
        return res;
    }
}