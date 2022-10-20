using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.ChannelStyle;

public class ChannelStyleDto : EntityDto
{
    /// <summary>
    /// Logo 連結
    /// </summary>
    public string LogoUrl { get; set; }

    /// <summary>
    /// 主視圖圖檔連結
    /// </summary>
    public string KeyVisualImageUrl { get; set; }

    /// <summary>
    /// 背景色
    /// </summary>
    public string BackgroundColor { get; set; }

    /// <summary>
    /// 按鍵色
    /// </summary>
    public string ButtonColor { get; set; }

    /// <summary>
    /// 按鍵字體色
    /// </summary>
    public string ButtonForeColor { get; set; }

    /// <summary>
    /// 按鍵邊框色
    /// </summary>
    public string ButtonBorderColor { get; set; }

    /// <summary>
    /// Panel 背景色
    /// </summary>
    public string PanelBackgroundColor { get; set; }
}