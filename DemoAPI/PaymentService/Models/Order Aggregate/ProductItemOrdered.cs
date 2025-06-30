
namespace PaymentService.Models

{
    public class ProductItemOrdered
    {

        public ProductItemOrdered()
        {

        }

        public ProductItemOrdered(string name, string pictureUrl, int productId)
        {
            Name = name;
            PictureUrl = pictureUrl;
            ProductId = productId;
        }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public int ProductId { get; set; }


    }
}