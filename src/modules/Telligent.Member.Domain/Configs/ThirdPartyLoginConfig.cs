using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Domain.Configs;

[Table("third_party_login_config")]
public class ThirdPartyLoginConfig : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 第三方渠道類別(1:Line 2:Google 3:FB)
    /// </summary>
    [Required]
    [Column("third_party_channel_type")]
    public ThirdPartyChannelType ThirdPartyChannelType { get; set; }

    /// <summary>
    /// 第三方渠道識別碼
    /// </summary>
    [StringLength(100)]
    [Column("third_party_channel_id")]
    public string ThirdPartyChannelId { get; set; }

    /// <summary>
    /// 第三方渠道秘鑰
    /// </summary>
    [StringLength(50)]
    [Column("third_party_channel_secret")]
    public string ThirdPartyChannelSecret { get; set; }
}