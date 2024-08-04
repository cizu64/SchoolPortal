namespace Application;
using DomainEvents;
using MediatR;
using SeedWork;
using Entities;
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
      var course = await _courseRepo.GetAsync(c=>c.Id == notification.CourseId);
      await _todoRepo.AddAsync(new Todo(notification.StudentId,"New Course", $"You have enrolled for {course.Name} course. Start reading!"));
      await _todoRepo.UnitOfWork.SaveAsync(cancellationToken);
   }
}