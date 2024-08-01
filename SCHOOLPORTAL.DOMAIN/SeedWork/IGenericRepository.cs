using System.Linq.Expressions;

namespace SeedWork;
public interface IGenericRepository<T> where T : Entity
{
   IUnitOfWork UnitOfWork{get;}
   Task<T> AddAsync(T entity);
   void DeleteAsync(T entity);
   Task<T> GetByIdAsync(int Id);
   Task<T> GetAsync(Expression<Func<T,bool>> predicate);
   Task<IReadOnlyList<T>> GetAllAsync();
   Task UpdateAsync(T entity);
}