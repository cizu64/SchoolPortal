namespace Entities;
using SeedWork;

public class StudentCourse: Entity
{
    public StudentCourse(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
    public int StudentId{get;private set;}
    public int CourseId{get;private set;}
}