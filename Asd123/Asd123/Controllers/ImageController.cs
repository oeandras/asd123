using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Asd123.ApplicationService;
using System.Security.Claims;

namespace Asd123.Controllers
{
    //[Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService, IUserService userService)
        {
            _imageService = imageService;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            IEnumerable<Claim> a = facebookIdentity.Claims;
            var user = await _userService.GetById(a.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            //var files = fo.Files;
            // full path to file in temp location
            //var filePath = Path.GetTempFileName();
            Uri uploadedImageUri = null;
            //long size = files.Sum(f => f.Length);

            //foreach (var formFile in files)
            //{
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        uploadedImageUri = await _imageService.UploadImage(ms.ToArray(), user, file.FileName);
                    }


                    // using (var stream = new FileStream(filePath, FileMode.Create))
                    // {
                    //     await formFile.CopyToAsync(stream);
                    // }
                }
            //}

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { uploadedImageUri });
        }
    }
}