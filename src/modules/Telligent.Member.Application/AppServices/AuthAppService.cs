using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Services;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Shared.Channels;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.AppServices;

public class AuthAppService : IAppService
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly ThirdPartyLoginAppService _thirdPartyLoginAppService;
    private readonly UnitOfWork _uow;

    public AuthAppService(
        IStringLocalizer<LocalizeResource> localizer,
        ThirdPartyLoginAppService thirdPartyLoginAppService,
        UnitOfWork uow)
    {
        _localizer = localizer;
        _thirdPartyLoginAppService = thirdPartyLoginAppService;
        _uow = uow;
    }

    public async Task<GrantValidationResult> ValidateAsync(string companyId, string userId, string password)
    {
        var account = await _uow.AccountRepository.GetAsync(a =>
            a.CompanyId.Equals(Guid.Parse(companyId)) &&
            a.UserId.Equals(userId) &&
            a.Password.Equals(EncryptionHelper.EncryptSha1(EncryptionHelper.EncryptSha1(password))) &&
            a.EntityStatus);

        if (account == null)
            return new GrantValidationResult(TokenRequestErrors.UnauthorizedClient,
                _localizer.GetString("account_validate_failed"));

        return account.AccountStatus switch
        {
            AccountStatus.Pending => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號未開通"),
            AccountStatus.Deactivated => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已停用"),
            AccountStatus.Activated => new GrantValidationResult(
                account.Id.ToString(),
                OidcConstants.AuthenticationMethods.Password,
                new List<Claim>
                {
                    new(JwtClaimTypes.Id, account.Id.ToString())
                }),
            AccountStatus.Deleted => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已關閉"),
            AccountStatus.Exception => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號發生異常錯誤"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<GrantValidationResult> ValidateAsync(string companyId, string userId)
    {
        var account = await _uow.AccountRepository.GetAsync(a =>
            a.CompanyId.Equals(Guid.Parse(companyId)) &&
            a.UserId.Equals(userId) &&
            a.EntityStatus);

        if (account == null)
            return new GrantValidationResult(TokenRequestErrors.UnauthorizedClient,
                _localizer.GetString("account_validate_failed"));

        return account.AccountStatus switch
        {
            AccountStatus.Pending => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號未開通"),
            AccountStatus.Deactivated => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已停用"),
            AccountStatus.Activated => new GrantValidationResult(
                account.Id.ToString(),
                OidcConstants.AuthenticationMethods.ConfirmationBySms,
                new List<Claim>
                {
                    new(JwtClaimTypes.Id, account.Id.ToString())
                }),
            AccountStatus.Deleted => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已關閉"),
            AccountStatus.Exception => new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號發生異常錯誤"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<GrantValidationResult> ValidateAsync(string companyId,
        ThirdPartyChannelType thirdPartyChannelType, string code, string redirectUrl)
    {
        var userId = thirdPartyChannelType switch
        {
            ThirdPartyChannelType.Line => await _thirdPartyLoginAppService.GetLineUserIdAsync(companyId, code,
                redirectUrl),
            ThirdPartyChannelType.Google => await _thirdPartyLoginAppService.GetGoogleUserIdAsync(companyId, code,
                redirectUrl),
            ThirdPartyChannelType.Facebook => await _thirdPartyLoginAppService.GetFacebookUserIdAsync(companyId, code,
                redirectUrl),
            _ => throw new ArgumentOutOfRangeException()
        };

        var account = await _uow.AccountRepository.GetAsync(a =>
            a.CompanyId.Equals(Guid.Parse(companyId)) &&
            a.UserId.Equals(userId) &&
            a.EntityStatus);

        if (account == null)
            return new GrantValidationResult(TokenRequestErrors.UnauthorizedClient,
                _localizer.GetString("account_not_find"), new Dictionary<string, object> { { "userId", userId } });

        switch (account.AccountStatus)
        {
            case AccountStatus.Activated:
                var member = await _uow.MemberRepository.GetAsync(m => m.Id.Equals(account.MemberId) && m.EntityStatus);
                if (member == null)
                    return new GrantValidationResult(TokenRequestErrors.UnauthorizedClient,
                        _localizer.GetString("member_validate_failed"));
                return new GrantValidationResult(
                    account.Id.ToString(),
                    OidcConstants.AuthenticationMethods.MultipleChannelAuthentication,
                    new List<Claim>
                    {
                        new("member_id", member.Id.ToString()),
                        new(JwtClaimTypes.Name, member.Name),
                        new(JwtClaimTypes.Email, member.Email),
                        new("avatar", member.Avatar)
                    });
            case AccountStatus.Pending:
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號未開通");
            case AccountStatus.Deactivated:
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已停用");
            case AccountStatus.Deleted:
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號已關閉");
            case AccountStatus.Exception:
                return new GrantValidationResult(TokenRequestErrors.InvalidGrant, "帳號發生異常錯誤");
            default: throw new ArgumentOutOfRangeException();
        }
    }
}