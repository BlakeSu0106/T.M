using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Profiles;

/// <summary>
/// 客製會員資料模板項目
/// </summary>
[Table("custom_profile_item")]
public class CustomProfileItem : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("custom_profile_id")]
    public Guid CustomProfileId { get; set; }

    /// <summary>
    /// 排序編號
    /// </summary>
    [Column("seq_no")]
    public int? SeqNo { get; set; }

    /// <summary>
    /// 項目內容
    /// </summary>
    [Required]
    [StringLength(400)]
    [Column("value")]
    public string Value { get; set; }
}