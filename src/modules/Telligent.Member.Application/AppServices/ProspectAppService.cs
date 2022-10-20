using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Prospect;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Prospect;

namespace Telligent.Member.Application.AppServices;

public class ProspectAppService : CrudAppService<Prospect, ProspectDto, CreateProspectDto, UpdateProspectDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public ProspectAppService(
        IRepository<Prospect> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    public override async Task<ProspectDto> CreateAsync(CreateProspectDto dto)
    {
        if (Repository.GetAsync(p =>
                p.TenantId.Equals(Payload.TenantId) && p.CompanyId.Equals(dto.CompanyId) &&
                p.ChannelId.Equals(dto.ChannelId) && p.ThirdPartyUserId.Equals(dto.ThirdPartyUserId) && p.EntityStatus) == null)
            throw new ValidationException("prospect is exist");

        return await base.CreateAsync(dto);
    }
}