namespace E_Commerce.API.Erorrs
{
    public class ApiErorrsResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErorrsResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefultMessageForStatusCode(statusCode);
        }

        private string? GetDefultMessageForStatusCode(int statusCode)
        {
            var defultMessage = statusCode switch
            {
                400=>"A Bad Request You Have Made",
                401 => "Authrized You Have Not",
                404 => "Resurse was Not Found",
                500  => "Server Erorr",
                _=>null
            };



            return defultMessage;
        }
    }
    
}
