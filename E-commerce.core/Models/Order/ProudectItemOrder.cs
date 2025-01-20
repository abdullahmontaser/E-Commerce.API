namespace E_commerce.core.Models.Order
{
    public class ProudectItemOrder
    {
        public ProudectItemOrder()
        {
            
        }
        public ProudectItemOrder(int proudectId, string proudectName, string pictureUrl)
        {
            ProudectId = proudectId;
            ProudectName = proudectName;
            PictureUrl = pictureUrl;
        }

        public int ProudectId { get; set; }
        public string ProudectName { get; set; }
        public string PictureUrl { get; set; }
    }
}