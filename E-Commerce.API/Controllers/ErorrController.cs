using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("erorr/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErorrController : ControllerBase
    {
        
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErorrsResponse(StatusCodes.Status404NotFound,"Not Fonund End Point"));
        }
    }
}
