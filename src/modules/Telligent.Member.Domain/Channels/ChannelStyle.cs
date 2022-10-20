using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Member.Domain.Channels;

[Table("channel_style")]
public class ChannelStyle : Entity
{
    /// <summary>
    /// 渠道識別碼
    /// </summary>
    [Required]
    [Column("channel_id")]
    public Guid ChannelId { get; set; }

    /// <summary>
    /// Logo 連結
    /// </summary>
    [Column("logo_url")]
    public string LogoUrl { get; set; }

    /// <summary>
    /// 主視圖圖檔連結
    /// </summary>
    [Column("key_visual_image_url")]
    public string KeyVisualImageUrl { get; set; }

    /// <summary>
    /// 背景色
    /// </summary>
    [Column("background_color")]
    public string BackgroundColor { get; set; }

    /// <summary>
    /// 按鍵色
    /// </summary>
    [Column("button_color")]
    public string ButtonColor { get; set; }

    /// <summary>
    /// 按鍵字體色
    /// </summary>
    [Column("button_fore_color")]
    public string ButtonForeColor { get; set; }

    /// <summary>
    /// 按鍵邊框色
    /// </summary>
    [Column("button_border_color")]
    public string ButtonBorderColor { get; set; }

    /// <summary>
    /// Panel 背景色
    /// </summary>
    [Column("panel_background_color")]
    public string PanelBackgroundColor { get; set; }
}