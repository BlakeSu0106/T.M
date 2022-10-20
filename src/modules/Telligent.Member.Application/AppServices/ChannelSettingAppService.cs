using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.ChannelSetting;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Channels;

namespace Telligent.Member.Application.AppServices;

public class
    ChannelSettingAppService : CrudAppService<ChannelSetting, ChannelSettingDto, ChannelSettingDto, ChannelSettingDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public ChannelSettingAppService(
        IRepository<ChannelSetting> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    public async Task<ChannelSettingDto> GetByChannelIdAsync(Guid channelId)
    {
        var entity = await _uow.ChannelSettingRepository.GetAsync(cs => cs.ChannelId.Equals(channelId));

        if (entity == null)
            throw new ValidationException("channel setting is null");

        return Mapper.Map<ChannelSettingDto>(entity);
    }
}