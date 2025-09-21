using Microsoft.AspNetCore.Identity;
using AccountService.Models;
using AccountService;
using Grpc.Core;

namespace AccountService.Services
{
    public class LoginService
    {
        private readonly ILogger<LoginService> _logger;

        private readonly UserManager<AppUser> _userManager;

        public LoginService(ILogger<LoginService> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
        }

        public override Task<UserDto> Login(LoginDto loginDto, ServerCallContext context)
        {


        }

    }
}