namespace Entities;
using SeedWork;
public class Student : Entity, IAggregateRoot
{
    public Student(string firstname, string lastname, string email, string password, int age, string gender)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        Age = age;
        Gender = gender;
    }

   public string Firstname  { get; private set; }   
   public string Lastname  { get; private set; }   
   public string Email  { get; private set; }   
   public string Password  { get; private set; }   
   public int Age  { get; private set; }   
   public string Gender  { get; private set; }   

   public Address Address{get; private set;}

   private readonly List<StudentCourse> _studentCourses= new();

   public IReadOnlyCollection<StudentCourse> StudentCourses => _studentCourses;


   public void EnrollCourse(int studentId, int courseId)
   {
     if(!StudentCourses.Any(s=>studentId == s.StudentId && s.CourseId == courseId))
     {
        //enroll the student for course 
        _studentCourses.Add(new StudentCourse(studentId,courseId));
     }
   }

   public bool HasEnrolled(int studentId, int courseId)
   {
      return StudentCourses.Any(s=>studentId == s.StudentId && s.CourseId == courseId);
   }
}