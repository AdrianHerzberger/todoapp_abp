using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using todoapp.Services.Dtos;

namespace todoapp.Service.Contracts
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAllTodosAsync(bool trackChanges);
        Task<TodoItemDto> GetTodoAsync(Guid id, bool trackChanges);
        Task<TodoItemDto> CreateTodoAsync(TodoItemDto todoItem);
        Task DeleteTodoAsync(Guid id, bool trackChanges);
        Task UpdateTodoAsync(Guid id, TodoItemDto todoForUpdate, bool trackChanges);
    }
}
