using System.ComponentModel.DataAnnotations;
using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Application.Dtos.Account;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.Dtos.Member;

public class CreateMemberDto : EntityDto
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 會籍識別碼
    /// </summary>
    public Guid? MembershipId { get; set; }

    /// <summary>
    /// 客戶端識別碼
    /// </summary>
    [StringLength(36)]
    public string ClientUserId { get; set; }

    /// <summary>
    /// 會員姓名
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// 電子郵件
    /// </summary>
    [StringLength(100)]
    public string Email { get; set; }

    /// <summary>
    /// 會員手機
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Mobile { get; set; }

    /// <summary>
    /// 會員電話
    /// </summary>
    [StringLength(50)]
    public string Phone { get; set; }

    /// <summary>
    /// 國別
    /// </summary>
    [StringLength(2)]
    public string CountryCode { get; set; }

    /// <summary>
    /// 電話國碼
    /// </summary>
    [StringLength(10)]
    public string CountryCallingCode { get; set; }

    /// <summary>
    /// 電話區碼
    /// </summary>
    [StringLength(5)]
    public string AreaCallingCode { get; set; }

    /// <summary>
    /// 會員生日
    /// </summary>
    [Required]
    public DateTime Birth { get; set; }

    /// <summary>
    /// 會員性別
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// 身份代碼
    /// </summary>
    [StringLength(20)]
    public string PersonalId { get; set; }

    /// <summary>
    /// 郵遞區碼
    /// </summary>
    public int? ZipCode { get; set; }

    /// <summary>
    /// 國家
    /// </summary>
    [StringLength(50)]
    public string Country { get; set; }

    /// <summary>
    /// 省/直轄市/州
    /// </summary>
    [StringLength(50)]
    public string Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [StringLength(50)]
    public string City { get; set; }

    /// <summary>
    /// 區
    /// </summary>
    [StringLength(50)]
    public string District { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [StringLength(100)]
    public string Address { get; set; }

    /// <summary>
    /// 用戶頭像
    /// </summary>
    [StringLength(200)]
    public string Avatar { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [StringLength(500)]
    public string Remark { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    public CreateAccountDto Account { get; set; }
}