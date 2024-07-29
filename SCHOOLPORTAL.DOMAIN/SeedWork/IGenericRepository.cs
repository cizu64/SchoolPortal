using System.Linq.Expressions;

namespace SeedWork;
public interface IGenericRepository<T> where T : Entity, IAggregateRoot
{
   IUnitOfWork UnitOfWork{get;}
   Task<T> AddAsync(T entity);
   Task DeleteAsync(T entity);
   Task<T> GetByIdAsync(int Id);
   Task<T> Get(Expression<Func<T,bool>> predicate);
   Task<IReadOnlyList<T>> GetAll();
   Task UpdateAsync(T entity);
}