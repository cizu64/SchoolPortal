namespace Application;
public class Error : IResponseData
{
   public object detail{ get; set; }
   public int statusCode{get;set;} = StatusCodes.Status400BadRequest;
}