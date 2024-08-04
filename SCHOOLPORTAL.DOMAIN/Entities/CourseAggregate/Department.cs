namespace Entities;
using SeedWork;

public class Department : Entity
{
    public Department(string name)
    {
        Name = name;
    }

    public string Name{get;private set;}
    public bool IsActive{get;private set;}=true;


}