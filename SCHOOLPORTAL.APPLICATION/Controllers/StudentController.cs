namespace Application;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

[Route("[Controller]/[Action]")]
public class StudentController : ControllerBase
{
   private readonly IMediator _mediator;
   private readonly StudentQuery studentQuery;
   private readonly Authenticate autheticate;

   public StudentController(IMediator mediator, Authenticate autheticate, StudentQuery studentQuery)
   {
      _mediator = mediator;
      this.studentQuery = studentQuery;
      this.autheticate = autheticate;
   }

   [HttpPost]
   public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
   {
      var student = await _mediator.Send(new CreateStudentCommand(dto.Firstname, dto.Lastname, dto.Email, dto.Password, dto.Age, dto.Gender, dto.City, dto.Street, dto.Country, dto.State));
      return Ok(new Success { detail = "Student created" });
   }

   [HttpPost]
   public async Task<IActionResult> Login([FromBody] LoginDto dto)
   {
      IResponseData response = await autheticate.CreateToken(dto.Email, dto.Password);
      if (response is Error error) return BadRequest(error);
      return Ok(response);
   }

   [Authorize, HttpPost]
   public async Task<IActionResult> Enroll([Required] int courseId)
   {
      var currentUser = int.Parse(User.Identity.Name);
      IResponseData response = await _mediator.Send(new EnrollCourseCommand(currentUser, courseId));
      return Ok(response);
   }

   [Authorize, HttpGet]
   public async Task<IActionResult> StudentCourse()
   {
      var currentUser = int.Parse(User.Identity.Name);
      var course = studentQuery.GetStudentCourses(currentUser);
      return Ok(course);
   }

     [Authorize, HttpGet]
   public async Task<IActionResult> StudentTodos()
   {
      var currentUser = int.Parse(User.Identity.Name);
      var todos = studentQuery.GetStudentTodos(currentUser);
      return Ok(todos);
   }
}