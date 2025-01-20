using E_commerce.core;
using E_commerce.core.Models;
using E_commerce.core.Models.Order;
using E_commerce.core.Repository.Interfaces;
using E_commerce.core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork
            )
        {
            this._configuration = configuration;
            this._basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<UserBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:Secretkey"];
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket == null) return null;
            var ShippingPrice=0M;
            if (Basket.DeliveryMetohdId.HasValue)
            {
                var derliverymethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(Basket.DeliveryMetohdId.Value);
                ShippingPrice= derliverymethod.Cost;
            }
            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items) { 
                var Proudect= await _unitOfWork.Repository<Proudect,int>().GetAsync(item.Id);
                    if(item.Price!=Proudect.Price)
                        item.Price = Proudect.Price;
                }
            }
            var SubTotal= Basket.Items.Sum(item=>item.Price*item.Quant);
            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId)) //Create
            {
                var Option = new PaymentIntentCreateOptions()
                
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>()
                    {
                        "card"
                    }
                };
                paymentIntent= await  Service.CreateAsync(Option);
                Basket.PaymentIntentId=paymentIntent.Id; 
                Basket.ClientSecret=paymentIntent.ClientSecret;
            }
            else //Update
            {
                var Option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingPrice * 100
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, Option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }
    }
}
