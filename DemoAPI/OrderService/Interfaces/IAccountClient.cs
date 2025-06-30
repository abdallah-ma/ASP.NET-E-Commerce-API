

using OrderService.Dtos;

namespace OrderService.Interfaces
{

    public interface IAccoutClient
    {

        public Task<UserDto> Login(LoginDto loginDto);


    }


}