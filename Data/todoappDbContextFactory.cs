using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace todoapp.Data;

public class todoappDbContextFactory : IDesignTimeDbContextFactory<todoappDbContext>
{
    public todoappDbContext CreateDbContext(string[] args)
    {
        todoappEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<todoappDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new todoappDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}