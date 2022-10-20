using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Prospect;
using Telligent.Member.Application.Dtos.ProspectMapping;
using Telligent.Member.Domain.Prospect;

namespace Telligent.Member.Application.AppServices;

public class
    ProspectMappingAppService : CrudAppService<ProspectMapping, ProspectMappingDto, ProspectMappingDto,
        ProspectMappingDto>
{
    public ProspectMappingAppService(
        IRepository<ProspectMapping> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }

    public async Task<IList<ProspectMappingDto>> GetByProspectiveCustomerNoAsync(List<int> nos)
    {
        return await GetListAsync(m =>
            nos.Contains(m.ProspectiveCustomerNo) &&
            m.EntityStatus);
    }
    public async Task<IList<ProspectMappingDtos>> GetByOldProspecIdAsync(ProspectsDto dto)
    {
        var prospectMappingDtos = await GetListAsync(m =>
            dto.Prospects.Contains(m.ProspectiveCustomerId) &&
            m.EntityStatus);
        return prospectMappingDtos.Select(p => new ProspectMappingDtos()
        {
            ProspectId = p.ProspectId,
            prospectiveCustomerId = p.ProspectiveCustomerId
        }).ToList();

    }
}