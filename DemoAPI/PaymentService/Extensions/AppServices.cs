using PaymentService.Interfaces;
using PaymentService.Clients;


namespace PaymentService.Extensions
{
    public static class AppServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddHttpClient<IBasketClient, BasketClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServicesUrls:BasketUrl"]);
            });

            services.AddHttpClient<IProductClient, ProductClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServicesUrls:ProductUrl"]);
            });

            services.AddHttpClient<IOrderClient, OrderClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServicesUrls:OrderUrl"]);
            });

            return services;
        }
        
    }
}