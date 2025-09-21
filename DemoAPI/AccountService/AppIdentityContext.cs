
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AccountService.Models;


namespace AccountService

{
    public class AppIdentityContext : IdentityDbContext<AppUser>
    {


        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
        }

    }
}
