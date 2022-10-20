using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.ProspectMapping;

public class ProspectMappingDtos 
{
    public Guid ProspectId { get; set; }
    public Guid prospectiveCustomerId { get; set; }
}