using System.ComponentModel.DataAnnotations;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.Membership;

public class CreateMembershipDto : EntityDto
{
    internal new Guid Id { get; set; }

    [Required]
    public Guid CompanyId { get; set; }

    [Required]
    public string Name { get; set; }

    public Guid? PreviousMembershipId { get; set; }

    public int ThresholdAmount { get; set; }

    public string IconUrl { get; set; }
}