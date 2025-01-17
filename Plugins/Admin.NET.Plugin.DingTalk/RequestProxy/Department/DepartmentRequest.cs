// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.Department.DTO;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Department;

public class DepartmentRequest : IScoped
{
    private readonly IDepartmentRequestProxy _request;

    public DepartmentRequest(IDepartmentRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 获取部门详情
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">部门ID，根部门ID为1</param>
    /// <param name="language">通讯录语言：zh_CN（默认）：中文，en_US：英文</param>
    /// <returns></returns>
    public async Task<GetDeptInfoResponse> GetDeptInfo(string accessToken, long deptId = 1, string language = "zh_CN")
    {
        var resStr = await _request.GetDeptInfo(accessToken, new DTO.GetDeptInfoRequest
        {
            DeptId = deptId,
            Language = language
        });
        var res = resStr.ToObject<GetDeptInfoResponse>();
        return res;
    }

    /// <summary>
    /// 获取子部门ID列表
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">父部门ID，根部门传1</param>
    /// <returns></returns>
    public async Task<ListSubIdResponse> ListSubId(string accessToken, long deptId = 1)
    {
        var resStr = await _request.ListSubId(accessToken, new ListSubIdRequest
        {
            DeptId = deptId
        });
        var res = resStr.ToObject<ListSubIdResponse>();
        return res;
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">父部门ID</param>
    /// <param name="language">通讯录语言：，zh_CN（默认）：中文，en_US：英文</param>
    /// <returns></returns>
    public async Task<ListSubResponse> ListSub(string accessToken, long deptId = 1, string language = "zh_CN")
    {
        var resStr = await _request.ListSub(accessToken, new ListSubRequest
        {
            DeptId = deptId,
            Language = language
        });
        var res = resStr.ToObject<ListSubResponse>();
        return res;
    }
}