using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  MESSAGEBUS.RabbitMQ;
using MICROSERVICE_TWO.Infrastructure;
namespace MICROSERVICE_TWO.Controllers;


[ApiController]
[Route("api/{Controller}/")]
public class LogController : ControllerBase
{
   
   private readonly MicroContext context;
    public LogController(MicroContext context)
    {
        this.context = context;
    }

  
    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs()
    {
        var log = context.Log;
        var result = log.Select(l => new
        {
            l.Level,
            l.Message,
            l.StackTrace,
            l.DateCreated
        });
        //send events to subscribers later
        return Ok(result);
    }
}