using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Telligent.Member.Application.Auth;

public static class IdentityServerExtension
{
    private const string IdentityServerSectionKey = "IdentityServer";

    public static IServiceCollection AddAuthServer(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(IdentityServerSectionKey);

        services.AddIdentityServer(options =>
            {
                options.IssuerUri = section.GetValue<string>("HostUri");
                options.UserInteraction.LoginUrl = $"{section.GetValue<string>("WebUri")}/login";
                options.UserInteraction.ErrorUrl = $"{section.GetValue<string>("WebUri")}/error";
                options.UserInteraction.LogoutUrl = $"{section.GetValue<string>("WebUri")}/logout";
            })
            .AddInMemoryIdentityResources(new IdentityResource[]
            {
                // some standard scopes from the oidc spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            })
            .AddInMemoryApiScopes(section.GetSection("ApiScopes"))
            .AddInMemoryApiResources(section.GetSection("ApiResources"))
            .AddInMemoryClients(section.GetSection("Clients"))
            .AddProfileService<ProfileService>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

        services.AddLocalApiAuthentication();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        return services;
    }

    public static IApplicationBuilder UseAuthServer(this IApplicationBuilder app, IConfiguration configuration)
    {
        var section = configuration.GetSection(IdentityServerSectionKey);

        app.Use(async (ctx, next) =>
        {
            ctx.Request.Scheme = "https";
            ctx.Request.Host = new HostString(section.GetValue<string>("Host"));

            await next();
        });

        app.UseCookiePolicy(new CookiePolicyOptions
            {MinimumSameSitePolicy = SameSiteMode.None, Secure = CookieSecurePolicy.Always});

        return app;
    }
}