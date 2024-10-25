using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using todoapp.Entities.Models;


namespace todoapp.Repository.Configuration
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasData(
                    
                new TodoItem
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Text = "Shopping",
                }
                
            );
        }
    }
}
