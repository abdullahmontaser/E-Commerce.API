using AutoMapper;
using E_commerce.core.Dtos;
using E_commerce.core.Models;
using E_commerce.core.Repository.Interfaces;
using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket(string? id)
        {
            if (id == null) return BadRequest(new ApiErorrsResponse(400, "Invaleid Id"));
            var basket= await _basketRepository.GetBasketAsync(id); 
             if (basket is null) new UserBasket() { Id = id };

            //await _basketRepository.UpdateBasketAsync(basket);

            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<UserBasket>>CreatorUpdate(UserBasketDto model)
        {

           var basket= await _basketRepository.UpdateBasketAsync(_mapper.Map<UserBasket>(model));
            if (basket is null) return BadRequest(new ApiErorrsResponse(400));
            return Ok(basket);

        }
        [HttpDelete]
        public async Task Delete(string id)
        {
             await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
