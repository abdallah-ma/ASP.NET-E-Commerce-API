using DemoAPI.Common;

namespace PaymentService.Models
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
            DeliveryTimes = deliveryTimes;
        }

        public string ShortName { get; set; }

        public string  Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTimes { get; set; }

    }
}
