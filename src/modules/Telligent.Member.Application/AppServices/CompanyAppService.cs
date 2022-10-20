using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Organization;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Organizations;

namespace Telligent.Member.Application.AppServices;

public class CompanyAppService : CrudAppService<Company, CompanyDto, CompanyDto, CompanyDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public CompanyAppService(
        IRepository<Company> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }
}