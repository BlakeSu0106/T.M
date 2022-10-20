using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Channels;

[Table("channel_mapping")]
public class ChannelMapping : Entity
{
    [Column("channel_id")] public Guid ChannelId { get; set; }
    [Column("member_source_no")] public int ChannelNo { get; set; }
    [Column("member_source_id")] public Guid OldChannelId { get; set; }
}