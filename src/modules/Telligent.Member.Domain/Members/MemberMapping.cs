using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Members;

[Table("member_mapping")]
public class MemberMapping : Entity
{
    [Column("member_id")] public Guid MemberId { get; set; }
    [Column("member_no")] public int MemberNo { get; set; }
    [Column("old_member_id")] public Guid OldMemberId { get; set; }
}