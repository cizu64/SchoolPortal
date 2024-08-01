namespace Infrastructure;

using SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    protected readonly SchoolContext _context;
    internal DbSet<T> _set;

    public IUnitOfWork UnitOfWork => _context;


    public GenericRepository(SchoolContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
       await _set.AddAsync(entity);
       return entity;
    }

    public void DeleteAsync(T entity)
    {
       _set.Remove(entity);
    }

    public async Task<T> GetAsync(Expression<Func<T,bool>> predicate)
    {
        var data = await _set.FirstOrDefaultAsync(predicate);
        return data;
    }
   
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int Id)
    {
        var data = await _set.FindAsync(Id);
        return data;
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
}