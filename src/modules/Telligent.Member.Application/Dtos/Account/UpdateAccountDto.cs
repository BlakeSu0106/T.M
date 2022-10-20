using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.Account;

public class UpdateAccountDto : EntityDto
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
}