using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using Infrastructure.Common.Factories;
using Infrastructure.Data;
using Infrastructure.LoggingConfig;
using Infrastructure.Repositories;
using Serilog;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'SQLConnection' not found.");

        // Custom services
        ConfigureSerilog();

        // DB context
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddTransient<IDbContextFactory, ApplicationDbContextFactory>(s =>
        {
            var connectionString = configuration.GetSection("ConnectionStrings:SQLConnection").Value;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(
                connectionString,
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                })
                .Options;
            return new ApplicationDbContextFactory(options);
        });

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient(typeof(IStoredProcedure<>), typeof(StoredProcedure<>));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<ILogging, CustomSerilogLogging>();

        services.AddAuthorizationBuilder();

        services.AddSingleton(TimeProvider.System);

        return services;
    }

    private static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.With(new LoggingEnricher())
            .MinimumLevel.Debug()
            .WriteTo.Console(
                outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}"
            )
            .CreateLogger();
    }
}
