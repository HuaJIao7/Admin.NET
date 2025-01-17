// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.AliTrip.DTO;

/// <summary>
/// 获取企业机票订单数据
/// </summary>
public class GetAliTripFlightOrdersResponse : DingtalkResponseErrorSuccess
{
    /// <summary>
    /// 机票列表
    /// </summary>
    [JsonProperty("flight_order_list")]
    [JsonPropertyName("flight_order_list")]
    public List<FlightOrderDomain> FlightOrderList { get; set; }

    /// <summary>
    /// 分页相关信息
    /// </summary>
    [JsonProperty("page_info")]
    [JsonPropertyName("page_info")]
    public PageInfoDomain PageInfo { get; set; }
}

public class FlightOrderDomain
{
    /// <summary>
    /// 机票订单id
    /// </summary>
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [JsonProperty("gmt_modified")]
    [JsonPropertyName("gmt_modified")]
    public DateTime GmtModified { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [JsonProperty("corp_name")]
    [JsonPropertyName("corp_name")]
    public string CorpName { get; set; }

    /// <summary>
    /// 企业id
    /// </summary>
    [JsonProperty("corpid")]
    [JsonPropertyName("corpid")]
    public string CorpId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonProperty("gmt_create")]
    [JsonPropertyName("gmt_create")]
    public DateTime GmtCreate { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    [JsonProperty("user_name")]
    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    /// <summary>
    /// 部门id
    /// </summary>
    [JsonProperty("deptid")]
    [JsonPropertyName("deptid")]
    public string DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [JsonProperty("dept_name")]
    [JsonPropertyName("dept_name")]
    public string DeptName { get; set; }

    /// <summary>
    /// 商旅申请单id
    /// </summary>
    [JsonProperty("apply_id")]
    [JsonPropertyName("apply_id")]
    public string ApplyId { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [JsonProperty("contact_name")]
    [JsonPropertyName("contact_name")]
    public string ContactName { get; set; }

    /// <summary>
    /// 出发城市
    /// </summary>
    [JsonProperty("dep_city")]
    [JsonPropertyName("dep_city")]
    public string DepCity { get; set; }

    /// <summary>
    /// 到达城市
    /// </summary>
    [JsonProperty("arr_city")]
    [JsonPropertyName("arr_city")]
    public string ArrCity { get; set; }

    /// <summary>
    /// 出发日期
    /// </summary>
    [JsonProperty("dep_date")]
    [JsonPropertyName("dep_date")]
    public DateTime DepDate { get; set; }

    /// <summary>
    /// 到达日期
    /// </summary>
    [JsonProperty("ret_date")]
    [JsonPropertyName("ret_date")]
    public DateTime RetDate { get; set; }

    /// <summary>
    /// 行程类型。0:单程，1:往返，2:中转
    /// </summary>
    [JsonProperty("trip_type")]
    [JsonPropertyName("trip_type")]
    public AliTripFlightOrdersTripTypeEnum TripType { get; set; }

    /// <summary>
    /// 乘机人数量
    /// </summary>
    [JsonProperty("passenger_count")]
    [JsonPropertyName("passenger_count")]
    public int PassengerCount { get; set; }

    /// <summary>
    /// 舱位类型
    /// </summary>
    [JsonProperty("cabin_class")]
    [JsonPropertyName("cabin_class")]
    public string CabinClass { get; set; }

    /// <summary>
    /// 订单状态：0待支付,1出票中,2已关闭,3有改签单,4有退票单,5出票成功,6退票申请中,7改签申请中
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// 折扣
    /// </summary>
    [JsonProperty("discount")]
    [JsonPropertyName("discount")]
    public string Discount { get; set; }

    /// <summary>
    /// 航班号
    /// </summary>
    [JsonProperty("flight_no")]
    [JsonPropertyName("flight_no")]
    public string FlightNo { get; set; }

    /// <summary>
    /// 乘机人，多个用‘,’分割
    /// </summary>
    [JsonProperty("passenger_name")]
    [JsonPropertyName("passenger_name")]
    public string PassengerName { get; set; }

    /// <summary>
    /// 出发机场
    /// </summary>
    [JsonProperty("dep_airport")]
    [JsonPropertyName("dep_airport")]
    public string DepAirport { get; set; }

    /// <summary>
    /// 到达机场
    /// </summary>
    [JsonProperty("arr_airport")]
    [JsonPropertyName("arr_airport")]
    public string ArrAirport { get; set; }

    /// <summary>
    /// 发票信息对象
    /// </summary>
    [JsonProperty("invoice")]
    [JsonPropertyName("invoice")]
    public InvoiceDomain Invoice { get; set; }

    /// <summary>
    /// 成本中心对象
    /// </summary>
    [JsonProperty("cost_center")]
    [JsonPropertyName("cost_center")]
    public CostCenterDomain CostCenter { get; set; }

    /// <summary>
    /// 价目信息
    /// </summary>
    [JsonProperty("price_info_list")]
    [JsonPropertyName("price_info_list")]
    public List<PriceInfoDomain> PriceInfoList { get; set; }

    /// <summary>
    /// 保险信息
    /// </summary>
    [JsonProperty("insureInfo_list")]
    [JsonPropertyName("insureInfo_list")]
    public List<InsureInfoDomain> InsureInfoList { get; set; }

    /// <summary>
    /// 第三方行程id
    /// </summary>
    [JsonProperty("thirdpart_itinerary_id")]
    [JsonPropertyName("thirdpart_itinerary_id")]
    public string ThirdpartItineraryId { get; set; }

    /// <summary>
    /// 出行人列表
    /// </summary>
    [JsonProperty("user_affiliate_list")]
    [JsonPropertyName("user_affiliate_list")]
    public List<UserAffiliateDomain> UserAffiliateList { get; set; }

    /// <summary>
    /// 第三方申请单ID
    /// </summary>
    [JsonProperty("thirdpart_apply_id")]
    [JsonPropertyName("thirdpart_apply_id")]
    public string ThirdpartApplyId { get; set; }

    /// <summary>
    /// 申请单名称
    /// </summary>
    [JsonProperty("btrip_title")]
    [JsonPropertyName("btrip_title")]
    public string BtripTitle { get; set; }

    /// <summary>
    /// 项目id
    /// </summary>
    [JsonProperty("project_id")]
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; }

    /// <summary>
    /// 项目code
    /// </summary>
    [JsonProperty("project_code")]
    [JsonPropertyName("project_code")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    [JsonProperty("project_title")]
    [JsonPropertyName("project_title")]
    public string ProjectTitle { get; set; }

    /// <summary>
    /// 第三方项目id
    /// </summary>
    [JsonProperty("third_part_project_id")]
    [JsonPropertyName("third_part_project_id")]
    public string ThirdPartProjectId { get; set; }
}

/// <summary>
/// 发票信息对象
/// </summary>
public class InvoiceDomain
{
    /// <summary>
    /// 商旅发票id
    /// </summary>
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// 发票抬头
    /// </summary>
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
}

/// <summary>
/// 成本中心对象
/// </summary>
public class CostCenterDomain
{
    /// <summary>
    /// 商旅成本中心id
    /// </summary>
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// 企业id
    /// </summary>
    [JsonProperty("corpid")]
    [JsonPropertyName("corpid")]
    public string CorpId { get; set; }

    /// <summary>
    /// 成本中心编号
    /// </summary>
    [JsonProperty("number")]
    [JsonPropertyName("number")]
    public string Number { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

/// <summary>
/// 价目信息
/// </summary>
public class PriceInfoDomain
{
    /// <summary>
    /// 价格
    /// </summary>
    [JsonProperty("price")]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// 资金流向:1:支出，2:收入
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public AliTripPriceTypeEnum Type { get; set; }

    /// <summary>
    /// 交易类目
    /// </summary>
    [JsonProperty("category")]
    [JsonPropertyName("category")]
    public string Category { get; set; }

    /// <summary>
    /// 结算方式:1：个人现付，2:企业现付,4:企业月结，8、企业预存
    /// </summary>
    [JsonProperty("pay_type")]
    [JsonPropertyName("pay_type")]
    public AliTripPricePayTypeEnum PayType { get; set; }

    /// <summary>
    /// 流水创建时间
    /// </summary>
    [JsonProperty("gmt_create")]
    [JsonPropertyName("gmt_create")]
    public DateTime GmtCreate { get; set; }

    /// <summary>
    /// 乘机人，多个用‘,’分割
    /// </summary>
    [JsonProperty("passenger_name")]
    [JsonPropertyName("passenger_name")]
    public string PassengerName { get; set; }

    /// <summary>
    /// 流水单号
    /// </summary>
    [JsonProperty("tradeId")]
    [JsonPropertyName("tradeId")]
    public string TradeId { get; set; }

    /// <summary>
    /// 改签票号
    /// </summary>
    [JsonProperty("ticket_no")]
    [JsonPropertyName("ticket_no")]
    public string TicketNo { get; set; }

    /// <summary>
    /// 改签前的票号
    /// </summary>
    [JsonProperty("original_ticket_no")]
    [JsonPropertyName("original_ticket_no")]
    public string OriginalTicketNo { get; set; }

    /// <summary>
    /// 改签航班号
    /// </summary>
    [JsonProperty("changeFlightNo")]
    [JsonPropertyName("changeFlightNo")]
    public string ChangeFlightNo { get; set; }

    /// <summary>
    /// 改签折扣
    /// </summary>
    [JsonProperty("discount")]
    [JsonPropertyName("discount")]
    public string Discount { get; set; }

    /// <summary>
    /// 改签航班起飞时间
    /// </summary>
    [JsonProperty("startTime")]
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 改签航班到达时间
    /// </summary>
    [JsonProperty("endTime")]
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }
}

/// <summary>
/// 保险信息
/// </summary>
public class InsureInfoDomain
{
    /// <summary>
    /// 保单号
    /// </summary>
    [JsonProperty("insure_no")]
    [JsonPropertyName("insure_no")]
    public string InsureNo { get; set; }

    /// <summary>
    /// 状态：1已出保 2已退保
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public AliTripFlightOrdersInsureInfoStatusEnum Status { get; set; }

    /// <summary>
    /// 乘机人(保险人)姓名
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

/// <summary>
/// 出行人列表
/// </summary>
public class UserAffiliateDomain
{
    /// <summary>
    /// 出行人ID
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 出行人名称
    /// </summary>
    [JsonProperty("user_name")]
    [JsonPropertyName("user_name")]
    public string UserName { get; set; }
}

/// <summary>
/// 分页相关信息
/// </summary>
public class PageInfoDomain
{
    /// <summary>
    /// 当前页
    /// </summary>
    [JsonProperty("page")]
    [JsonPropertyName("page")]
    public string Page { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    [JsonProperty("page_size")]
    [JsonPropertyName("page_size")]
    public string PageSize { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    [JsonProperty("total_number")]
    [JsonPropertyName("total_number")]
    public string TotalNumber { get; set; }
}