using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ThirdPartyLoginConfigAppService _configAppService;

        public ConfigController(ThirdPartyLoginConfigAppService configAppService)
        {
            _configAppService = configAppService;
        }

        /// <summary>
        /// 取第三方登入設定
        /// </summary>
        /// <param name="companyId">公司識別碼</param>
        /// <param name="type">第三方登入類別</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetThirdPartyLoginConfigAsync(Guid companyId, ThirdPartyChannelType type)
        {
            return Ok(await _configAppService.GetThirdPartyLoginConfigAsync(companyId, type));
        }
    }
}
