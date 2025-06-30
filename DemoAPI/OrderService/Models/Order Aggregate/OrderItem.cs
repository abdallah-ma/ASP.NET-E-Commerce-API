using DemoAPI.Common;

namespace OrderService.Models
{
    public class OrderItem : BaseEntity
    {

        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrdered product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrdered Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
