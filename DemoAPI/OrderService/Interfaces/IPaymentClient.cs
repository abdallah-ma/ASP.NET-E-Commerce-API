
namespace OrderService.Interfaces


{
    public interface IPaymentClient
    {
        public Task CreateOrUpdatePaymentIntentAsync(string BasketId);
    }
}