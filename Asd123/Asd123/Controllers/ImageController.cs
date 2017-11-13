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
using Asd123.DTO;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            IEnumerable<Claim> a = facebookIdentity.Claims;
            var user = await _userService.GetById(a.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            Uri uploadedImageUri = null;
            foreach (var file in files)
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        uploadedImageUri = await _imageService.UploadImage(ms.ToArray(), user, file.FileName);
                    }
                }

            {

            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { uploadedImageUri });
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ImageInfoDto>> GetUserImages()
        {
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            IEnumerable<Claim> a = facebookIdentity.Claims;
            var user = await _userService.GetById(a.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var images = await _imageService.FetchImagesOfUser(user);
            return images.Select(x => new ImageInfoDto
            {
                UploadedAt = x.CreatedAt,
                Name = x.Name,
                UploadedBy = x.UploadedBy.Name,
                ImageId = x.Id.ToString(),
                ImageUri = x.ImageUri,
                //Base64Picture = _imageService.GetBase64String(x.ImageId).Result
            });
        }
    }
}