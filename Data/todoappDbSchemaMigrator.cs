using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using todoapp.Repository;

namespace todoapp.Data;

public class todoappDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public todoappDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the todoappDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<RepositoryContext>()
            .Database
            .MigrateAsync();

    }
}
