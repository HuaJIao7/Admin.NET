// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Plugin.DingTalk.RequestProxy.Attendance.DTO;

/// <summary>
/// 考勤审批单类型，可取值：1：加班，2：出差、外出，3：请假
/// </summary>
[SuppressSniffer]
public enum AttendanceBizTypeEnum
{
    加班 = 1,
    出差 = 2,
    请假 = 3
}

/// <summary>
/// 考勤审批单时长单位，支持格式如下：day，halfDay，hour：biz_type为1时仅支持hour
/// </summary>
[SuppressSniffer]
public enum AttendanceDurationUnitEnum
{
    Day,
    HalfDay,
    Hour
}

/// <summary>
/// 考勤审批单计算方法：0：按自然日计算，1：按工作日计算
/// </summary>
[SuppressSniffer]
public enum AttendanceCalculateModelEnum
{
    按自然日计算 = 0,
    按工作日计算 = 1
}