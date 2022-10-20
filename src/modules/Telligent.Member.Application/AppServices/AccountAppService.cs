using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Extensions;
using Telligent.Member.Application.Dtos.Account;
using Telligent.Member.Application.Dtos.Auth;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Account;
using Telligent.Member.Domain.Shared.Auth;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.AppServices;

public class AccountAppService : CrudAppService<Account, AccountDto, CreateAccountDto, UpdateAccountDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public AccountAppService(
        IRepository<Account> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    public override async Task<AccountDto> CreateAsync(CreateAccountDto dto)
    {
        var entity = Mapper.Map<Account>(dto);

        if (!string.IsNullOrEmpty(dto.Password))
            dto.Password = EncryptionHelper.EncryptSha1(EncryptionHelper.EncryptSha1(dto.Password));

        var channelSetting = await _uow.ChannelSettingRepository.GetAsync(cs =>
            cs.TenantId.Equals(Payload.TenantId) && cs.ChannelId.Equals(dto.ChannelId) && cs.EntityStatus);

        var member = channelSetting.RegisterType switch
        {
            RegisterType.Email => await _uow.MemberRepository.GetAsync(m =>
                m.TenantId.Equals(Payload.TenantId) && m.CompanyId.Equals(dto.CompanyId) && m.Email.Equals(dto.Uuid) &&
                m.EntityStatus),
            _ => await _uow.MemberRepository.GetAsync(m =>
                m.TenantId.Equals(Payload.TenantId) && m.CompanyId.Equals(dto.CompanyId) && m.Mobile.Equals(dto.Uuid) &&
                m.EntityStatus)
        };

        if (member == null)
            throw new Exception("member is null");

        entity.MemberId = member.Id;
        entity.AccountStatus = AccountStatus.Activated;

        await Repository.CreateAsync(entity);

        await Repository.SaveChangesAsync();

        return Mapper.Map<AccountDto>(await Repository.GetAsync(entity.Id));
    }

    /// <summary>
    /// 驗證帳號狀態
    /// </summary>
    /// <param name="companyId">公司識別碼</param>
    /// <param name="userId">帳號</param>
    /// <returns></returns>
    public async Task<ValidateAccountResultDto> ValidateAccountAsync(Guid companyId, string userId)
    {
        var account = await Repository.GetAsync(a =>
            a.CompanyId.Equals(companyId) && a.UserId.Equals(userId) && a.EntityStatus);

        if (account == null)
            return new ValidateAccountResultDto { IsExist = false };

        return new ValidateAccountResultDto
        {
            IsExist = true,
            IsPasswordEmpty = string.IsNullOrEmpty(account.Password),
            AccountStatus = account.AccountStatus
        };
    }

    /// <summary>
    /// 修改唯一識別碼(電話)
    /// </summary>
    /// <param name="companyId">公司識別碼</param>
    /// <param name="userId">帳號</param>
    /// <param name="uuid">唯一識別碼</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> UpdateUuidAsync(Guid companyId, string userId, string uuid)
    {
        var entity = await _uow.AccountRepository.GetAsync(a =>
            a.CompanyId.Equals(companyId) && a.UserId.Equals(userId) && a.EntityStatus);

        if (entity == null)
            throw new Exception("account is null");

        var member = await _uow.MemberRepository.GetAsync(m => m.Id.Equals(entity.MemberId) && m.EntityStatus);

        if (member == null)
            throw new Exception("member is null");

        var accounts = await _uow.AccountRepository.GetListAsync(a => a.MemberId.Equals(member.Id) && a.EntityStatus);

        foreach (var item in accounts)
        {
            item.Uuid = uuid;
            item.ModificationTime = DateTime.UtcNow.ToUtc8DateTime();
            item.ModifierId = member.Id;
        }

        member.Mobile = uuid;
        member.ModificationTime = DateTime.UtcNow.ToUtc8DateTime();
        member.ModifierId = member.Id;

        _uow.AccountRepository.Update(accounts);
        _uow.MemberRepository.Update(member);

        return await _uow.SaveChangeAsync() == accounts.Count + 1;
    }

    /// <summary>
    /// 修改密碼
    /// </summary>
    /// <param name="companyId">公司識別碼</param>
    /// <param name="userId">帳號</param>
    /// <param name="password">新密碼</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> UpdatePasswordAsync(Guid companyId, string userId, string password)
    {
        var account = await Repository.GetAsync(a =>
            a.CompanyId.Equals(companyId) && a.UserId.Equals(userId) && a.EntityStatus);

        if (account == null)
            throw new Exception("account is null");

        account.Password = EncryptionHelper.EncryptSha1(EncryptionHelper.EncryptSha1(password));
        account.ModificationTime = DateTime.UtcNow.ToUtc8DateTime();

        Repository.Update(account);

        return await Repository.SaveChangesAsync() == 1;
    }
}