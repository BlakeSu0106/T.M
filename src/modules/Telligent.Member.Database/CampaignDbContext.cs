using Microsoft.EntityFrameworkCore;
using Telligent.Core.Infrastructure.Database;
using Telligent.Member.Domain.Campaign;

namespace Telligent.Member.Database;

public class CampaignDbContext : BaseDbContext
{
    public CampaignDbContext(DbContextOptions<CampaignDbContext> options) : base(options)
    {
    }

    public DbSet<FileDetail> FileDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}