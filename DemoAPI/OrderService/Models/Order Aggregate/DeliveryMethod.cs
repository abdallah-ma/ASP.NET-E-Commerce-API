using DemoAPI.Common;

namespace OrderService.Models
{
    public class DeliveryMethod : BaseEntity
    {

        public DeliveryMethod()
        {
            
        }

        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTimes)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTimes;
        }

        public string ShortName { get; set; }

        public string  Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }

    }
}
