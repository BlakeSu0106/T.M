using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HotChocolate.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Telligent.Core.Infrastructure.IoC;
using Telligent.Core.Infrastructure.Loggers;
using Telligent.Member.Application.Auth;
using Telligent.Member.Application.GraphQL.Type;
using Telligent.Member.Application.IoC;
using Telligent.Member.Application.Localization;
using Telligent.Member.Application.Swagger;
using Telligent.Member.Database;
using Path = System.IO.Path;

ConfigureLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterAppServices();

        // register unused assemblies
        containerBuilder.RegisterAppServices(new List<string>
        {
            "Telligent.Core.Application"
        });

        containerBuilder.RegisterAutoMappers();
        containerBuilder.RegisterDbContexts();
        containerBuilder.RegisterRepositories();
        containerBuilder.RegisterUnitOfWork();
    });

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddGraphQLServer()
                    .RegisterDbContext<MemberDbContext>(DbContextKind.Pooled)
                    .RegisterService<IMapper>()
                    .AddQueryType<Query>();

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            if (builder.Environment.IsDevelopment())
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();

            policy.WithOrigins(builder.Configuration.GetSection("Cors").Get<string[]>())
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    });

    builder.Services.AddSelfLocalization();
    builder.Services.AddDbContexts(builder.Configuration.GetConnectionString("Default"));
    builder.Services.AddCampaignDbContexts(builder.Configuration.GetConnectionString("Campaign"));
    builder.Services.AddAuthServer(builder.Configuration);
    builder.Services.AddHealthChecks();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        builder.Services.AddSwagger(builder.Configuration);
    }

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        // swagger
        app.UseSwaggerDoc();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // record all requests and save to sys_log, should be put before other using.
    app.UseRequestLogger();

    // localization
    app.UseSelfLocalization();

    app.UseCors();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseForwardedHeaders();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/health");
    });

    app.MapGraphQL("/api/graphql").WithOptions(new GraphQLServerOptions
    {
        Tool = {
            Enable = app.Environment.IsDevelopment()
        }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
}
finally
{
    Log.CloseAndFlush();
}

void ConfigureLogger()
{
    var configBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);

    var configuration = configBuilder.Build();

    Log.Logger = new LoggerConfiguration()
#if DEBUG
        .MinimumLevel.Debug()
#else
        .MinimumLevel.Information()
#endif
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Async(c => c.MySQL(configuration.GetConnectionString("Default"), "sys_log"))
        .WriteTo.Async(
            c => c.File("logs/.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true))
        .WriteTo.Async(c => c.Console())
        .CreateLogger();
}