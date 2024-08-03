namespace Application;

using Microsoft.AspNetCore.Mvc;
using MediatR;

[Route("[Controller]/[Action]")]
public class TestController: ControllerBase
{
   DepartmentQuery departmentQuery;
   private IMediator _mediator;
   
   public TestController(DepartmentQuery departmentQuery, IMediator mediator)
   {
      this.departmentQuery = departmentQuery;
      _mediator=mediator;
   }

  [HttpGet("/index")]
   public async Task<IActionResult> Index()
   {
      var department = await departmentQuery.GetDepartments();
      return Ok(department);
   }

   [HttpPost]
   public async Task<IActionResult> CreateDepartment(string name)
   {
      await _mediator.Send(new DepartmentCommand(name));
      return Ok("done");
   }
}