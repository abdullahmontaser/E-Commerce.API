using E_commerce.core.Models.Order;

namespace E_commerce.core.Dtos.Order
{
    public class OrderItemDto
    {
        public int ProudectId { get; set; }
        public string ProudectName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}