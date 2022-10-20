using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.CompanyMapping;
using Telligent.Member.Domain.Organizations;

namespace Telligent.Member.Application.AppServices;

public class CompanyMappingAppService : CrudAppService<CompanyMapping, CompanyMappingDto, CompanyMappingDto, CompanyMappingDto>
{
    public CompanyMappingAppService(
        IRepository<CompanyMapping> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }

    public async Task<CompanyMappingDto> GetByOldCompanyIdAsync(string id)
    {
        return await GetAsync(m => 
            m.OldCompanyId.Equals(id)&&
            m.EntityStatus);
    }
}