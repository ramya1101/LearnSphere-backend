using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LearnSphere.Dtos
{
    public class LoginDto
    {
        //public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string Role { get; set; }
    }
}
