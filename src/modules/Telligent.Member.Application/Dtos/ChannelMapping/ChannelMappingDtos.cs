using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.ChannelMapping;

public class ChannelMappingDtos 
{
    public Guid ChannelId { get; set; }
    public Guid OldChannelId { get; set; }
}