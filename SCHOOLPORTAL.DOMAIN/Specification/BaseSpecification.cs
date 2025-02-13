using System.Linq.Expressions;
using SeedWork;
namespace Specifications;

public class BaseSpecification<T> : ISpecification<T> where T : Entity
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
       Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria {get;}

    public List<Expression<Func<T, object>>> Includes {get;} = new();

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
