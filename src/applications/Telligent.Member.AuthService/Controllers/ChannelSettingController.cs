using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;

namespace Telligent.Member.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelSettingController : ControllerBase
    {
        private readonly ChannelSettingAppService _channelSettingService;

        public ChannelSettingController(ChannelSettingAppService channelSettingService)
        {
            _channelSettingService = channelSettingService;
        }

        /// <summary>
        /// 透過渠道識別碼取渠道相關設定
        /// </summary>
        /// <param name="channelId">渠道識別碼</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetChannelSettingAsync(Guid channelId)
        {
            return Ok(await _channelSettingService.GetByChannelIdAsync(channelId));
        }
    }
}
