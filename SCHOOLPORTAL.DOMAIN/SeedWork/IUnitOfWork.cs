namespace SeedWork;

public interface IUnitOfWork : IDisposable
{
   Task<bool> SaveAsync(CancellationToken cancellationToken= default);
}