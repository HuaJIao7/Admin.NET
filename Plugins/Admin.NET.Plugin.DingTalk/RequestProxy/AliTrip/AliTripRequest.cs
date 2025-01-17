// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip.DTO;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip;

public class AliTripRequest : IScoped
{
    private readonly IAliTripRequestProxy _request;

    public AliTripRequest(IAliTripRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 获取阿里商旅访问地址
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="req"></param>
    /// <returns></returns>
    public async Task<GetAliTripAddressResponse> GetAliTripAddress(string accessToken, AliTripAddressRequestDomain req)
    {
        var resStr = await _request.GetAliTripAddress(accessToken, new GetAliTripAddressRequest
        {
            Request = req
        });
        return resStr.ToObject<GetAliTripAddressResponse>();
    }

    /// <summary>
    /// 获取企业机票订单数据
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="rq"></param>
    /// <returns></returns>
    public async Task<GetAliTripFlightOrdersResponse> GetAliTripFlightOrders(string accessToken, GetAliTripFlightOrdersRequest rq)
    {
        var resStr = await _request.GetAliTripFlightOrders(accessToken, rq);
        return resStr.ToObject<GetAliTripFlightOrdersResponse>();
    }

    /// <summary>
    /// 获取企业火车票订单数据
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="rq"></param>
    /// <returns></returns>
    public async Task<GetAliTripTrainOrdersResponse> GetAliTripTrainOrders(string accessToken, GetAliTripTrainOrdersRequest rq)
    {
        var resStr = await _request.GetAliTripTrainOrders(accessToken, rq);
        return resStr.ToObject<GetAliTripTrainOrdersResponse>();
    }
}