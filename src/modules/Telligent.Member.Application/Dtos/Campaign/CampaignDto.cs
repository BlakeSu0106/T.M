namespace Telligent.Member.Application.Dtos.Campaign;

public class CampaignDto
{
    public List<Guid> MemberIds { get; set; }
    public List<Guid> ProspectIds { get; set; }
}