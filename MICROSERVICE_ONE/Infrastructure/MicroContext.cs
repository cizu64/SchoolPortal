using System.Reflection;
using MICROSERVICE_ONE.Entity;
using Microsoft.EntityFrameworkCore;

namespace MICROSERVICE_ONE.Infrastructure;

public class MicroContext : DbContext
{
    public MicroContext(DbContextOptions<MicroContext> contextOptions) : base(contextOptions)
    { }
    public DbSet<Log> Log { get; set; }
    public DbSet<IntegrationData> IntegrationData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //no entity configurations created for now. can be used later
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
