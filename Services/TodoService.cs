using AutoMapper;
using todoapp.Entities.Models;
using Volo.Abp.Application.Services;
using todoapp.Services.Dtos;
using todoapp.Service.Contracts;
using todoapp.Contracts;

namespace todoapp.Services
{
    public class TodoService : ApplicationService, ITodoService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TodoService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllTodosAsync(bool trackChanges)
        {
            var todoItems = await _repository.TodoItem.GetAllTodosAsync(trackChanges);
            var todoItemsDto = _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);

            return todoItemsDto;
        }

        public async Task<TodoItemDto> GetTodoAsync(Guid id, bool trackChanges)
        {
            var todoItem = await _repository.TodoItem.GetTodoItemAsync(id, trackChanges);
            var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);

            return todoItemDto;
        }

        public async Task<TodoItemDto> CreateTodoAsync(TodoItemDto todoItem)
        {

            var todoItemEntity = _mapper.Map<TodoItem>(todoItem);

            _repository.TodoItem.CreateTodoItem(todoItemEntity);
            await _repository.SaveAsync();

            var todoItemToReturn = _mapper.Map<TodoItemDto>(todoItemEntity);

            return todoItemToReturn;
        }

        public async Task DeleteTodoAsync(Guid id, bool trackChanges)
        {
            var todoItem = await _repository.TodoItem.GetTodoItemAsync(id, trackChanges);

            _repository.TodoItem.DeleteTodoItem(todoItem);
            await _repository.SaveAsync();
        }

        public async Task UpdateTodoAsync(Guid id,  TodoItemDto todoItemForUpdate, bool trackChanges)
        {
            var todoItem = await _repository.TodoItem.GetTodoItemAsync(id, trackChanges); 
            _mapper.Map(todoItemForUpdate, todoItem);
            await _repository.SaveAsync();
        }
    }
}
