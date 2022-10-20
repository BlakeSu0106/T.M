using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.Config;

public class ThirdPartyLoginConfigDto : EntityDto
{
    /// <summary>
    /// 第三方渠道識別碼
    /// </summary>
    public string ThirdPartyChannelId { get; set; }

    /// <summary>
    /// 第三方渠道秘鑰
    /// </summary>
    public string ThirdPartyChannelSecret { get; set; }
}