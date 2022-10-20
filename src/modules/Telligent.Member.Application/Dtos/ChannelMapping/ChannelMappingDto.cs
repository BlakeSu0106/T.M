using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.ChannelMapping;

public class ChannelMappingDto : EntityDto
{
    public Guid ChannelId { get; set; }
    public int ChannelNo { get; set; }
    public Guid OldChannelId { get; set; }
}