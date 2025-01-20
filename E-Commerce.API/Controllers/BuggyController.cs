using E_Commerce.API.Erorrs;
using E_Commerce.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }
        [HttpGet("NotFound")]
        public async Task<IActionResult> GetNotFoundError(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound(new ApiErorrsResponse(404,$"Brand With ID: {id} Not Found "));
            return Ok(brand);
        }   
        [HttpGet("servererorr")]
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);
            var brandtostring = brand.ToString();
            return Ok(brand);
        }
        [HttpGet("BadRequist")]
        public async Task<IActionResult> GetBadRequest()
        {
          
            return BadRequest(new ApiErorrsResponse (400));
        }
        [HttpGet("BadRequist/{id}")]
        public async Task<IActionResult> ValditionErorr(int id)
        {
          
            return Ok();
        }
        [HttpGet("unaithoriezd")]
        public async Task<IActionResult> GetUnauthorizedErorr(int id)
        {
          
            return Unauthorized(new ApiErorrsResponse(401));
        }



    }
}
