namespace Application;
using Infrastructure;
using SeedWork;
using Entities;
using System.Linq;
using Specifications;
public class DepartmentQuery
{
   private readonly IGenericRepository<Department> _departmentRepo;
   public DepartmentQuery(IGenericRepository<Department> departmentRepo)
   {
      _departmentRepo = departmentRepo;
   }

   public async Task<IEnumerable<DepartmentResponse>> GetDepartments()
   {
      var activeDepartment = _departmentRepo.Specify(new GetActiveDepartment());
      return activeDepartment.Select(d => new DepartmentResponse{ Name = d.Name});
   }
}