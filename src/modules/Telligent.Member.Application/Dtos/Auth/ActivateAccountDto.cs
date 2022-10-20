using Telligent.Member.Application.Dtos.AccountCaptcha;

namespace Telligent.Member.Application.Dtos.Auth;

public class ActivateAccountDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 驗證碼檢核 Dto
    /// </summary>
    public ValidateAccountCaptchaDto ValidateAccountCaptchaDto { get; set; }
}