using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace AccountService.Models.Identity

{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public Address Address { get; set; }


    }
}
