namespace Infrastructure;

using Entities;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using System.Reflection;
using MediatR;

public class SchoolContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    
    public SchoolContext(DbContextOptions<SchoolContext> options, IMediator mediator): base(options)
    {
        _mediator = mediator;
    }


    public DbSet<Student> Student{get;set;}
    public DbSet<Course> Course{get;set;}

    public DbSet<StudentCourse> StudentCourse{get;set;}
    public DbSet<Department> Department{get;set;}
    public DbSet<Todo> Todo{get;set;}

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        //before calling save changes, dispatch domain events
        await _mediator.DispatchDomainEventsAsync(this);
        
        await base.SaveChangesAsync(cancellationToken);    
        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(s=>
        {
            s.Ignore(d=>d.DomainEvents);
        });

        modelBuilder.Entity<StudentCourse>(s=>
        {
            s.Ignore(d=>d.DomainEvents);
        });
        
        modelBuilder.Entity<Department>(s=>
        {
            s.Ignore(d=>d.DomainEvents);
        });

        modelBuilder.Entity<Todo>(s=>
        {
            s.Ignore(d=>d.DomainEvents);
        });
        
        modelBuilder.Entity<Course>(s=>
        {
            s.Ignore(d=>d.DomainEvents);
        });
      
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}