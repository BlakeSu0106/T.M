using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.AccountCaptcha;

namespace Telligent.Member.AuthService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaptchaController : ControllerBase
{
    private readonly AccountCaptchaAppService _accountCaptchaService;

    public CaptchaController(AccountCaptchaAppService accountCaptchaService)
    {
        _accountCaptchaService = accountCaptchaService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendCaptcha(CreateAccountCaptchaDto dto)
    {
        try
        {
            return Ok(await _accountCaptchaService.SendCaptchaAsync(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCaptcha(ValidateAccountCaptchaDto dto)
    {
        try
        {
            return Ok(await _accountCaptchaService.ValidateCaptchaAsync(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}