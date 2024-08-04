using System.Linq.Expressions;
using SeedWork;
namespace Specifications;

public interface ISpecification<T> where T : Entity
{
    Expression<Func<T,bool>> Criteria{get;} //to be used for filtering
    List< Expression<Func<T,object>>> Includes{get;} //for including related entities
}