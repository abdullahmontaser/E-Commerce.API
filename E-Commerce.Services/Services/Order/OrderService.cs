using E_commerce.core;
using E_commerce.core.Models;
using E_commerce.core.Models.Order;
using E_commerce.core.Repository.Interfaces;
using E_commerce.core.Services.Interfaces;
using E_commerce.core.Specifiction.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this._basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
            this._paymentService = paymentService;
        }
        public async Task<E_commerce.core.Models.Order.Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverymethodId, Address ShipingAddress)
        {
            //1.Get Basket From Basket Repo
           var Basket=await _basketRepository.GetBasketAsync(BasketId);
            //2.Get Selected Items at Basket From Proudct Repo
            var OrderItems=new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items) 
                { 
                 var Proudect=await _unitOfWork.Repository<Proudect,int>().GetAsync(item.Id);
                    var proudectItemOrder = new ProudectItemOrder(Proudect.Id, Proudect.Name, Proudect.PictureUrl);
                    var OrderItem = new OrderItem(proudectItemOrder, Proudect.Price, item.Quant);
                    OrderItems.Add(OrderItem);
                
                }
            }
            //3.SubTotal
            var subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            //4.Get Delivery Method
            var deliverymethod=await _unitOfWork.Repository<DeliveryMethod,int>().GetAsync(DeliverymethodId);
            //5.Creat Order
            var Spec = new OrderWithPaymentSpecifiction(Basket.PaymentIntentId);
            var ExOrder = await _unitOfWork.Repository<E_commerce.core.Models.Order.Order, int>().GetWithSpecAsync(Spec);
            if (ExOrder != null)
            {
                _unitOfWork.Repository<E_commerce.core.Models.Order.Order, int>().Delete(ExOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(BasketId);

            }
            var order = new E_commerce.core.Models.Order.Order(BuyerEmail, ShipingAddress, deliverymethod, OrderItems, subtotal,Basket.PaymentIntentId);
            //6.add order localy 
             await _unitOfWork.Repository<E_commerce.core.Models.Order.Order,int>().AddAsync(order);
            //7.Save in DB
            var result = await _unitOfWork.ComplyeteAsync();
            if (result <= 0) return null;
            return order;


        }

        public async Task<E_commerce.core.Models.Order.Order?> GetOrderByIdForSpecifcUser(string BuyerEmail, int OrderId)
        {
            var Spec = new OrderSpecifictin(BuyerEmail,OrderId);
            var order = await _unitOfWork.Repository<E_commerce.core.Models.Order.Order, int>().GetWithSpecAsync(Spec);
            return order;
        }

        public async Task<IReadOnlyList<E_commerce.core.Models.Order.Order>> GetOrderForSpecifcUser(string BuyerEmail)
        {
            var Spec= new OrderSpecifictin(BuyerEmail);

            var Orders = await _unitOfWork.Repository<E_commerce.core.Models.Order.Order, int>().GetAllWithSpecAsync(Spec);

            return Orders as IReadOnlyList<E_commerce.core.Models.Order.Order>;
        }
    }
}
