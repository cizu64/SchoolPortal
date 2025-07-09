using System.Reflection;
using MICROSERVICE_TWO.Entity;
using Microsoft.EntityFrameworkCore;

namespace MICROSERVICE_TWO.Infrastructure;

public class MicroContext : DbContext
{
    public MicroContext(DbContextOptions<MicroContext> contextOptions) : base(contextOptions)
    { }
    public DbSet<Log> Log { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //no entity configurations created for now. can be used later
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
