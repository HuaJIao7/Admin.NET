﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.ProblemComment.Dto;

/// <summary>
/// 问题评论输出参数
/// </summary>
public class ProblemCommentDto:Template
{
    /// <summary>
    /// 评论人id
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 评论人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 评论人部门id
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 评论人部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime? CommentTime { get; set; }

}

