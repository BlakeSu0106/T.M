using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.Profile;

public class ProfileDto : EntityDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 是否可以編輯
    /// </summary>
    public bool IsEditable { get; set; }
}