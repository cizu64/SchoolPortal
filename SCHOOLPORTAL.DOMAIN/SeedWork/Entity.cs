namespace SeedWork;
public class Entity
{
    public int Id { get; private set;}
    public DateTime DateCreated { get; private set;} = DateTime.UtcNow;
}