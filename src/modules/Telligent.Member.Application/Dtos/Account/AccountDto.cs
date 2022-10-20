using System.ComponentModel.DataAnnotations;
using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.Dtos.Account;

public class AccountDto : EntityDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 會員代碼
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// 帳號來源類別
    /// </summary>
    public AccountType AccountType { get; set; }

    /// <summary>
    /// 登入帳號
    /// </summary>
    [Required]
    public string UserId { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 唯一識別碼
    /// </summary>
    public string Uuid { get; set; }

    /// <summary>
    /// 頻道識別碼
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 是否訂閱官方帳號(頻道)
    /// </summary>
    public bool? IsSubscribedChannel { get; set; }
}