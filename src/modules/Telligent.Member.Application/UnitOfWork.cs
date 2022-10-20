using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Database;
using Telligent.Member.Domain.Account;
using Telligent.Member.Domain.Channels;
using Telligent.Member.Domain.Configs;
using Telligent.Member.Domain.Members;
using Telligent.Member.Domain.Organizations;
using Telligent.Member.Domain.Profiles;
using Telligent.Member.Domain.Prospect;
using Telligent.Member.Domain.Users;

namespace Telligent.Member.Application;

/// <summary>
/// UnitOfWork
/// </summary>
public class UnitOfWork : IDisposable
{
    private bool _disposed;

    public UnitOfWork(
        BaseDbContext context,
        IRepository<Tenant> tenantRepository,
        IRepository<Company> companyRepository,
        IRepository<CompanyMapping> companyMappingRepository,
        IRepository<Domain.Members.Member> memberRepository,
        IRepository<Membership> membershipRepository,
        IRepository<Account> accountRepository,
        IRepository<AccountCaptcha> accountCaptchaRepository,
        IRepository<Prospect> prospectRepository,
        IRepository<Channel> channelRepository,
        IRepository<ChannelSetting> channelSettingRepository,
        IRepository<Profile> profileRepository,
        IRepository<CustomProfile> customProfileRepository,
        IRepository<CustomProfileItem> customProfileItemRepository,
        IRepository<User> userRepository,
        IRepository<ThirdPartyLoginConfig> thirdPartyLoginConfigRepository)
    {
        Context = context;
        TenantRepository = tenantRepository;
        CompanyRepository = companyRepository;
        CompanyMappingRepository = companyMappingRepository;
        MemberRepository = memberRepository;
        MembershipRepository = membershipRepository;
        AccountRepository = accountRepository;
        AccountCaptchaRepository = accountCaptchaRepository;
        ProspectRepository = prospectRepository;
        ChannelRepository = channelRepository;
        ChannelSettingRepository = channelSettingRepository;
        ProfileRepository = profileRepository;
        CustomProfileRepository = customProfileRepository;
        CustomProfileItemRepository = customProfileItemRepository;
        UserRepository = userRepository;
        ThirdPartyLoginConfigRepository = thirdPartyLoginConfigRepository;
    }

    public IRepository<Tenant> TenantRepository { get; }
    public IRepository<Company> CompanyRepository { get; }
    public IRepository<CompanyMapping> CompanyMappingRepository { get; }
    public IRepository<Domain.Members.Member> MemberRepository { get; set; }
    public IRepository<Membership> MembershipRepository { get; set; }
    public IRepository<Account> AccountRepository { get; }
    public IRepository<AccountCaptcha> AccountCaptchaRepository { get; }
    public IRepository<Prospect> ProspectRepository { get; }
    public IRepository<Channel> ChannelRepository { get; }
    public IRepository<ChannelSetting> ChannelSettingRepository { get; }
    public IRepository<Profile> ProfileRepository { get; }
    public IRepository<CustomProfile> CustomProfileRepository { get; }
    public IRepository<CustomProfileItem> CustomProfileItemRepository { get; }
    public IRepository<User> UserRepository { get; }
    public IRepository<ThirdPartyLoginConfig> ThirdPartyLoginConfigRepository { get; }

    /// <summary>
    /// Context
    /// </summary>
    public BaseDbContext Context { get; private set; }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// SaveChange
    /// </summary>
    /// <returns></returns>
    public async Task<int> SaveChangeAsync()
    {
        return await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
            {
                Context.Dispose();
                Context = null;
            }

        _disposed = true;
    }
}