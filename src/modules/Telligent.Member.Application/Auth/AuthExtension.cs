using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Telligent.Member.Application.Auth;

public static class AuthExtension
{
    private const string AuthSectionKey = "Auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var section = configuration.GetSection(AuthSectionKey);

        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            options.Authority = section.GetValue<string>("Authority");
            options.Audience = section.GetValue<string>("Audience");
            options.TokenValidationParameters.ValidTypes = new[] {"at+jwt"};

            if (environment.IsDevelopment())
                options.RequireHttpsMetadata = false;
        });

        return services;
    }
}