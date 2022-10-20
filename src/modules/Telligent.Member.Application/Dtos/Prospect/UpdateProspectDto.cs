using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.Prospect;

public class UpdateProspectDto : EntityDto
{
    /// <summary>
    /// 暱稱
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 是否訂閱官方帳號(頻道)
    /// </summary>
    public bool? IsSubscribedChannel { get; set; }
}