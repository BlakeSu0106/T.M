using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Domain.Members;

[Table("membership")]
public class Membership : Entity
{
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("previous_membership_id")]
    public Guid? PreviousMembershipId { get; set; }

    [Column("threshold_amount")]
    public int ThresholdAmount { get; set; }

    [Column("expiration_type")]
    public ExpirationType ExpirationType { get; set; }

    [Column("icon_url")]
    public string IconUrl { get; set; }
}