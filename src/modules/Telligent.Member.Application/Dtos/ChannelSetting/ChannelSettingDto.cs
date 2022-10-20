using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Auth;

namespace Telligent.Member.Application.Dtos.ChannelSetting;

public class ChannelSettingDto : EntityDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 註冊類別
    /// </summary>
    public RegisterType RegisterType { get; set; }

    /// <summary>
    /// 登入方式
    /// </summary>
    public LoginType LoginType { get; set; }
}