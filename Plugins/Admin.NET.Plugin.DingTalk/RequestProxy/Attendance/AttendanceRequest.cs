// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

using NewLife;

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance;

public class AttendanceRequest : IScoped
{
    private readonly IAttendanceRequestProxy _request;

    public AttendanceRequest(IAttendanceRequestProxy request)
    {
        _request = request;
    }

    /// <summary>
    /// 获取打卡详情
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="useridList">企业内的员工ID列表，最大值50</param>
    /// <param name="from">查询考勤打卡记录的起始工作日</param>
    /// <param name="to">查询考勤打卡记录的结束工作日</param>
    /// <param name="isI18N">是否为海外企业使用</param>
    /// <returns></returns>
    public async Task<ListRecordResponse> ListRecord(string accessToken, List<string> useridList, DateTime from, DateTime to, bool isI18N = false)
    {
        var requestBody = new ListRecordRequest
        {
            UserIds = useridList,
            CheckDateFrom = from.ToString("yyyy-MM-dd HH:mm:ss"),
            CheckDateTo = to.ToString("yyyy-MM-dd HH:mm:ss"),
            IsI18n = isI18N
        };
        var resStr = await _request.ListRecord(accessToken, requestBody);
        var res = resStr.ToObject<ListRecordResponse>();
        return res;
    }

    /// <summary>
    /// 通知审批通过
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="userid">员工的userId</param>
    /// <param name="approveId">审批单ID，最大长度100个字符，自定义值</param>
    /// <param name="tagName">审批单类型名称，最大长度20个字符。 支持类型如下：请假， 出差，外出，加班</param>
    /// <param name="jumpUrl">审批单跳转地址，最大长度200个字符</param>
    /// <param name="bizType">审批单类型，可取值：1：加班，2：出差、外出，3：请假</param>
    /// <param name="fromTime">开始时间。开始时间不能早于当前时间前31天</param>
    /// <param name="toTime">结束时间</param>
    /// <param name="durationUnit">时长单位，支持格式如下：day，halfDay，hour：biz_type为1时仅支持hour</param>
    /// <param name="calculateModel">计算方法：0：按自然日计算，1：按工作日计算</param>
    /// <param name="leaveCode">假期规则唯一标识。选填。仅支持bizType=3 请假时传不为空，可以支持根据假期类型设置的取整规则进行时长取整</param>
    /// <param name="subType">子类型名称，最大长度20个字符。审批单类型biz_type=3时，该参数必传。</param>
    /// <param name="overTimeDuration">biz_type为1时必传，加班时长单位小时</param>
    /// <param name="overTimeToMore">biz_type为1时必传：1：加班转调休，2：加班转工资</param>
    /// <returns></returns>
    public async Task<AttendanceApprovalsFinishResponse> ApproveFinish(string accessToken, string userid, string approveId, string tagName, string jumpUrl,
        AttendanceBizTypeEnum bizType, DateTime fromTime, DateTime toTime, AttendanceDurationUnitEnum durationUnit, AttendanceCalculateModelEnum calculateModel, string? leaveCode = null,
        string? subType = null, string? overTimeDuration = null, long? overTimeToMore = null)
    {
        if (bizType == AttendanceBizTypeEnum.请假 && string.IsNullOrEmpty(subType))
            throw Oops.Oh("审批单类型biz_type=3时，subType必传。");
        if (bizType == AttendanceBizTypeEnum.加班 && (string.IsNullOrEmpty(overTimeDuration) || !overTimeToMore.HasValue))
            throw Oops.Oh("biz_type为1时,overTimeDuration和overTimeToMore必传");
        var fromTimeStr = "";
        var toTimeStr = "";
        switch (durationUnit)
        {
            case AttendanceDurationUnitEnum.Day:
                fromTimeStr = fromTime.ToString("yyyy-MM-dd");
                toTimeStr = toTime.ToString("yyyy-MM-dd");
                break;

            case AttendanceDurationUnitEnum.HalfDay:
                fromTimeStr = TimeToHalfDay(fromTime);
                toTimeStr = TimeToHalfDay(toTime);
                break;

            case AttendanceDurationUnitEnum.Hour:
                fromTimeStr = fromTime.ToString("yyyy-MM-dd HH:mm");
                toTimeStr = toTime.ToString("yyyy-MM-dd HH:mm");
                break;
        }
        var requestBody = new AttdendanceApprovalsFinishRequest
        {
            ApproveId = approveId,
            TagName = tagName,
            JumpUrl = jumpUrl,
            OvertimeDuration = overTimeDuration,
            OverTimeToMore = overTimeToMore,
            SubType = subType,
            TopCalculateApproveDurationParam = new TopCalculateApproveDurationParamDomain
            {
                BizType = bizType,
                FromTime = fromTimeStr,
                ToTime = toTimeStr,
                DurationUnit = durationUnit.ToString(),
                CalculateModel = (long)calculateModel,
                LeaveCode = leaveCode
            }
        };
        try
        {
            var resStr = await _request.ApprovalsFinish(userid, accessToken, requestBody);
            var res = resStr.ToObject<AttendanceApprovalsFinishResponse>();
            return res;
        }
        catch (Exception ex)
        {
            throw Oops.Oh(ex.Message);
        }
    }

    /// <summary>
    /// 通知审批撤销
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="userid">员工的userId</param>
    /// <param name="approveId">审批ID</param>
    /// <returns></returns>
    public async Task<AttendanceApprovelsCancelResponse> ApproveCancel(string accessToken, string userid, string approveId)
    {
        var resStr = await _request.ApprovalsCancel(accessToken, new AttendanceApprovelsCancelRequest
        {
            ApproveId = approveId,
            UserId = userid,
        });
        return resStr.ToObject<AttendanceApprovelsCancelResponse>();
    }

    /// <summary>
    /// 查询请假状态
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="userIdList">待查询用户的ID列表，每次最多100个</param>
    /// <param name="startTime">开始时间 ，支持最多180天的查询</param>
    /// <param name="endTime">结束时间，支持最多180天的查询</param>
    /// <param name="offset">支持分页查询，与size参数同时设置时才生效，此参数代表偏移量，偏移量从0开始</param>
    /// <param name="size">支持分页查询，与offset参数同时设置时才生效，此参数代表分页大小，最大20</param>
    /// <returns></returns>
    public async Task<GetLeaveStatusResponse> GetLeaveStatus(string accessToken, List<string> userIdList, DateTime startTime, DateTime endTime, int offset = 0, int size = 10)
    {
        var resStr = await _request.GetLeaveStatus(accessToken, new GetLeaveStatusRequest
        {
            UserIdList = userIdList.Join(),
            StartTime = new DateTimeOffset(startTime).ToUnixTimeMilliseconds(),
            EndTime = new DateTimeOffset(endTime).ToUnixTimeMilliseconds(),
            Offset = offset,
            Size = size
        });
        return resStr.ToObject<GetLeaveStatusResponse>();
    }

    private string TimeToHalfDay(DateTime time)
    {
        var dateStr = time.ToString("yyyy-MM-dd");
        var apm = time.Hour < 12 ? "AM" : "PM";
        return dateStr + " " + apm;
    }
}