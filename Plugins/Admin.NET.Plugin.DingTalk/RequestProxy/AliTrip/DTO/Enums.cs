// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip.DTO;

/// <summary>
/// 阿里商旅结算方式
/// </summary>
[SuppressSniffer]
public enum AliTripPricePayTypeEnum
{
    个人现付 = 1,
    企业现付 = 2,
    企业月结 = 4,
    企业预存 = 8
}

/// <summary>
/// 阿里商旅资金流向
/// </summary>
[SuppressSniffer]
public enum AliTripPriceTypeEnum
{
    支出 = 1,
    收入 = 2
}

/// <summary>
/// 状态
/// </summary>
[SuppressSniffer]
public enum AliTripFlightOrdersInsureInfoStatusEnum
{
    已出保 = 1,
    已退保 = 2
}

/// <summary>
/// 行程类型
/// </summary>
[SuppressSniffer]
public enum AliTripFlightOrdersTripTypeEnum
{
    单程 = 0,
    往返 = 1,
    中转 = 2
}

/// <summary>
/// 订单状态
/// </summary>
[SuppressSniffer]
public enum AliTripTrainOrdersStatusEnum
{
    待支付 = 0,
    出票中 = 1,
    已关闭 = 2,
    改签成功 = 3,
    退票成功 = 4,
    出票完成 = 5,
    退票申请中 = 6,
    改签申请中 = 7,
    已出票or已发货 = 8,
    出票失败 = 9,
    改签失败 = 10,
    退票失败 = 11
}