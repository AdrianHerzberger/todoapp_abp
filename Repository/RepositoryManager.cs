

using todoapp.Contracts;

namespace todoapp.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITodoRepository> _todoRepository;

        public RepositoryManager(RepositoryContext respositoryContext)
        {
            _repositoryContext = respositoryContext;
            _todoRepository = new Lazy<ITodoRepository>(() =>
                new TodoRepository(respositoryContext));
        }

        public ITodoRepository TodoItem => _todoRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
