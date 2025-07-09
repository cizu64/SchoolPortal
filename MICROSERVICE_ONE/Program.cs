using System.Reflection;
using MESSAGEBUS.RabbitMQ;
using MICROSERVICE_ONE.HostedService;
using MICROSERVICE_ONE.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddMediatR(m => { m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });

builder.Services.AddSingleton<Publisher>();

builder.Services.AddHostedService<IntegrationOutboxProcessor>();

builder.Services.AddDbContext<MicroContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionString"], opt =>
    {
        opt.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), errorCodesToAdd: null);
    });
});

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
