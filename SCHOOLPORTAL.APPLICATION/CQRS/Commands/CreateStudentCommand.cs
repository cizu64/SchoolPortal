namespace Application;

using MediatR;
using SeedWork;
using Entities;
using System.Text.Json;

public class CreateStudentCommand : IRequest<Task>
{
     public string Firstname{get;}
     public string Lastname{get;} 
     public string Email{get;} 
     public string Password{get;} 
     public string Gender {get;}
    public int Age{get;} 
    public string City{get;}
    public string Street{get;}
    public string Country{get;}
    public string State{get;}
    public CreateStudentCommand(string firstname, string lastname, string email, string password, int age, string gender, string city, string street, string country, string state)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Gender = gender;
        Age = age;
        Password = password;
        City=city;
        Street=street;
        Country=country;
        State=state;
    }
}

public class CreateStudentCommandHandler: IRequestHandler<CreateStudentCommand, Task>
{
    private readonly IGenericRepository<Student> _studentRepo;
    private readonly ILogger<CreateStudentCommandHandler> logger;
    public CreateStudentCommandHandler(IGenericRepository<Student> studentRepo, ILogger<CreateStudentCommandHandler> logger)
    {
        _studentRepo = studentRepo;
        this.logger = logger;
    }

    public async Task<Task> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var student = new Student(command.Firstname, command.Lastname, command.Email,command.Password,command.Age, command.Gender);
        student.AddAddress(command.State, command.Country, command.Street, command.City);
        
        await _studentRepo.AddAsync(student); //add to repo
        await _studentRepo.UnitOfWork.SaveAsync(cancellationToken);
        return Task.CompletedTask;
    }
}