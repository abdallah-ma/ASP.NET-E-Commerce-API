namespace OrderService.Dtos
{
    public class OrderToReturnDto
    {


        public int Id { get; set; }

        
        public string BuyerEmail { get; set; }
        
        public AddressDto ShippingAddress { get; set; }
        
        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }
        

        
        public decimal Total { get; set; }
        
        public DateTimeOffset OrderDate { get; set; }

        public decimal Subtotal { get; set; }

        public string Status { get; set; }
        
        public IReadOnlyList<OrderItemToReturnDto> Items { get; set; }


    }
}
