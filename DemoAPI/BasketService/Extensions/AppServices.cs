using BasketService.Interfaces;
using BasketService.Models;
using DemoAPI.Common.Errors;
using BasketService;
using BasketService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Extensions
{
    public static class AppServices
    {

        public static IServiceCollection AddServices(this IServiceCollection Services)
        {

            Services.AddControllers();
            

            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ProductUrlResolver>();

           Services.AddAutoMapper(M => M.AddProfile<MappingProfiles>());

           Services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = (Context) =>
           {
               var Errors = Context.ModelState.Where(e => e.Value.Errors.Count > 0)
                   .SelectMany(x => x.Value.Errors)
                   .Select(x => x.ErrorMessage).ToArray();

               var Response = new BadRequestResponse()
               {
                   Errors = Errors
               };
               return new BadRequestObjectResult(Response);
           }


           );


            return Services;
        }

    }
}
