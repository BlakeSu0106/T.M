using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Extensions;
using Telligent.Core.Infrastructure.Generators;
using Telligent.Member.Application.Dtos.Member;
using Telligent.Member.Application.Localization;
using Telligent.Member.Domain.Account;
using Telligent.Member.Domain.Shared.Members;

namespace Telligent.Member.Application.AppServices;

public class MemberAppService : CrudAppService<Domain.Members.Member, MemberDto, CreateMemberDto, MemberDto>
{
    private readonly IStringLocalizer<LocalizeResource> _localizer;
    private readonly UnitOfWork _uow;

    public MemberAppService(
        IRepository<Domain.Members.Member> repository,
        IMapper mapper,
        IStringLocalizer<LocalizeResource> localizer,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow) : base(repository, mapper, httpContextAccessor)
    {
        _localizer = localizer;
        _uow = uow;
    }

    public override async Task<MemberDto> CreateAsync(CreateMemberDto dto)
    {
        var member = Mapper.Map<Domain.Members.Member>(dto);
        var account = Mapper.Map<Account>(dto.Account);

        if (!member.Mobile.Equals(account.Uuid))
            throw new Exception("mobile is exception");

        if (await _uow.MemberRepository.GetAsync(m =>
                m.TenantId.Equals(Payload.TenantId) && m.CompanyId.Equals(dto.CompanyId) &&
                m.Mobile.Equals(dto.Mobile) &&
                m.EntityStatus) != null)
            throw new Exception("mobile is exist");

        var membership = await _uow.MembershipRepository.GetAsync(m =>
            m.TenantId.Equals(Payload.TenantId) && m.CompanyId.Equals(dto.CompanyId) &&
            !m.PreviousMembershipId.HasValue &&
            m.EntityStatus);

        if (membership != null)
            member.MembershipId = membership.Id;

        member.Id = SequentialGuidGenerator.Instance.GetGuid();
        member.RegistrationTime = DateTime.UtcNow.ToUtc8DateTime();

        account.MemberId = member.Id;
        account.AccountStatus = AccountStatus.Activated;
        if (!string.IsNullOrEmpty(account.Password))
            account.Password = EncryptionHelper.EncryptSha1(EncryptionHelper.EncryptSha1(account.Password));

        await _uow.MemberRepository.CreateAsync(member);
        await _uow.AccountRepository.CreateAsync(account);

        await _uow.SaveChangeAsync();

        return Mapper.Map<MemberDto>(await Repository.GetAsync(member.Id));
    }
}