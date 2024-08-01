namespace Infrastructure;

using Entities;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using System.Reflection;

public class SchoolContext : DbContext, IUnitOfWork
{
    public SchoolContext(DbContextOptions<SchoolContext> options): base(options){ }

    public DbSet<Student> Student{get;set;}
    public DbSet<Course> Course{get;set;}

    public DbSet<StudentCourse> StudentCourse{get;set;}
    public DbSet<Department> Department{get;set;}
    public DbSet<Todo> Todo{get;set;}

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}