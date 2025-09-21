using AccountService.Interfaces;
using AccountService.Models;
using AccountService.Dtos;
using AccountService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DemoAPI.Common;
using DemoAPI.Common.Errors;
using AutoMapper;

namespace AccountService
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseAPIController
    {


           
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AccountsController(UserManager<AppUser> userManager,
            IAuthService authService,
            IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _mapper = mapper;
        }


        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(Dtos.LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            
            

            if (user == null) return Unauthorized(new ApiResponse(401, null));


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized(new ApiResponse(401 , null));

            var reply = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _authService.CreateToken(user, _userManager).Result
            };

            return Ok(reply);
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if(!ModelState.IsValid)
            {
                Console.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (CheckEmailExists(registerDto.Email).Result.Value)
            {
                Console.WriteLine("Email already exists");
                return BadRequest(new ApiResponse(400, "Email already exists"));
            }

            var user = new AppUser()
            {
                DisplayName = registerDto.Name,
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                Console.WriteLine("User creation failed");
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new UserDto()
            {
                DisplayName = registerDto.Name
                ,
                Email = registerDto.Email
                ,
                Token = _authService.CreateToken(user, _userManager).Result
            });
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _authService.CreateToken(user, _userManager).Result
            });

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserWithAddress(User);

            if (user == null || user.Address == null)
            {
                return NotFound(new ApiResponse(404, "User or Address not found"));
            }

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAddress(User);
            if (user == null) return NotFound(new ApiResponse(404, "User not found"));

            user.Address = _mapper.Map<AddressDto, Address>(addressDto);



            var result = await _userManager.UpdateAsync(user);


            if (!result.Succeeded) return BadRequest(new ApiResponse(400 , null));

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        } 

    }
}

