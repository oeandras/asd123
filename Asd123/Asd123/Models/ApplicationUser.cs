using Microsoft.AspNetCore.Identity;

namespace Asd123
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}