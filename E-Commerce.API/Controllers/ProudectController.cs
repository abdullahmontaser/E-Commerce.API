using AutoMapper;
using E_commerce.core.Dtos.Proudects;
using E_commerce.core.Models;
using E_commerce.core.Services.Interfaces;
using E_commerce.core.Specifiction;
using E_Commerce.API.Attributes;
using E_Commerce.API.Erorrs;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Services.Services.Proudects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
  
    public class ProudectController : BaseApiController
    {
        private readonly IProdectService _servicers;
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public ProudectController(IProdectService servicers,StoreDbContext context, IMapper mapper)
        {

            _servicers = servicers;
            _context = context;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(IEnumerable<ProudectDtos>),StatusCodes.Status200OK)]
        [HttpGet]
        [Cach(100)]
        [Authorize]
        public async Task<IActionResult> GetAllProudect([FromQuery] ProductSpecPrams productSpec)
        {
            var result = await _servicers.GetAllProudectAsync(productSpec);
            return Ok(result);
        }
    
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrandsAsync()
        {

            var result = await _servicers.GetAllBrandsAsync();
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypesAsync()
        {

            var result = await _servicers.GetAllTypesAsync();
            return Ok(result);
        }   
        [HttpPut]
        public async Task<IActionResult> update(int id,ProudectDtos proudect)
        {
            //var get = await _context.Proudects.FindAsync(id);
            //var result = _mapper.Map<Proudect>(proudect);
            //get.Name = result.Name;
            //_context.Update(get);
            //_context.SaveChanges();

            //return Ok(get);

            var get = await _context.Proudects.FindAsync(id);
            var witid = get.Id;
            _mapper.Map(proudect, get);
            get.Id= witid;
            _context.Update(get);
            _context.SaveChanges();

            return Ok(get);
        }

        [ProducesResponseType(typeof(ProudectDtos), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErorrsResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErorrsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
         public async Task<IActionResult> GetProudectByIdAsync(int? id) {
            if (id is null) return BadRequest(new ApiErorrsResponse(400));
          var result= await _servicers.GetProudectByIdAsync(id.Value);
            if (result is null) return BadRequest(new ApiErorrsResponse(404, $"The prodect Id:{id} is not found"));
            return Ok(result);
        }
    } 
     
    }

