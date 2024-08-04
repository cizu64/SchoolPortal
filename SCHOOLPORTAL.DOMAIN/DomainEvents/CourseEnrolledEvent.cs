namespace DomainEvents;
using MediatR;
public class CourseEnrolledEvent : INotification
{
    public int StudentId { get; }
    public int CourseId { get; }
    public CourseEnrolledEvent(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}