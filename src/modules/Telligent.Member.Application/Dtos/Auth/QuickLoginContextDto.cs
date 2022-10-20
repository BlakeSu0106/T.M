using System.ComponentModel.DataAnnotations;

namespace Telligent.Member.Application.Dtos.Auth;

public class QuickLoginContextDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public string CompanyId { get; set; }

    /// <summary>
    /// 使用者帳號
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// ReturnUrl
    /// </summary>
    public string ReturnUrl { get; set; }
}