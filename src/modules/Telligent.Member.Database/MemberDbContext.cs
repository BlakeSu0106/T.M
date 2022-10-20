using Microsoft.EntityFrameworkCore;
using Telligent.Core.Infrastructure.Database;
using Telligent.Member.Domain.Account;
using Telligent.Member.Domain.Channels;
using Telligent.Member.Domain.Configs;
using Telligent.Member.Domain.Members;
using Telligent.Member.Domain.Organizations;
using Telligent.Member.Domain.Profiles;
using Telligent.Member.Domain.Prospect;
using Telligent.Member.Domain.Users;

namespace Telligent.Member.Database;

public class MemberDbContext : BaseDbContext
{
    public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<Company> Company { get; set; }

    public DbSet<CompanyMapping> CompanyMapping { get; set; }

    public DbSet<Domain.Members.Member> Members { get; set; }

    public DbSet<MemberMapping> MemberMappings { get; set; }

    public DbSet<ChannelMapping> ChannelMappings { get; set; }

    #region membership 會籍

    public DbSet<Membership> Memberships { get; set; }

    #endregion

    #region account 帳號

    public DbSet<Account> Accounts { get; set; }

    public DbSet<AccountCaptcha> AccountCaptcha { get; set; }

    #endregion

    #region prospect 潛客

    public DbSet<Prospect> Prospects { get; set; }

    public DbSet<ProspectMapping> ProspectMappings { get; set; }

    #endregion

    #region channel 渠道

    public DbSet<Channel> Channels { get; set; }

    public DbSet<ChannelSetting> ChannelSettings { get; set; }

    #endregion

    #region profile 

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<CustomProfile> CustomProfiles { get; set; }

    public DbSet<CustomProfileItem> CustomProfilesItems { get; set; }

    #endregion

    #region config 設定

    public DbSet<ThirdPartyLoginConfig> ThirdPartyLoginConfigs { get; set; }

    #endregion

    #region user

    public DbSet<User> Users { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tenant>().Ignore(t => t.TenantId);
        builder.Entity<MemberMapping>().Ignore(t => t.TenantId);
        builder.Entity<ChannelMapping>().Ignore(t => t.TenantId);
        builder.Entity<ProspectMapping>().Ignore(t => t.TenantId);
        builder.Entity<User>().Ignore(t => t.TenantId);

        base.OnModelCreating(builder);
    }
}