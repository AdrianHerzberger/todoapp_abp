using System;
using todoapp.Data;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;
using todoapp.Extentions;
using System.Runtime.CompilerServices;
using Autofac;
using todoapp.Service.Contracts;
using todoapp.Services;
using Autofac.Extras.DynamicProxy;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using todoapp.Contracts;
using todoapp.Repository;
using NLog;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace todoapp;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            #if DEBUG
                .MinimumLevel.Debug()
            #else
                .MinimumLevel.Information()
            #endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Add custom services to the container 
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureSqlContext(builder.Configuration);

            // Add controllers to the service container
            builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true; 
            });

            // Configure Autofac as the DI container
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    if (IsMigrateDatabase(args))
                    {
                        loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
                        loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    }
                    else
                    {
                        loggerConfiguration.WriteTo.Async(c => c.AbpStudio(services));
                    }
                });

            if (IsMigrateDatabase(args))
            {
                builder.Services.AddDataMigrationEnvironment();
            }
            await builder.AddApplicationAsync<todoappModule>();
            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<todoappDbMigrationService>().MigrateAsync();
                return 0;
            }

            Log.Information("Starting todoapp.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "todoapp terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
