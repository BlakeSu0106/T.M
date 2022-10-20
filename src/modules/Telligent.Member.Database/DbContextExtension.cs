using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Member.Database;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, string connection)
    {
        services.AddPooledDbContextFactory<MemberDbContext>(options =>
        {
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });

        return services;
    }

    public static IServiceCollection AddCampaignDbContexts(this IServiceCollection services, string connection)
    {
        services.AddPooledDbContextFactory<CampaignDbContext>(options =>
        {
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });

        return services;
    }

    public static void RegisterDbContexts(this ContainerBuilder builder)
    {
        builder.RegisterType<CampaignDbContext>().As<BaseDbContext>().InstancePerLifetimeScope();
        builder.RegisterType<MemberDbContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}