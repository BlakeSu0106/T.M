using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Organizations;

[Table("company_mapping")]
public class CompanyMapping : Entity
{
    [Column("company_id")] public Guid CompanyId { get; set; }
    [Column("company_no")] public int CompanyNo { get; set; }
    [Column("old_company_id")] public string OldCompanyId { get; set; }
}