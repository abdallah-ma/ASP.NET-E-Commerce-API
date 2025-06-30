using OrderService.Interfaces;
using DemoAPI.Common.Errors;
using OrderService.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DemoAPI.Common.Interfaces;
using DemoAPI.Common;
using OrderService.Helpers;
using System.Net.Http.Headers;
using OrderService.Dtos;

namespace OrderService.Extensions
{
    public static class AppServices
    {

        public static IServiceCollection AddServices(this IServiceCollection Services)
        {

            Services.AddControllers();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();

            var Configuration = Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            Services.AddHttpClient<IBasketClient, BasketClient>(c =>
            {
                c.BaseAddress = new Uri(Configuration["ServicesUrls:BasketUrl"]);
            });

            Services.AddHttpClient<IProductClient, ProductClient>(c =>
            {
                c.BaseAddress = new Uri(Configuration["ServicesUrls:ProductUrl"]);
            });

            Services.AddHttpClient<IAccoutClient, AccountClient>(c =>
                c.BaseAddress = new Uri(Configuration["ServicesUrls:AccountUrl"])
            );

            Services.AddHttpClient<IPaymentClient, PaymentClient>(async c =>
            {
                var AccountClient = Services.BuildServiceProvider().GetRequiredService<IAccoutClient>();
                var LoginCreds = new LoginDto(Configuration["PaymentClientCreds:Password"] , Configuration["PaymentClientCreds:Email"]);
                var Response = await AccountClient.Login(LoginCreds);

                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Response.Token);
                c.BaseAddress = new Uri(Configuration["ServicesUrls:PaymentUrl"]);
            });

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

            Services.AddAuthentication("Bearer").
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"], // or use a separate "Audience" if you have one
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JWT:AuthKey"])),
                        ClockSkew = TimeSpan.FromDays(double.Parse(Configuration["JWT:DurationDays"]))
                    };
                });

            return Services;
        }

    }
}
