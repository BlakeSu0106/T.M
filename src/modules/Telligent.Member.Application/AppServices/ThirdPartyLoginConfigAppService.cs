using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Config;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Configs;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Application.AppServices;

public class ThirdPartyLoginConfigAppService : CrudAppService<ThirdPartyLoginConfig, ThirdPartyLoginConfigDto,
    ThirdPartyLoginConfigDto
    , ThirdPartyLoginConfigDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public ThirdPartyLoginConfigAppService(
        IRepository<ThirdPartyLoginConfig> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    /// <summary>
    /// 取第三方登入設定
    /// </summary>
    /// <param name="companyId">公司識別碼</param>
    /// <param name="type">第三方登入類別</param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<ThirdPartyLoginConfigDto> GetThirdPartyLoginConfigAsync(Guid companyId,
        ThirdPartyChannelType type)
    {
        var entity = await _uow.ThirdPartyLoginConfigRepository.GetAsync(t =>
            t.CompanyId.Equals(companyId) && t.ThirdPartyChannelType.Equals(type) && t.EntityStatus);

        if (entity == null)
            throw new ValidationException("third party login config is null");

        return Mapper.Map<ThirdPartyLoginConfigDto>(entity);
    }
}