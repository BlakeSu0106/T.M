using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.Dtos.Membership;

public class MembershipDto : EntityDto
{
    public string Name { get; set; }

    internal Guid? PreviousMembershipId { get; set; }

    public MembershipDto PreviousMembership { get; set; }

    public int ThresholdAmount { get; set; }

    public ExpirationType ExpirationType { get; set; }

    public string IconUrl { get; set; }
}