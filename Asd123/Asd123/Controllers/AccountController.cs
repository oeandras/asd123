using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Asd123.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult LoginFacebook()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("AuthCallback", "Account")
            };


            return Challenge(authenticationProperties, "Facebook");
        }

        [HttpGet("[action]")]
        public IActionResult AuthCallback()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);

            if (facebookIdentity == null)
            {
                return Redirect(Url.Action("Login", "Account"));
            }

            string email = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value; // TODO: <--- based on this, create a proprietary user account etc.

            return Redirect(Url.Action("ShowEmail", "Account"));
        }

        [HttpGet("[action]")]
        public string ShowEmail()
        {
            return User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}