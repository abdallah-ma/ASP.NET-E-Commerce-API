using OrderService.Interfaces;
using DemoAPI.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DemoAPI.Common.Interfaces;
using DemoAPI.Common;
using OrderService.Helpers;

using BasketClient = OrderService.BasketGrpcService.BasketGrpcServiceClient;
using PaymentClient = OrderService.PaymentGrpcService.PaymentGrpcServiceClient;
using ProductClient = OrderService.ProductGrpcService.ProductGrpcServiceClient;
using System.Net;

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

            Services.AddGrpcClient<BasketClient>(o =>
            {
                o.Address = new Uri(Configuration.GetSection("ServicesUrls:BasketUrl").Value);

            }).ConfigureChannel(o => o.HttpVersion = HttpVersion.Version20);

            Services.AddGrpcClient<PaymentClient>(o =>
            {
                o.Address = new Uri(Configuration.GetSection("ServicesUrls:PaymentUrl").Value);

            }).ConfigureChannel(o => o.HttpVersion = HttpVersion.Version20);

            Services.AddGrpcClient<ProductClient>(o =>
            {
                o.Address = new Uri(Configuration.GetSection("ServicesUrls:ProductUrl").Value);

            }).ConfigureChannel(o => o.HttpVersion = HttpVersion.Version20);

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
