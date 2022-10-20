using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Domain.Channels;

[Table("channel")]
public class Channel : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 渠道類別
    /// </summary>
    [Required]
    [Column("channel_type")]
    public ChannelType ChannelType { get; set; }

    /// <summary>
    /// 渠道名稱
    /// </summary>
    [Required]
    [StringLength(30)]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 第三方渠道類別
    /// </summary>
    [Column("third_party_channel_type")]
    public ThirdPartyChannelType ThirdPartyChannelType { get; set; }

    /// <summary>
    /// 第三方渠道id
    /// </summary>
    //[StringLength(36)]
    //[Column("third_party_channel_id")]
    //public string ThirdPartyChannelId { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [StringLength(500)]
    [Column("remark")]
    public string Remark { get; set; }
}