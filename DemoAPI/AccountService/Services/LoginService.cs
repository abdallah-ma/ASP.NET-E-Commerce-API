using Microsoft.AspNetCore.Identity;
using AccountService.Models;
using AccountService;
using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using AccountService.Interfaces;

namespace AccountService.Services
{
    public class LoginService : Login.LoginBase
    {
        private readonly ILogger<LoginService> _logger;

        private readonly UserManager<AppUser> _userManager;

        private readonly IAuthService _authService;
        public LoginService(ILogger<LoginService> logger, UserManager<AppUser> userManager, IAuthService authService)
        {
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
        }

        public async override Task<UserDto> Login(LoginDto loginDto, ServerCallContext context)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _authService.CreateToken(user, _userManager).Result
                };
            }
            return null;
        }

    }
}