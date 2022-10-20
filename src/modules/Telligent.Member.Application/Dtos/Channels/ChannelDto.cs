using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Channels;
namespace Telligent.Member.Application.Dtos.Channels;

public class ChannelDto : EntityDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public Guid CompanyId { get; set; }
    /// <summary>
    /// 渠道識別碼
    /// </summary>
    public ChannelType ChannelType { get; set; }



}