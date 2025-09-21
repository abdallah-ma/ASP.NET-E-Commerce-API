using PaymentService.Interfaces;
using PaymentService;

using BasketGrpcClient = PaymentService.BasketGrpcService.BasketGrpcServiceClient;
using ProductGrpcClient = PaymentService.ProductGrpcService.ProductGrpcServiceClient;
using OrderGrpcClient = PaymentService.OrderGrpcService.OrderGrpcServiceClient;

namespace PaymentService.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddGrpcClient<BasketGrpcClient>(o =>
            {
                o.Address = new Uri(configuration["ServicesUrls:BasketUrl"]);
            });

            services.AddGrpcClient<ProductGrpcClient>(o =>
            {
                o.Address = new Uri(configuration["ServicesUrls:ProductUrl"]);
            });

            services.AddGrpcClient<OrderGrpcClient>(o =>
            {
                o.Address = new Uri(configuration["ServicesUrls:OrderUrl"]);
            });

            return services;
        }

    }
}