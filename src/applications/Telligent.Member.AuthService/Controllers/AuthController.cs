using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.Account;
using Telligent.Member.Application.Dtos.Auth;
using Telligent.Member.Application.Dtos.Member;

namespace Telligent.Member.AuthService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AccountAppService _accountService;
    private readonly AuthAppService _authService;
    private readonly AccountCaptchaAppService _captchaAppService;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly MemberAppService _memberService;

    public AuthController(
        IIdentityServerInteractionService interaction,
        AuthAppService authService,
        MemberAppService memberService,
        AccountAppService accountService,
        AccountCaptchaAppService captchaAppService)
    {
        _interaction = interaction;
        _authService = authService;
        _memberService = memberService;
        _accountService = accountService;
        _captchaAppService = captchaAppService;
    }

    /// <summary>
    /// 一般登入
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginContextDto context)
    {
        var result = await _authService.ValidateAsync(context.CompanyId, context.UserId, context.Password);

        if (result.IsError) return Unauthorized(result.ErrorDescription);

        var user = new IdentityServerUser(result.Subject.Claims.First(c => c.Type.Equals(JwtClaimTypes.Id)).Value);

        foreach (var claim in result.Subject.Claims) user.AdditionalClaims.Add(claim);

        await HttpContext.SignInAsync(user);

        return new JsonResult(new { RedirectUrl = context.ReturnUrl, IsOk = true });
    }

    /// <summary>
    /// 快速登入
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [HttpPost("quick-login")]
    public async Task<IActionResult> QuickLoginAsync(QuickLoginContextDto context)
    {
        var result = await _authService.ValidateAsync(context.CompanyId, context.UserId);

        if (result.IsError) return Unauthorized(result.ErrorDescription);

        var user = new IdentityServerUser(result.Subject.Claims.First(c => c.Type.Equals(JwtClaimTypes.Id)).Value);

        foreach (var claim in result.Subject.Claims) user.AdditionalClaims.Add(claim);

        await HttpContext.SignInAsync(user);

        return new JsonResult(new { RedirectUrl = context.ReturnUrl, IsOk = true });
    }

    /// <summary>
    /// 第三方登入
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [HttpPost("third-party-login")]
    public async Task<IActionResult> ThirdPartyLoginAsync(ThirdPartyLoginContextDto context)
    {
        var result = await _authService.ValidateAsync(context.CompanyId, context.ThirdPartyChannelType, context.Code,
            context.RedirectUri);

        if (result.IsError)
            return Unauthorized(new LoginResultDto
            {
                Description = result.ErrorDescription,
                UserId = result.CustomResponse["userId"].ToString()
            });

        var user = new IdentityServerUser(result.Subject.Claims.First(c => c.Type.Equals(JwtClaimTypes.Id)).Value);

        foreach (var claim in result.Subject.Claims) user.AdditionalClaims.Add(claim);

        await HttpContext.SignInAsync(user);

        return Ok(new LoginResultDto { Description = "sign in success", RedirectUrl = context.ReturnUrl });
    }

    /// <summary>
    /// 使用者登出
    /// </summary>
    /// <param name="logoutId"></param>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync(string logoutId)
    {
        var context = await _interaction.GetLogoutContextAsync(logoutId);
        var showSignoutPrompt = context?.ShowSignoutPrompt != false;

        if (User.Identity is { IsAuthenticated: true }) await HttpContext.SignOutAsync();

        return Ok(new
        {
            showSignoutPrompt,
            ClientName = string.IsNullOrEmpty(context?.ClientName) ? context?.ClientId : context.ClientName,
            context?.PostLogoutRedirectUri,
            context?.SignOutIFrameUrl,
            logoutId
        });
    }

    /// <summary>
    /// 判斷帳號狀態
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("validate/account")]
    public async Task<IActionResult> ValidateAccountAsync(ValidateAccountDto dto)
    {
        return Ok(await _accountService.ValidateAccountAsync(dto.CompanyId, dto.UserId));
    }

    /// <summary>
    /// 透過 Uuid(電話)判斷是否存在會員
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("validate/member")]
    public async Task<IActionResult> ValidateMemberByUuidAsync(ValidateMemberDto dto)
    {
        if (!await _captchaAppService.ValidateCaptchaAsync(dto.ValidateAccountCaptchaDto))
            return BadRequest("captcha validate fail");

        var mobile = dto.ValidateAccountCaptchaDto.Key;

        if (mobile[..3].Equals("886"))
            mobile = mobile.Substring(3, mobile.Length - 3);

        return Ok(await _memberService.GetAsync(m =>
            m.CompanyId.Equals(dto.CompanyId) &&
            m.Mobile.Contains(mobile) &&
            m.EntityStatus) != null);
    }

    /// <summary>
    /// 會員註冊
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(CreateMemberDto dto)
    {
        return Ok(await _memberService.CreateAsync(dto));
    }

    /// <summary>
    /// 會員歸戶(綁定)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("bind")]
    public async Task<IActionResult> BindAsync(CreateAccountDto dto)
    {
        return Ok(await _accountService.CreateAsync(dto));
    }

    /// <summary>
    /// 帳號啟用
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("activation")]
    public async Task<IActionResult> ActivateAsync(ActivateAccountDto dto)
    {
        if (!await _captchaAppService.ValidateCaptchaAsync(dto.ValidateAccountCaptchaDto))
            return BadRequest("captcha validate fail");

        var mobile = dto.ValidateAccountCaptchaDto.Key;

        if (mobile[..3].Equals("886"))
            mobile = mobile.Substring(3, mobile.Length - 3);

        return Ok(await _accountService.UpdateUuidAsync(dto.CompanyId, dto.UserId, mobile));
    }

    /// <summary>
    /// 忘記密碼
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("forget/password")]
    public async Task<IActionResult> ForgetPasswordAsync(ForgetPasswordDto dto)
    {
        if (!await _captchaAppService.ValidateCaptchaAsync(dto.ValidateAccountCaptchaDto))
            return BadRequest("captcha validate fail");

        return Ok(await _accountService.UpdatePasswordAsync(dto.CompanyId, dto.UserId, dto.Password));
    }

    [HttpGet("error")]
    public async Task<IActionResult> GetErrorAsync(string errorId)
    {
        return Ok(await _interaction.GetErrorContextAsync(errorId));
    }
}