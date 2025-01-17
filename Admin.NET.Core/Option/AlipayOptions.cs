// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// 支付宝支付配置选项
/// </summary>
public sealed class AlipayOptions : IConfigurableOptions
{
    /// <summary>
    /// 支付宝 APPID（必填）
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// 支付宝 websocket 服务地址 （必填）
    /// </summary>
    public string AlipayWebsocketUrl { get; set; }

    /// <summary>
    /// 支付宝网关地址 （必填）
    /// </summary>
    public string ServerUrl { get; set; }

    /// <summary>
    /// 支付宝授权回调地址 （必填）
    /// </summary>
    public string AuthUrl { get; set; }

    /// <summary>
    /// 应用回调地址
    /// </summary>
    public string NotifyUrl { get; set; }

    /// <summary>
    /// 加密算法（必填）
    /// </summary>
    public string SignType { get; set; }

    /// <summary>
    /// 从支付宝获取敏感信息时的加密密钥（可选）
    /// </summary>
    public string EncryptKey { get; set; }

    /// <summary>
    /// 应用私钥 （必填）
    /// </summary>
    public string PrivateKey { get; set; }

    /// <summary>
    /// 支付宝公钥证书存放路径（证书加签方式必填）
    /// </summary>
    public string AlipayPublicCertPath { get; set; }

    /// <summary>
    /// 支付宝根证书存放路径（证书加签方式必填）
    /// </summary>
    public string RootCertPath { get; set; }

    /// <summary>
    /// 应用公钥证书存放路径（证书加签方式必填）
    /// </summary>
    public string AppCertPath { get; set; }
}