using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.MemberMapping;

public class MemberMappingDto : EntityDto
{
    public Guid MemberId { get; set; }
    public Guid OldMemberId { get; set; }
}