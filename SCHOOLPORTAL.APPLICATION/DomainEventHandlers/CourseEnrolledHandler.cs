namespace Application;
using DomainEvents;
using MediatR;
using SeedWork;
using Entities;
using Specifications;

public class CourseEnrolledHandler: INotificationHandler<CourseEnrolledEvent>
{
    private readonly IGenericRepository<Todo> _todoRepo;
    private readonly IGenericRepository<Course> _courseRepo;
   public CourseEnrolledHandler(IGenericRepository<Todo> todoRepo, IGenericRepository<Course> courseRepo)
   {
      _todoRepo=todoRepo;
      _courseRepo=courseRepo;
   }

   public async Task Handle(CourseEnrolledEvent notification, CancellationToken cancellationToken)
   {
      var spec = new BaseSpecification<Course>(c=>c.Id == notification.CourseId);
      var course = await _courseRepo.GetAsync(spec);
      await _todoRepo.AddAsync(new Todo(notification.StudentId,"New Course", $"You have enrolled for {course.Name} course. Start reading!"));
      await _todoRepo.UnitOfWork.SaveAsync(cancellationToken);
   }
}