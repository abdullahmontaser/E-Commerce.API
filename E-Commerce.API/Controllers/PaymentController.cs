using E_commerce.core.Services.Interfaces;
using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
  
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket= await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            if (Basket is null) return BadRequest(new ApiErorrsResponse(400, "There Is A Problem With Your Basket"));
            return  Ok(Basket);
        }
    }
}
