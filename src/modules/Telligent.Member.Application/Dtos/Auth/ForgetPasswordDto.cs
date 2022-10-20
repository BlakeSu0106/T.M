using Telligent.Member.Application.Dtos.AccountCaptcha;

namespace Telligent.Member.Application.Dtos.Auth
{
    public class ForgetPasswordDto
    {
        /// <summary>
        /// 公司識別碼
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>

        public string UserId { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        public ValidateAccountCaptchaDto ValidateAccountCaptchaDto { get; set; }
    }
}
