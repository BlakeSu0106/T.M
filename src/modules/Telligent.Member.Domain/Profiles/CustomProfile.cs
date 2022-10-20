using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Profile;

namespace Telligent.Member.Domain.Profiles;

/// <summary>
/// 會員資料模板
/// </summary>
[Table("custom_profile")]
public class CustomProfile : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 渠道識別碼
    /// </summary>
    [Required]
    [Column("channel_id")]
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 模板類型(註冊/維護)
    /// </summary>
    [Required]
    [Column("profile_type")]
    public ProfileType ProfileType { get; set; }

    /// <summary>
    /// 排序編號
    /// </summary>
    [Column("seq_no")]
    public int? SeqNo { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 顯示名稱
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("title")]
    public string Title { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    [Required]
    [Column("is_required")]
    public bool IsRequired { get; set; }

    /// <summary>
    /// 是否可以編輯
    /// </summary>
    [Required]
    [Column("is_editable")]
    public bool IsEditable { get; set; }


    /// <summary>
    /// 組件類型
    /// </summary>
    [Required]
    [Column("component_type")]
    public ComponentType ComponentType { get; set; }
}