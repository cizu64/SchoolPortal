using System.ComponentModel.DataAnnotations;
using MediatR;
using MICROSERVICE_ONE.Entity;
using MICROSERVICE_ONE.Infrastructure;
using MICROSERVICE_ONE.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  MESSAGEBUS.RabbitMQ;
namespace MICROSERVICE_ONE.Controllers;


[ApiController]
[Route("api/{Controller}/")]
public class LogController : ControllerBase
{
    public record LogDto
    {
        [Required]
        public required string Level { get; set; }
        [Required]
        public required string Message { get; set; }
        public string? StackTrace { get; set; }

    }
    private readonly MicroContext context;
    private readonly IMediator _mediator;
    public LogController(MicroContext context, IMediator mediator)
    {
        this.context = context;
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddLogs([FromBody] LogDto dto)
    {
        //pushish integration events
        await _mediator.Publish(new CreateLog { Level = dto.Level, Message = dto.Message, StackTrace = dto.StackTrace });

        return Ok("Log added");
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