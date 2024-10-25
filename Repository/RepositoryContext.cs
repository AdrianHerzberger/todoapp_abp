using Microsoft.EntityFrameworkCore;
using todoapp.Entities.Models;
using todoapp.Repository.Configuration;
using Volo.Abp.EntityFrameworkCore;

namespace todoapp.Repository
{
    public class RepositoryContext : AbpDbContext<RepositoryContext>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }

        public DbSet<TodoItem>? TodoItems { get; set; }
    }
}
