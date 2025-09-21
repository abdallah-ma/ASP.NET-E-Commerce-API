using AccountService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AccountService.Interfaces;

namespace AccountService.Extensions
{
    public static class AppIdentityServices
    {

        public static void AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityContext>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication("Bearer").
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Issuer"], // or use a separate "Audience" if you have one
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"])),
                        ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationDays"]))
                    };
                });

        }


    }
}
