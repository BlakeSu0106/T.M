using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Users;

/// <summary>
/// 維護人員主檔
/// </summary>
[Table("user")]
public class User : Entity
{
    /// <summary>
    /// 帳號
    /// </summary>
    [Column("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 手機號碼
    /// </summary>
    [Column("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 電子信箱
    /// </summary>
    [Column("email")]
    public string Email { get; set; }
}