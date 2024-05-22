using Microsoft.AspNetCore.Identity;

namespace WebApplication9.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
