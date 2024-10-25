using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoapp.Services.Dtos
{
    public record TodoItemDto(Guid id, string Text);
}
