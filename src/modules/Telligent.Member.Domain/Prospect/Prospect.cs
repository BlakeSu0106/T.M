using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Domain.Prospect;

[Table("prospect")]
public class Prospect : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 渠道來源識別碼
    /// </summary>
    [Required]
    [Column("channel_id")]
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 會員識別碼
    /// </summary>
    [Column("member_id")]
    public Guid? MemberId { get; set; }

    /// <summary>
    /// 帳號來源類別
    /// </summary>
    [Required]
    [Column("account_type")]
    public AccountType AccountType { get; set; }

    /// <summary>
    /// 第三方 Uid
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("third_party_user_id")]
    public string ThirdPartyUserId { get; set; }

    /// <summary>
    /// 暱稱
    /// </summary>
    [Column("nickname")]
    public string Nickname { get; set; }

    /// <summary>
    /// 是否訂閱官方帳號(頻道)
    /// </summary>
    [Column("is_subscribed_channel")]
    public bool? IsSubscribedChannel { get; set; }
}