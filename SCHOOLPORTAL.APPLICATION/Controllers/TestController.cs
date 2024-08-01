using Microsoft.AspNetCore.Mvc;

[Route("[Controller]/[Action]")]
public class TestController: ControllerBase
{
   public TestController()
   {
    
   }

  [HttpGet("/index")]
   public async Task<IActionResult> Index()
   {
      return Ok("this is a test endpoint");
   }
}