using todoapp.Services.Dtos;

namespace todoapp.Service.Contracts
{
    public interface IServiceManager
    {
        ITodoService TodoService { get; }
    }
}
