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

namespace Asd123.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IConfiguration _configurationRoot;

        public AccountController( IConfiguration configurationRoot)
        {
            _configurationRoot = configurationRoot;
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
        public IActionResult AuthCallback()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);

            if (facebookIdentity == null)
            {
                return Redirect(Url.Action("Login", "Account"));
            }

            var jwtSecurityToken = GenerateToken();
            string tokenstring = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Redirect(Url.Content("/account?token="+tokenstring));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("[action]")]
        public IActionResult ShowEmail()
        {
            var email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value; 
            return Ok(email);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect(Url.Action("Index", "Home"));
        }

        private JwtSecurityToken GenerateToken()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            string email = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            string name = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            //var userid = facebookIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.)?.Value;
            //TODO: search db for user, if not found create an account

            //get jwt token
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Name, name)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["Tokens:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _configurationRoot["Tokens:Issuer"],
                audience: _configurationRoot["Tokens:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            //token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            //expiration = jwtSecurityToken.ValidTo
        }
    }
}