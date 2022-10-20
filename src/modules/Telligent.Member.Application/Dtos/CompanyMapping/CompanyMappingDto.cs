using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.CompanyMapping;

public class CompanyMappingDto : EntityDto
{
    public Guid CompanyId { get; set; }
    public int CompanyNo { get; set; }
    public string OldCompanyId { get; set; }
}