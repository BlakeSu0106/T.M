using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services.Notifications;
using Telligent.Core.Domain.Notifications;
using Telligent.Core.Infrastructure.Captcha;
using Telligent.Core.Infrastructure.Extensions;
using Telligent.Core.Infrastructure.Generators;
using Telligent.Core.Infrastructure.Services;
using Telligent.Member.Application.Dtos.AccountCaptcha;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Account;

namespace Telligent.Member.Application.AppServices;

public class AccountCaptchaAppService : IAppService
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly SmsPushService _smsService;
    private readonly UnitOfWork _uow;

    public AccountCaptchaAppService(
        IStringLocalizer<LocalizeResource> localizer,
        UnitOfWork uow,
        SmsPushService smsService)
    {
        _localizer = localizer;
        _uow = uow;
        _smsService = smsService;
    }

    /// <summary>
    /// 發送驗證碼
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<PushResult> SendCaptchaAsync(CreateAccountCaptchaDto dto)
    {
        var captcha = new AccountCaptcha
        {
            Id = SequentialGuidGenerator.Instance.GetGuid(),
            Key = dto.Key,
            Value = CaptchaHelper.RandomString(6)
        };

        var list = await _uow.AccountCaptchaRepository.GetListAsync(ac => ac.Key.Equals(dto.Key));

        if (list.Any())
        {
            var last = list.Max(l => l.CreationTime);

            if (last.HasValue && DateTime.UtcNow.ToUtc8DateTime().Subtract(last.Value).Minutes < 1)
                throw new Exception("seconds not over 60 ");
        }

        await _uow.AccountCaptchaRepository.CreateAsync(captcha);

        await _uow.SaveChangeAsync();

        return await _smsService.PushAsync("80", captcha.Key, captcha.Value);
    }

    /// <summary>
    /// 檢查驗證碼
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<bool> ValidateCaptchaAsync(ValidateAccountCaptchaDto dto)
    {
        if (string.IsNullOrEmpty(dto.Key))
            throw new Exception("mobile is null");

        var list = await _uow.AccountCaptchaRepository.GetListAsync(ac => ac.Key.Equals(dto.Key));

        return list.Any() && list.OrderByDescending(l => l.CreationTime).FirstOrDefault()!.Value.Equals(dto.Value);
    }
}