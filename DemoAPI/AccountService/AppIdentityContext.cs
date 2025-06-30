using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AccountService.Models.Identity;


namespace AccountService

{
    public class AppIdentityContext : IdentityDbContext<AppUser>
    {


        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
        }

    }
}
