using AutoMapper;
using E_commerce.core;
using E_commerce.core.Dtos.Order;
using E_commerce.core.Models.Order;
using E_commerce.core.Services.Interfaces;
using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
  
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            this._orderService = orderService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        [ProducesResponseType(typeof(E_commerce.core.Models.Order.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErorrsResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
           var MappAddress= _mapper.Map<Address>(orderDto.Shippinaddress);
         var order= await _orderService.CreateOrderAsync(email, orderDto.BasketId, orderDto.DeliveryMethodId, MappAddress);
            if (order is null) return BadRequest(new ApiErorrsResponse(400, "There Problem in order"));
            return Ok(order);

        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<E_commerce.core.Models.Order.Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErorrsResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders= await _orderService.GetOrderForSpecifcUser(email);
            if (orders is null) return NotFound(new ApiErorrsResponse(404,"There Is No Order"));
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(mappedOrder);
        } 
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(E_commerce.core.Models.Order.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErorrsResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderByIdForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order= await _orderService.GetOrderByIdForSpecifcUser(email,id);
            if (order is null) return NotFound(new ApiErorrsResponse(404,$"There Is No Order with {id} "));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
            return Ok(mappedOrder);
        }
        [HttpGet("Deliverymethods")]
        public async Task<IActionResult> GetDeliveryMethods() 
        {
           var deliverymethods= await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();
            return Ok(deliverymethods);
        }
    }
}
