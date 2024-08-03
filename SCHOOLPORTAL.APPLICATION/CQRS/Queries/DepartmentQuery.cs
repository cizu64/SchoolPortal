namespace Application;
using Infrastructure;
using SeedWork;
using Entities;
using System.Linq;
public class DepartmentQuery
{
   private readonly IGenericRepository<Department> _departmentRepo;
   public DepartmentQuery(IGenericRepository<Department> departmentRepo)
   {
      _departmentRepo = departmentRepo;
   }

   public async Task<IEnumerable<DepartmentResponse>> GetDepartments()
   {
      var department = await _departmentRepo.GetAllAsync();
      return department.Select(d => new DepartmentResponse{ Name = d.Name});
   }
}