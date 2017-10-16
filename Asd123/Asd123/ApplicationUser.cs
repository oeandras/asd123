using Microsoft.AspNetCore.Identity;

namespace Asd123
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
    }
}