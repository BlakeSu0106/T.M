using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Account;

/// <summary>
/// 驗證碼
/// </summary>
[Table("account_captcha")]
public class AccountCaptcha : Entity
{
    /// <summary>
    /// 驗證目標
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("key")]
    public string Key { get; set; }

    /// <summary>
    /// 驗證碼
    /// </summary>
    [StringLength(10)]
    [Column("value")]
    public string Value { get; set; }
}