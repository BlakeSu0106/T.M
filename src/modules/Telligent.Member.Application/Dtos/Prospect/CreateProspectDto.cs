using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.Dtos.Prospect;

public class CreateProspectDto : EntityDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 渠道來源識別碼
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 帳號來源類別
    /// </summary>
    public AccountType AccountType { get; set; }

    /// <summary>
    /// 第三方 Uid
    /// </summary>
    public string ThirdPartyUserId { get; set; }

    /// <summary>
    /// 暱稱
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 是否訂閱官方帳號(頻道)
    /// </summary>
    public bool? IsSubscribedChannel { get; set; }
}