using Asd123.Domain;
using Asd13.Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asd123.ApplicationService
{
    public class ImageService : IImageService
    {
        private IImageRepository imageRepo;

        public ImageService(IImageRepository imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        public async Task<Uri> UploadImage(byte[] imageBytes, User uploader, string name)
        {
            ImageUploadResult result = await imageRepo.UploadImage(imageBytes);
            await imageRepo.EnqueueWorkItem(result.ImageId);
            ImageInfo info = new ImageInfo()
            {
                ImageId = result.ImageId.ToString(),
                UploadedBy = uploader.UserIdentifier,
                ImageUri = result.ImageUri.ToString(),
                Name = name
            };
            await imageRepo.Create(info);
            return result.ImageUri;
        }
    }
}
