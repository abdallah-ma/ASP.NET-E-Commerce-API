using AccountService.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Interfaces
{
    public interface IAuthService
    {

        public Task<string> CreateToken(AppUser user,UserManager<AppUser> userManager);

    }
}
