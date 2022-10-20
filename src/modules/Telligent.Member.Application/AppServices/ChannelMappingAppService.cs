using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.ChannelMapping;
using Telligent.Member.Domain.Channels;
using Channel = Telligent.Member.Domain.Channels.Channel;

namespace Telligent.Member.Application.AppServices;

public class
    ChannelMappingAppService : CrudAppService<ChannelMapping, ChannelMappingDto, ChannelMappingDto, ChannelMappingDto>
{
    public ChannelMappingAppService(
        IRepository<ChannelMapping> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }

    public async Task<IList<ChannelMappingDtos>> GetByOldChannelIdAsync(Channels ids)
    {
        var channelMappingDtos = await GetListAsync(m =>
            ids.ChannelIds.Contains(m.OldChannelId) &&
            m.EntityStatus);
       return channelMappingDtos.Select(c => new ChannelMappingDtos()
        {
            ChannelId = c.ChannelId,
            OldChannelId = c.OldChannelId
        }).ToList();
    }


}