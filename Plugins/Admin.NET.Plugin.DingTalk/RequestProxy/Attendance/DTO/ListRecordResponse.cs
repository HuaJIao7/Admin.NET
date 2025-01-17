// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.BaseTypes;

using System.Text.Json.Serialization;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 获取打卡详情
/// </summary>
public class ListRecordResponse : DingtalkResponseError
{
    /// <summary>
    /// 打卡详情
    /// </summary>
    [JsonProperty("recordresult")]
    [JsonPropertyName("recordresult")]
    public List<ListRecordResultDomain> RecordResult { get; set; }
}

public class ListRecordResultDomain
{
    /// <summary>
    /// 关联的审批ID，当该字段非空时，表示打卡记录与请假、加班等审批有关
    /// </summary>
    [JsonPropertyName("approveId")]
    [JsonProperty("approveId")]
    public long ApproveId { get; set; }

    /// <summary>
    /// 基准定位精度
    /// </summary>
    [JsonProperty("baseAccuracy")]
    [JsonPropertyName("baseAccuracy")]
    public string BaseAccuracy { get; set; }

    /// <summary>
    /// 基准地址
    /// </summary>
    [JsonProperty("baseAddress")]
    [JsonPropertyName("baseAddress")]
    public string BaseAddress { get; set; }

    /// <summary>
    /// 计算迟到和早退，基准时间；也可作为排班打卡时间
    /// </summary>
    [JsonProperty("baseCheckTime")]
    [JsonPropertyName("baseCheckTime")]
    public string BaseCheckTime { get; set; }

    /// <summary>
    /// 基准纬度
    /// </summary>
    [JsonProperty("baseLatitude")]
    [JsonPropertyName("baseLatitude")]
    public string BaseLatitude { get; set; }

    /// <summary>
    /// 基准经度
    /// </summary>
    [JsonProperty("baseLongitude")]
    [JsonPropertyName("baseLongitude")]
    public string BaseLongitude { get; set; }

    /// <summary>
    /// 基准MAC地址
    /// </summary>
    [JsonProperty("baseMacAddr")]
    [JsonPropertyName("baseMacAddr")]
    public string BaseMacAddr { get; set; }

    /// <summary>
    /// 基准wifi ssid
    /// </summary>
    [JsonProperty("baseSsid")]
    [JsonPropertyName("baseSsid")]
    public string BaseSsid { get; set; }

    /// <summary>
    /// 关联的业务ID
    /// </summary>
    [JsonProperty("bizId")]
    [JsonPropertyName("bizId")]
    public string BizId { get; set; }

    /// <summary>
    /// 考勤类型：OnDuty：上班，OffDuty：下班
    /// </summary>
    [JsonProperty("checkType")]
    [JsonPropertyName("checkType")]
    public string CheckType { get; set; }

    /// <summary>
    /// 班次ID
    /// </summary>
    [JsonProperty("classId")]
    [JsonPropertyName("classId")]
    public long ClassId { get; set; }

    /// <summary>
    /// 打卡设备ID
    /// </summary>
    [JsonProperty("deviceId")]
    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; }

    /// <summary>
    /// 打卡设备序列号
    /// </summary>
    [JsonProperty("deviceSN")]
    [JsonPropertyName("deviceSN")]
    public string DeviceSN { get; set; }

    /// <summary>
    /// 打卡记录创建时间
    /// </summary>
    [JsonProperty("gmtCreate")]
    [JsonPropertyName("gmtCreate")]
    public string GmtCreate { get; set; }

    /// <summary>
    /// 打卡记录修改时间
    /// </summary>
    [JsonProperty("gmtModified")]
    [JsonPropertyName("gmtModified")]
    public string GmtModified { get; set; }

    /// <summary>
    /// 考勤组ID
    /// </summary>
    [JsonProperty("groupId")]
    [JsonPropertyName("groupId")]
    public long GroupId { get; set; }

    /// <summary>
    /// 考勤ID
    /// </summary>
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// 对应的invalidRecordType异常信息的具体描述
    /// </summary>
    [JsonProperty("invalidRecordMsg")]
    [JsonPropertyName("invalidRecordMsg")]
    public string InvalidRecordMsg { get; set; }

    /// <summary>
    /// 异常信息类型： Security：安全相关原因，Other：其他原因
    /// </summary>
    [JsonProperty("invalidRecordType")]
    [JsonPropertyName("invalidRecordType")]
    public string InvalidRecordType { get; set; }

    /// <summary>
    /// 是否合法.Y：合法，N：不合法
    /// </summary>
    [JsonProperty("isLegal")]
    [JsonPropertyName("isLegal")]
    public string IsLegal { get; set; }

    /// <summary>
    /// 定位方法
    /// </summary>
    [JsonProperty("locationMethod")]
    [JsonPropertyName("locationMethod")]
    public string LocationMethod { get; set; }

    /// <summary>
    /// 位置结果：Normal：范围内，Outside：范围外，NotSigned：未打卡
    /// </summary>
    [JsonProperty("locationResult")]
    [JsonPropertyName("locationResult")]
    public string LocationResult { get; set; }

    /// <summary>
    /// 打卡备注
    /// </summary>
    [JsonProperty("outsideRemark")]
    [JsonPropertyName("outsideRemark")]
    public string OutsideRemark { get; set; }

    /// <summary>
    /// 排班打卡时间
    /// </summary>
    [JsonProperty("planCheckTime")]
    [JsonPropertyName("planCheckTime")]
    public string PlanCheckTime { get; set; }

    /// <summary>
    /// 排班ID
    /// </summary>
    [JsonProperty("planId")]
    [JsonPropertyName("planId")]
    public long PlanId { get; set; }

    /// <summary>
    /// 关联的审批实例ID，当该字段非空时，表示打卡记录与请假、加班等审批有关
    /// </summary>
    [JsonProperty("procInstId")]
    [JsonPropertyName("procInstId")]
    public string ProcInstId { get; set; }

    /// <summary>
    /// 数据来源：ATM：考勤机打卡（指纹/人脸打卡），BEACON：IBeacon，DING_ATM：钉钉考勤机（考勤机蓝牙打卡），USER：用户打卡
    ///         BOSS：老板改签，APPROVE：审批系统，SYSTEM：考勤系统，AUTO_CHECK：自动打卡
    /// </summary>
    [JsonProperty("sourceType")]
    [JsonPropertyName("sourceType")]
    public string SourceType { get; set; }

    /// <summary>
    /// 打卡结果。 Normal：正常，Early：早退，Late：迟到，SeriousLate：严重迟到，Absenteeism：旷工迟到，NotSigned：未打卡
    /// </summary>
    [JsonProperty("timeResult")]
    [JsonPropertyName("timeResult")]
    public string TimeResult { get; set; }

    /// <summary>
    /// 用户打卡定位精度
    /// </summary>
    [JsonProperty("userAccuracy")]
    [JsonPropertyName("userAccuracy")]
    public string UserAccuracy { get; set; }

    /// <summary>
    /// 用户打卡地址
    /// </summary>
    [JsonProperty("userAddress")]
    [JsonPropertyName("userAddress")]
    public string UserAddress { get; set; }

    /// <summary>
    /// 实际打卡时间
    /// </summary>
    [JsonProperty("userCheckTime")]
    [JsonPropertyName("userCheckTime")]
    public string UserCheckTime { get; set; }

    /// <summary>
    /// 打卡人的userId
    /// </summary>
    [JsonProperty("userId")]
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    /// <summary>
    /// 用户打卡纬度
    /// </summary>
    [JsonProperty("userLatitude")]
    [JsonPropertyName("userLatitude")]
    public string UserLatitude { get; set; }

    /// <summary>
    /// 用户打卡经度
    /// </summary>
    [JsonProperty("userLongitude")]
    [JsonPropertyName("userLongitude")]
    public string UserLongitude { get; set; }

    /// <summary>
    /// 用户打卡wifi Mac地址
    /// </summary>
    [JsonProperty("userMacAddr")]
    [JsonPropertyName("userMacAddr")]
    public string UserMacAddr { get; set; }

    /// <summary>
    /// 用户打卡wifi SSID
    /// </summary>
    [JsonProperty("userSsid")]
    [JsonPropertyName("userSsid")]
    public string UserSsid { get; set; }

    /// <summary>
    /// 工作日
    /// </summary>
    [JsonProperty("workDate")]
    [JsonPropertyName("workDate")]
    public long WorkDate { get; set; }

    /// <summary>
    /// 企业ID
    /// </summary>
    [JsonProperty("corpId")]
    [JsonPropertyName("corpId")]
    public string CorpId { get; set; }
}