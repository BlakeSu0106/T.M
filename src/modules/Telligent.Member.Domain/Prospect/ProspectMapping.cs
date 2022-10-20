using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Prospect;

[Table("prospect_mapping")]
public class ProspectMapping : Entity
{
    [Column("prospect_id")] public Guid ProspectId { get; set; }
    [Column("prospective_customer_no")] public int ProspectiveCustomerNo { get; set; }
    [Column("prospective_customer_id")] public Guid ProspectiveCustomerId { get; set; }
}