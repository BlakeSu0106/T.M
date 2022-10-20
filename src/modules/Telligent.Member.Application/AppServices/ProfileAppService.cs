using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Profile;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Shared.Profile;
using Profile = Telligent.Member.Domain.Profiles.Profile;

namespace Telligent.Member.Application.AppServices;

public class ProfileAppService : CrudAppService<Profile, ProfileDto, ProfileDto, ProfileDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public ProfileAppService(
        IRepository<Profile> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    /// <summary>
    /// 透過公司與渠道取資料模板
    /// </summary>
    /// <param name="companyId">公司識別碼</param>
    /// <param name="channelId">渠道識別碼</param>
    /// <param name="type">模板類別</param>
    /// <returns></returns>
    public async Task<ProfileMappingDto> GetProfileMappingAsync(Guid companyId, Guid channelId, ProfileType type)
    {
        var profiles = Mapper.Map<IList<ProfileDto>>(await Repository.GetListAsync(p =>
            p.CompanyId.Equals(companyId) && p.ChannelId.Equals(channelId) && p.ProfileType.Equals(type) &&
            p.EntityStatus));

        var customProfiles = Mapper.Map<IList<CustomProfileDto>>(await _uow.CustomProfileRepository.GetListAsync(cp =>
            cp.CompanyId.Equals(companyId) && cp.ChannelId.Equals(channelId) && cp.ProfileType.Equals(type) &&
            cp.EntityStatus));

        foreach (var entity in customProfiles)
        {
            entity.Items = Mapper.Map<IList<CustomProfileItemDto>>(
                await _uow.CustomProfileItemRepository.GetListAsync(cpi =>
                    cpi.CustomProfileId.Equals(entity.Id) && cpi.EntityStatus));
        }

        return new ProfileMappingDto { Profiles = profiles, CustomProfiles = customProfiles};
    }
}