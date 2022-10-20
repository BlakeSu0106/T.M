using Duende.IdentityServer.Validation;
using Telligent.Member.Application.AppServices;

namespace Telligent.Member.Application.Auth;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly AuthAppService _authService;

    public ResourceOwnerPasswordValidator(AuthAppService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 驗證使用者
    /// </summary>
    /// <param name="context">Account</param>
    /// <returns></returns>
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        context.Result =
            await _authService.ValidateAsync(context.Request.Raw.Get("CompanyId"), context.UserName, context.Password);
    }

}