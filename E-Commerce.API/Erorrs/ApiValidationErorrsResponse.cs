namespace E_Commerce.API.Erorrs
{
    public class ApiValidationErorrsResponse : ApiErorrsResponse
    {
        public IEnumerable<String> Erorrs { get; set; } = new List<String>();
        public ApiValidationErorrsResponse(): base(400)
        {
             
        }
    }
}
