namespace Application;

using Microsoft.AspNetCore.Mvc;
using MediatR;

[Route("[Controller]/[Action]")]
public class CourseController : ControllerBase
{
    private readonly StudentQuery studentQuery;
    public CourseController(StudentQuery studentQuery)
    {
        this.studentQuery = studentQuery;
    }

    [HttpGet]
    public IActionResult Courses()
    {
        var courses =  studentQuery.GetCourses();
        return Ok(courses);
    }

}