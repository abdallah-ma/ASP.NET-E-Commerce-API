using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PaymentService.Extensions
{
    public static class AppIdentityServices
    {

        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAuthentication("Bearer").
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,

                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudiences = configuration.GetSection("JWT:Audiences").Get<string[]>(),
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"])),
                        ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationDays"]))
                    };
                });

        }


    }
}
