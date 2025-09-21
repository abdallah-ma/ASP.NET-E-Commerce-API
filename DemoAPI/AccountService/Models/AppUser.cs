using Microsoft.AspNetCore.Identity;

namespace AccountService.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address? Address { get; set; }
    }
}
