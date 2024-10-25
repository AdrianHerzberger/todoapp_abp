namespace todoapp.Contracts
{
    public interface IRepositoryManager
    {
        ITodoRepository TodoItem { get; }
        Task SaveAsync();
    }
}
