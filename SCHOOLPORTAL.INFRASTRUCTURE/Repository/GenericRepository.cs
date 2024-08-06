namespace Infrastructure;

using SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Specifications;
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

    public IEnumerable<T> Specify(ISpecification<T> spec)
    {
        var includes = spec.Includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));
        return includes.Where(spec.Criteria).AsEnumerable();
    }

    //if your dont understand the first Specify version, you can use this
    public IEnumerable<T> Specify2(ISpecification<T> spec)
    {
        var includes = spec.Includes;
        IQueryable<T>? query = _set;
        foreach(Expression<Func<T,object>> include in includes)
        {
            query= query.Include(include);
        }
        return query.Where(spec.Criteria).AsEnumerable();
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

    // public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    // {
    //     var data = await _set.FirstOrDefaultAsync(predicate);
    //     return data;
    // }

    public async Task<T> GetAsync(ISpecification<T> spec)
    {
        var includes = spec.Includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));
        var data = await includes.FirstOrDefaultAsync(spec.Criteria);
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