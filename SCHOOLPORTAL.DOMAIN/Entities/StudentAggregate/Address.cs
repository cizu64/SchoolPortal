namespace Entities;
public class Address
{
    public Address(string state, string country, string street, string city)
    {
        State = state;
        Country = country;
        Street = street;
        City = city;
    }

    public string City{get;private set;}
    public string Street{get;private set;}
    public string Country{get;private set;}
    public string State{get;private set;}
}