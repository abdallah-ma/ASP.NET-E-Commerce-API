using PaymentService.Models;


namespace PaymentService.Interfaces
{
    public interface IProductClient
    {
        Task<GrpcProduct> GetProduct(GrpcProductId id);

    }
}