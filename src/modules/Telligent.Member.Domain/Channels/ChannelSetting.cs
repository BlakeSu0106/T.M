using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;
using Telligent.Member.Domain.Shared.Auth;

namespace Telligent.Member.Domain.Channels;

[Table("channel_setting")]
public class ChannelSetting : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Required]
    [Column("channel_id")]
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 註冊類別
    /// </summary>
    [Required]
    [Column("register_type")]
    public RegisterType RegisterType { get; set; }

    /// <summary>
    /// 登入類別
    /// </summary>
    [Required]
    [Column("login_type")]
    public LoginType LoginType { get; set; }
}