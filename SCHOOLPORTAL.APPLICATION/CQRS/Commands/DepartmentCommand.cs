namespace Application;

using MediatR;
using SeedWork;
using Entities;
public class DepartmentCommand : IRequest<Task>
{
    public string Name{get;}
    public DepartmentCommand(string name)
    {
        Name = name;
    }
}

public class DepartmentCommandHandler: IRequestHandler<DepartmentCommand, Task>
{
    private readonly IGenericRepository<Department> _departmentRepo;
    public DepartmentCommandHandler(IGenericRepository<Department> departmentRepo)
    {
        _departmentRepo = departmentRepo;
    }

    public async Task<Task> Handle(DepartmentCommand command, CancellationToken cacellationToken)
    {
        var department = new Department(command.Name);
        department.AddDepartment();
        await _departmentRepo.AddAsync(department); //add to repo
        await _departmentRepo.UnitOfWork.SaveAsync(cacellationToken);
        return Task.CompletedTask;

    }
}