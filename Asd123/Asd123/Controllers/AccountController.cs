using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Asd123.DTO;
using Asd123.ApplicationService;

namespace Asd123.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IConfiguration _configurationRoot;
        private readonly IUserService _userService;

        public AccountController(IConfiguration configurationRoot, IUserService userService)
        {
            _configurationRoot = configurationRoot;
            _userService = userService;
        }

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
        public async Task<IActionResult> AuthCallback()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);

            if (facebookIdentity == null)
            {
                return Redirect(Url.Action("Login", "Account"));
            }

            IEnumerable<Claim> a = facebookIdentity.Claims;
            await _userService.EnsureUser(facebookIdentity.Claims.ToList());

            return Redirect(Url.Content("/upload"));
        }

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult ShowEmail()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            string email = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return Ok(email);
        }

        [Authorize]
        [HttpPost("[action]")]
        public UserDto GetLoggedInUserInfo()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            string email = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            string name = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string gender = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Gender)?.Value;
            string dateOfBirth = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.DateOfBirth)?.Value;
            string locale = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Locality)?.Value;
            string uri = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Uri)?.Value;
            string id = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            string country = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country)?.Value;

            return new UserDto { Name = name, Email = email, DateOfBirth = dateOfBirth, Gender = gender, Id = id, Locale = locale, HomeTown = country };
        }



        [HttpGet("[action]")]
        public IActionResult Logout()
        {
           
       
            HttpContext.SignOutAsync();
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}