using todoapp.Entities.Models;

namespace todoapp.Contracts
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync(bool trackChanges);
        Task<TodoItem> GetTodoItemAsync(Guid Id, bool trackChanges);
        void CreateTodoItem(TodoItem todo);
        Task<IEnumerable<TodoItem>> GetByIdsAsync(IEnumerable<Guid> Id, bool trackChanges);
        void DeleteTodoItem(TodoItem todoItem);
    }
}
