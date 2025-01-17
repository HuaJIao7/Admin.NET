// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// 系统应用表
/// </summary>
[SysTable]
[SugarTable(null, "系统应用表")]
public partial class SysApp : EntityBase
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnDescription = "名称", Length = 32), Required, MaxLength(32)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(ColumnDescription = "图标", Length = 256), Required, MaxLength(256)]
    public virtual string? Logo { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn(ColumnDescription = "标题", Length = 32), MaxLength(32)]
    public string Title { get; set; }

    /// <summary>
    /// 副标题
    /// </summary>
    [SugarColumn(ColumnDescription = "副标题", Length = 32), MaxLength(32)]
    public string ViceTitle { get; set; }

    /// <summary>
    /// 副描述
    /// </summary>
    [SugarColumn(ColumnDescription = "副描述", Length = 64), MaxLength(64)]
    public string? ViceDesc { get; set; }

    /// <summary>
    /// 水印
    /// </summary>
    [SugarColumn(ColumnDescription = "水印", Length = 32), MaxLength(32)]
    public string? Watermark { get; set; }

    /// <summary>
    /// 版权信息
    /// </summary>
    [SugarColumn(ColumnDescription = "版权信息", Length = 64), MaxLength(64)]
    public string? Copyright { get; set; }

    /// <summary>
    /// ICP备案号
    /// </summary>
    [SugarColumn(ColumnDescription = "ICP备案号", Length = 32), MaxLength(32)]
    public string? Icp { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [IgnoreUpdateSeedColumn]
    [SugarColumn(ColumnDescription = "排序")]
    public int OrderNo { get; set; } = 100;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnDescription = "备注", Length = 256), MaxLength(256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 应用租户
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(SysTenant.AppId))]
    public List<SysTenant> TenantList { get; set; }
}