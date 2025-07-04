
using DemoAPI.Common.Interfaces;
using DemoAPI.Common;
using ProductService.Interfaces;
using ProductService.Helpers;


namespace ProductService.Extensions
{

    public static class AppServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            return services;
        }
    }

}
