using Microsoft.EntityFrameworkCore;
using todoapp.Contracts;
using todoapp.Entities.Models;

namespace todoapp.Repository
{
    internal sealed class TodoRepository : RepositoryBase<TodoItem>, ITodoRepository
    {
        public TodoRepository(RepositoryContext repositoryContext) : base(repositoryContext) 
        {
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync(bool trackChnages) =>
            await FindAll(trackChnages)
                .OrderBy(c => c.Text)
                .ToListAsync();

        public async Task<TodoItem> GetTodoItemAsync(Guid todoId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(todoId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTodoItem(TodoItem todoItem) => Create(todoItem);

        public async Task<IEnumerable<TodoItem>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void DeleteTodoItem(TodoItem item) => Delete(item);
    }
}
