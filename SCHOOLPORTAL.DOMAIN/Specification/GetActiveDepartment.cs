namespace Specifications;

using System.Linq.Expressions;
using Entities;

public class GetActiveDepartment : BaseSpecification<Department>
{
    public GetActiveDepartment() : base(d=>d.IsActive)
    {
    }
}