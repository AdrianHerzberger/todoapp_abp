using todoapp.Entities.Models; 
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Services;
using todoapp.Services.Dtos;
using todoapp.Service.Contracts;
using todoapp.Contracts;

namespace todoapp.Services
{
    public class TodoService : ApplicationService, ITodoService
    {
 
        private readonly IRepositoryManager _repository;

        public TodoService(IRepositoryManager repository) 
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllTodosAsync(bool trackChanges)
        {
            var todoItems = await _repository.TodoItem.GetAllTodosAsync(trackChanges);

            return todoItems
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Text = item.Text

                }).ToList(); 
        }

        public async Task<TodoItemDto> GetTodoAsync(Guid id, bool trackChanges)
        {
            var todoItem = await _repository.TodoItem.GetTodoItemAsync(id, trackChanges);

            var todoItemDto = new TodoItemDto
            {
                Id = todoItem.Id,
                Text = todoItem.Text,
            };

            return todoItemDto;
        }

        public async Task<TodoItemDto> CreateTodoAsync(TodoItemDto todoItem)
        {

            var todoItemEntity = new TodoItem
            {
                Text = todoItem.Text,
            };

            _repository.TodoItem.CreateTodoItem(todoItemEntity);
            await _repository.SaveAsync();

            return new TodoItemDto
            {
                Id = todoItemEntity.Id,
                Text = todoItemEntity.Text
            };
        }

        public async Task DeleteTodoAsync(Guid id, bool trackChanges)
        {
            var todoItem = await _repository.TodoItem.GetTodoItemAsync(id, trackChanges);

            _repository.TodoItem.DeleteTodoItem(todoItem);
            await _repository.SaveAsync();
        }

        public async Task UpdateTodoAsync(Guid id,  TodoItemDto todoItem, bool trackChanges)
        {
            await _repository.TodoItem.GetTodoItemAsync(id, trackChanges);
            await _repository.SaveAsync();
        }
    }
}
