namespace Infrastructure;
using Entities;
using Microsoft.EntityFrameworkCore;

public static class SchoolContextSeed
{
    public static async Task SeedAsync(this SchoolContext context)
    {
        try
        {
             await context.Database.MigrateAsync(); //this will automatically migrate the database
            int departmentId=0;
            if(!context.Department.Any())
            {
                var department = context.Department.Add(new Department("Computer Science"));
                await context.SaveChangesAsync();
                departmentId = department.Entity.Id;
            }
            if(!context.Course.Any())
            {
                context.Course.AddRange(new List<Course>
                {
                    {new Course(departmentId,"Data Analytics")},
                    {new Course(departmentId,"Software Enginerring")},
                });

                await context.SaveChangesAsync();
            }
        }
        catch(Exception ex)
        {

        }
    }
}