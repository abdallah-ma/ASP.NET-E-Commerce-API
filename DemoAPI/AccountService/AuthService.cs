using AccountService.Models;
using AccountService.Interfaces;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AccountService
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateToken(AppUser User, UserManager<AppUser> userManager)
        {

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName ,User.DisplayName),
                new Claim(ClaimTypes.Email , User.Email)
            };

            var Roles = userManager.GetRolesAsync(User).Result;

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]));

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Issuer"],
                claims: Claims,
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationDays"])),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );


            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}
