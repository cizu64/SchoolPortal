namespace SeedWork;

using MediatR;
public interface IUnitOfWork : IDisposable
{
   Task<bool> SaveAsync(CancellationToken cancellationToken= default);
}