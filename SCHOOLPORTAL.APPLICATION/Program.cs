using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Application;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<DepartmentQuery>();

builder.Services.AddDbContext<SchoolContext>(options=>
{
   options.UseNpgsql(builder.Configuration["ConnectionString"]);
});
builder.Services.AddMediatR(m=>{m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());});


var app = builder.Build();

await SeedAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


async Task SeedAsync(WebApplication host)
{
    using(var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<SchoolContext>();
        await SchoolContextSeed.SeedAsync(context);
    }
}
