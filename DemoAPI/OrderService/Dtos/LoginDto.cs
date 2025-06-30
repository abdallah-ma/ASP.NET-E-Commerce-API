
namespace OrderService.Dtos
{
    public class LoginDto
    {


        public LoginDto(string password, string email)
        {
            Password = password;
            Email = email;
        }
        public string Password { get; set; }

        public string Email { get; set; }

    }
}
