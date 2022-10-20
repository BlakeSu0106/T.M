using System.ComponentModel.DataAnnotations;

namespace Telligent.Member.Application.Dtos.Auth;

public class LoginContextDto
{
    public string CompanyId { get; set; }

    /// <summary>
    /// 使用者帳號
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// 使用者密碼
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// ReturnUrl
    /// </summary>
    public string ReturnUrl { get; set; }
}