using AccountService.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AccountService.Extensions
{
    public static class GetUserAddress
    {

        public static async Task<AppUser?> FindUserWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user =  await userManager.FindByEmailAsync(Email);

            return await userManager.Users.Include(U => U.Address).Where(U => U.Email == Email).FirstOrDefaultAsync();
        }
        
    }
}
