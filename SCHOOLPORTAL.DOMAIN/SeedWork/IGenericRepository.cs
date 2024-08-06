using System.Linq.Expressions;
using Specifications;
namespace SeedWork;
public interface IGenericRepository<T> where T : Entity
{
   IEnumerable<T> Specify(ISpecification<T> spec);
   IUnitOfWork UnitOfWork{get;}
   Task<T> AddAsync(T entity);
   void DeleteAsync(T entity);
   Task<T> GetByIdAsync(int Id);
   //Task<T> GetAsync(Expression<Func<T,bool>> predicate);
   Task<T> GetAsync(ISpecification<T> spec);
   Task<IReadOnlyList<T>> GetAllAsync();
   Task UpdateAsync(T entity);
}