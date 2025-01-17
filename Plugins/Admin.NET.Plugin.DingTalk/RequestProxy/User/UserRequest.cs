// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.User.DTO;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.User;

public class UserRequest : IScoped
{
    private readonly IUserRequestProxy _request;

    public UserRequest(IUserRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 查询用户详情
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="userid">用户的userId</param>
    /// <param name="language">通讯录语言。zh_CN：中文（默认值），en_US：英文</param>
    /// <returns></returns>
    public async Task<UserDetailResponse> UserDetail(string accessToken, string userid, string language = "zh_CN")
    {
        var res = await _request.UserDetail(accessToken, new UserDetailRequest
        {
            UserId = userid,
            Language = language
        });
        return res.ToObject<UserDetailResponse>();
    }

    /// <summary>
    /// 获取部门用户基础信息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">部门ID，如果是根部门，该参数传1</param>
    /// <param name="cursor">分页查询的游标，最开始传0，后续传返回参数中的next_cursor值</param>
    /// <param name="size">分页长度，最大值100</param>
    /// <param name="orderField">部门成员的排序规则。默认值，custom。entry_asc：代表按照进入部门的时间升序。entry_desc：代表按照进入部门的时间降序。modify_asc：代表按照部门信息修改时间升序。modify_desc：代表按照部门信息修改时间降序。custom：代表用户定义(未定义时按照拼音)排序。</param>
    /// <param name="containAccessLimit">是否返回访问受限的员工</param>
    /// <param name="language">通讯录语言，取值。 zh_CN：中文（默认值）。 en_US：英文。</param>
    /// <returns></returns>
    public async Task<UserListSimpleResponse> UserListSimple(string accessToken, long deptId = 1, long cursor = 0, long size = 100, string orderField = "custom", bool containAccessLimit = false, string language = "zh_CN")
    {
        var resStr = await _request.UserListSimple(accessToken, new UserListSimpleRequest
        {
            DeptId = deptId,
            Cursor = cursor,
            Size = size,
            OrderField = orderField,
            ContainAccessLimit = containAccessLimit,
            Language = language
        });
        var res = resStr.ToObject<UserListSimpleResponse>();
        return res;
    }

    /// <summary>
    /// 获取部门用户userid列表
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">部门deptId</param>
    /// <returns></returns>
    public async Task<UserListIdResponse> UserListId(string accessToken, long deptId = 1)
    {
        var resStr = await _request.UserListId(accessToken, new UserListIdRequest
        {
            DeptId = deptId
        });
        return resStr.ToObject<UserListIdResponse>();
    }

    /// <summary>
    /// 获取部门用户详情
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="deptId">部门ID，如果是根部门，该参数传1</param>
    /// <param name="cursor">分页查询的游标，最开始传0，后续传返回参数中的next_cursor值</param>
    /// <param name="size">分页大小</param>
    /// <param name="orderField">部门成员的排序规则，默认不传是按自定义排序（custom）：entry_asc：代表按照进入部门的时间升序，entry_desc：代表按照进入部门的时间降序，modify_asc：代表按照部门信息修改时间升序，modify_desc：代表按照部门信息修改时间降序，custom：代表用户定义(未定义时按照拼音)排序</param>
    /// <param name="containAccessLimit">是否返回访问受限的员工</param>
    /// <param name="language">通讯录语言，取值。zh_CN：中文（默认值）。en_US：英文。</param>
    /// <returns></returns>
    public async Task<UserListResponse> UserList(string accessToken, long deptId = 1, long cursor = 0, long size = 100, string orderField = "custom", bool containAccessLimit = false, string language = "zh_CN")
    {
        var resStr = await _request.UserList(accessToken, new UserListRequest
        {
            DeptId = deptId,
            Cursor = cursor,
            Size = size,
            OrderField = orderField,
            ContainAccessLimit = containAccessLimit,
            Language = language
        });
        var res = resStr.ToObject<UserListResponse>();
        return res;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="size">支持分页查询，与offset参数同时设置时才生效，此参数代表分页大小，默认值20，最大值200</param>
    /// <param name="offset">支持分页查询，与size参数同时设置时才生效，此参数代表偏移量，偏移量从0开始</param>
    /// <returns></returns>
    public async Task<RoleListResponse> RoleList(string accessToken, int size = 20, int offset = 0)
    {
        var resStr = await _request.RoleList(accessToken, new RoleListRequest { Offset = offset, Size = size });
        var res = resStr.ToObject<RoleListResponse>();
        return res;
    }
}