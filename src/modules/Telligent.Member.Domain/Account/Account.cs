using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Domain.Account;

/// <summary>
/// 帳號主檔
/// </summary>
[Table("account")]
public class Account : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 會員代碼
    /// </summary>
    [Required]
    [Column("member_id")]
    public Guid MemberId { get; set; }

    /// <summary>
    /// 帳號來源類別
    /// </summary>
    [Required]
    [Column("account_type")]
    public AccountType AccountType { get; set; }

    /// <summary>
    /// 登入帳號
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    [StringLength(200)]
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// 唯一識別碼
    /// </summary>
    [Required]
    [StringLength(36)]
    [Column("uuid")]
    public string Uuid { get; set; }

    /// <summary>
    /// 渠道識別碼
    /// </summary>
    [Column("channel_id")]
    public Guid? ChannelId { get; set; }

    /// <summary>
    /// 是否訂閱官方帳號(頻道)
    /// </summary>
    [Column("is_subscribed_channel")]
    public bool? IsSubscribedChannel { get; set; }

    /// <summary>
    /// 帳號狀態
    /// </summary>
    [Column("account_status")]
    public AccountStatus AccountStatus { get; set; }
}