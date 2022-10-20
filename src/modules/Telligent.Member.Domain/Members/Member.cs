using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Domain.Members;

/// <summary>
/// 會員主檔
/// </summary>
[Table("member")]
public class Member : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 會籍識別碼
    /// </summary>
    [Required]
    [Column("membership_id")]
    public Guid? MembershipId { get; set; }

    /// <summary>
    /// 客戶端識別碼
    /// </summary>
    [Column("client_user_id")]
    public string ClientUserId { get; set; }

    /// <summary>
    /// 會員姓名
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 會員英文名
    /// </summary>
    [StringLength(50)]
    [Column("english_name")]
    public string EnglishName { get; set; }

    /// <summary>
    /// 電子郵件
    /// </summary>
    [StringLength(100)]
    [Column("email")]
    public string Email { get; set; }

    /// <summary>
    /// 會員手機
    /// </summary>
    [Required]
    [StringLength(20)]
    [Column("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 會員電話
    /// </summary>
    [StringLength(50)]
    [Column("phone")]
    public string Phone { get; set; }

    /// <summary>
    /// 國別
    /// </summary>
    [StringLength(2)]
    [Column("country_code")]
    public string CountryCode { get; set; }

    /// <summary>
    /// 電話國碼
    /// </summary>
    [StringLength(10)]
    [Column("country_calling_code")]
    public string CountryCallingCode { get; set; }

    /// <summary>
    /// 電話區碼
    /// </summary>
    [StringLength(5)]
    [Column("area_calling_code")]
    public string AreaCallingCode { get; set; }

    /// <summary>
    /// 會員生日
    /// </summary>
    [Required]
    [Column("birth")]
    public DateTime Birth { get; set; }

    /// <summary>
    /// 會員性別
    /// </summary>
    [Column("gender")]
    public Gender? Gender { get; set; }

    /// <summary>
    /// 身份代碼
    /// </summary>
    [StringLength(20)]
    [Column("personal_id")]
    public string PersonalId { get; set; }

    /// <summary>
    /// 郵遞區碼
    /// </summary>
    [Column("zip_code")]
    public int? ZipCode { get; set; }

    /// <summary>
    /// 國家
    /// </summary>
    [StringLength(50)]
    [Column("country")]
    public string Country { get; set; }

    /// <summary>
    /// 省/直轄市/州
    /// </summary>
    [StringLength(50)]
    [Column("province")]
    public string Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [StringLength(50)]
    [Column("city")]
    public string City { get; set; }

    /// <summary>
    /// 區
    /// </summary>
    [StringLength(50)]
    [Column("district")]
    public string District { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [StringLength(100)]
    [Column("address")]
    public string Address { get; set; }

    /// <summary>
    /// 用戶頭像
    /// </summary>
    [StringLength(200)]
    [Column("avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [StringLength(500)]
    [Column("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// 註冊時間
    /// </summary>
    [Required]
    [Column("registration_time")]
    public DateTime RegistrationTime { get; set; }
}