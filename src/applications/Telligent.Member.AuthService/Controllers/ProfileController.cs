using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Domain.Shared.Profile;

namespace Telligent.Member.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileAppService _profileAppService;

        public ProfileController(ProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }

        /// <summary>
        /// 透過公司與渠道取註冊資料模板
        /// </summary>
        /// <param name="companyId">公司識別碼</param>
        /// <param name="channelId">渠道識別碼</param>
        /// <returns></returns>
        [HttpGet("register")]
        public async Task<IActionResult> GetProfileMappingForRegisterAsync(Guid companyId, Guid channelId)
        {
            return Ok(await _profileAppService.GetProfileMappingAsync(companyId, channelId, ProfileType.Register));
        }

        /// <summary>
        /// 透過公司與渠道取維護資料模板
        /// </summary>
        /// <param name="companyId">公司識別碼</param>
        /// <param name="channelId">渠道識別碼</param>
        /// <returns></returns>
        [HttpGet("edit")]
        public async Task<IActionResult> GetProfileMappingForEditAsync(Guid companyId, Guid channelId)
        {
            return Ok(await _profileAppService.GetProfileMappingAsync(companyId, channelId, ProfileType.Edit));
        }
    }
}
