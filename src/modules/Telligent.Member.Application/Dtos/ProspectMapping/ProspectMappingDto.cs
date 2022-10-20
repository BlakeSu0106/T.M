using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.ProspectMapping;

public class ProspectMappingDto : EntityDto
{
    public Guid ProspectId { get; set; }
    public int ProspectiveCustomerNo { get; set; }
    public Guid ProspectiveCustomerId { get; set; }
}