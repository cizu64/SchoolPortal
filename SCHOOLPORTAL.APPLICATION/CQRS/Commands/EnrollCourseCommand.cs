namespace Application;

using MediatR;
using SeedWork;
using Entities;
using System.Text.Json;
using Specifications;
public class EnrollCourseCommand : IRequest<IResponseData>
{
    public int StudentId { get; }
    public int CourseId { get; }
    public EnrollCourseCommand(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}

public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, IResponseData>
{
    private readonly IGenericRepository<Student> _studentRepo;
    private readonly IGenericRepository<StudentCourse> _studentCourseRepo;
    public EnrollCourseCommandHandler(IGenericRepository<Student> studentRepo,  IGenericRepository<StudentCourse> studentCourseRepo)
    {
        _studentRepo = studentRepo;
        _studentCourseRepo = studentCourseRepo;
    }

    public async Task<IResponseData> Handle(EnrollCourseCommand command, CancellationToken cancellationToken)
    {
        var spec = new BaseSpecification<Student>(s=>s.Id==command.StudentId);
        var student = await _studentRepo.GetAsync(spec);
        student.EnrollCourse(student.Id,command.CourseId);
        
        await _studentCourseRepo.AddAsync(student.StudentCourses.FirstOrDefault());

        await _studentCourseRepo.UnitOfWork.SaveAsync(cancellationToken);
        return new Success{detail = "Enrolled!"};
    }
}