using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace todoapp.Entities.Models
{
    public class TodoItem : BasicAggregateRoot<Guid>
    {
        [Column("TodoItemId")]
        public Guid Id { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for Text is 60 characters")]
        public string? Text { get; set; }
    }
}
