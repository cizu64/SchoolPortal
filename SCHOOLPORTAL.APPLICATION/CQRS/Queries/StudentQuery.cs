namespace Application;
using Infrastructure;
using SeedWork;
using Entities;
using System.Linq;
using Specifications;
public class StudentQuery
{
   private readonly IGenericRepository<Course> _courseRepo;
   private readonly IGenericRepository<Student> _sRepo;
   private readonly IGenericRepository<Todo> _tRepo;
   public StudentQuery(IGenericRepository<Course> courseRepo, IGenericRepository<Student> sRepo, IGenericRepository<Todo> tRepo)
   {
      _courseRepo = courseRepo;
      _sRepo = sRepo;
      _tRepo = tRepo;
   }

   public IEnumerable<CourseResponse> GetCourses()
   {
      //filer and include using specification
      var specification = new BaseSpecification<Course>(c => c.IsActive);
      specification.AddInclude(d => d.Department);
      var activeCourse = _courseRepo.Specify(specification);
      return activeCourse.Select(c => new CourseResponse { Id = c.Id, Name = c.Name, DepartmentId = c.DepartmentId, DepartmentName = c.Department.Name });
   }

   public IEnumerable<object> GetStudentCourses(int studentId)
   {
      var specification = new BaseSpecification<Student>(c => c.Id == studentId); //filter
      specification.AddInclude(s => s.StudentCourses);
      var student = _sRepo.Specify(specification);
      return student.SelectMany(s => s.StudentCourses.Select(c => new
      {
         StudentCourseId = c.Id,
         StudentId = c.StudentId,
         CourseId = c.CourseId,
         DateCreated = c.DateCreated
      }));
   }

   public IEnumerable<object> GetStudentTodos(int studentId)
   {
      var specification = new BaseSpecification<Todo>(c => c.StudentId == studentId); //filter
      var todos = _tRepo.Specify(specification);
      return todos.Select(t => new
      {
         Id = t.Id,
         studentId = t.StudentId,
         name = t.Name,
         description = t.Description,
         isActive = t.IsActive,
         isCompleted = t.IsCompleted,
         dateCompleted = t.DateCompleted,
         dateCreated = t.DateCreated,
      });
   }
}