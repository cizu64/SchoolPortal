namespace Entities;
using SeedWork;

public class Todo: Entity, IAggregateRoot
{
    public Todo(int studentId, string name, string description)
    {
        StudentId = studentId;
        Name = name;
        Description = description;
    }

    public int StudentId{get;private set;}
    public string Name{get;private set;}
    public string Description{get;private set;}
    public bool IsActive{get;private set;} = true;
    public bool IsCompleted{get;private set;} = false;
    public DateTime? DateCompleted{get;private set;} = null;

    //first behavior
    public void CompleteTodo(DateTime dateCompleted)
    {
        IsCompleted = true;
        DateCompleted = dateCompleted;
        IsActive=true;
    }

    public void CompleteTodo(){}
}