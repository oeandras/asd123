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

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files.Any((f) => !f.ContentType.StartsWith(@"image/")))
            {
                return new UnsupportedMediaTypeResult();
            }
            var facebookIdentity = User.Identities.FirstOrDefault(i => i.AuthenticationType == "Facebook" && i.IsAuthenticated);
            IEnumerable<Claim> a = facebookIdentity.Claims;
            var user = await _userService.GetById(a.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            List<Uri> uploadedImageUris = new List<Uri>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var uploadedImageUri = await _imageService.UploadImage(ms.ToArray(), user, file.FileName);
                        uploadedImageUris.Add(uploadedImageUri);
                    }
                }
            }
            
            return Ok(new { uploadedImageUris });
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
                ImageUri = x.ImageUri
            });
        }
    }
}