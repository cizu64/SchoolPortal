namespace Application;
public class Success : IResponseData
{
   public object detail{ get; set; }
   public int statusCode{get;set;} = StatusCodes.Status200OK;
}