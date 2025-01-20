namespace E_Commerce.API.Erorrs
{
    public class ApiExceptionResponce:ApiErorrsResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponce(int statusCode,string? message=null,string?details=null):base(statusCode,message)
        {
            Details=details;
        }

    }
}
