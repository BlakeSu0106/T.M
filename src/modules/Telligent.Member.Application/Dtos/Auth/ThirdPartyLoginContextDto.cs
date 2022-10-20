using System.ComponentModel.DataAnnotations;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Application.Dtos.Auth;

public class ThirdPartyLoginContextDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public string CompanyId { get; set; }

    /// <summary>
    /// 第三方渠道類別
    /// </summary>
    [Required]
    public ThirdPartyChannelType ThirdPartyChannelType { get; set; }

    /// <summary>
    /// Authorization Code
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// RedirectUri
    /// </summary>
    public string RedirectUri { get; set; }

    /// <summary>
    /// ReturnUrl
    /// </summary>
    public string ReturnUrl { get; set; }
}