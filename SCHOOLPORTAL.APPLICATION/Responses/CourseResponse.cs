namespace Application;

public record CourseResponse
{
    public int Id{get;set;}
    public string Name{get; set;} 
     public int DepartmentId{get;set;}  

    public string DepartmentName{get;set;}  
}