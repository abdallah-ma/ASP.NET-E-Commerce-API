

using System.Text;
using System.Text.Json;
using OrderService.Dtos;
using OrderService.Interfaces;

namespace OrderService.Clients
{

    public class AccountClient : IAccoutClient
    {

        private readonly HttpClient _HttpClient;

        public AccountClient(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var json = JsonSerializer.Serialize(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _HttpClient.PostAsync("api/Accounts/Login", content);

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }


    }

}