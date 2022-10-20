using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Profile;

namespace Telligent.Member.Application.Dtos.Profile;

public class CustomProfileDto : EntityDto
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 顯示名稱
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 是否可以編輯
    /// </summary>
    public bool IsEditable { get; set; }

    /// <summary>
    /// 組件類型
    /// </summary>
    public ComponentType ComponentType { get; set; }

    /// <summary>
    /// 項目內容
    /// </summary>
    public IList<CustomProfileItemDto> Items { get; set; }
}