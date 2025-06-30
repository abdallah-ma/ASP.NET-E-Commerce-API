using PaymentService.Models;
using DemoAPI.Common;

namespace PaymentService.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {


        public OrderSpecifications(int id,string buyerEmail) :
            base(O => O.Id == id && O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
        }

        public OrderSpecifications(string buyerEmail) :
            base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.Items);
            Includes.Add(o => o.DeliveryMethod);
        }

    }
}
