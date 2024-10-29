using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using todoapp.Contracts;
using todoapp.LoggerService;
using todoapp.Repository;
using todoapp.Service.Contracts;
using todoapp.Services;

namespace todoapp.Extentions
{
    public static class ServiceExtentions
    {

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
           services.Configure<IISOptions>(options =>
           {

           });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseMySql(configuration.GetConnectionString("mysqlConnection"), new MySqlServerVersion(new Version(8, 0, 21))));
    }
}
