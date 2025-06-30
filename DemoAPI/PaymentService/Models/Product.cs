using DemoAPI.Common;


namespace PaymentService.Models
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public int Price { get; set; }

        public string PictureUrl { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }




    }
}
