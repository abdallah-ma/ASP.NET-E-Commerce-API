using Microsoft.AspNetCore.Mvc;
using DemoAPI.Common.Interfaces;
using DemoAPI.Common.Errors;
using DemoAPI.Common;
using AutoMapper;
using AccountService;

namespace AccountService.Extensions
{
    public static class AppServices
    {

        public static IServiceCollection AddServices(this IServiceCollection Services)
        {

            Services.AddControllers();


            Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));




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
