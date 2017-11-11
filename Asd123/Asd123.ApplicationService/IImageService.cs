using Asd123.Domain;
using System;
using System.Threading.Tasks;

namespace Asd123.ApplicationService
{
    public interface IImageService
    {
        Task<Uri> UploadImage(byte[] imageBytes, User uploader, string name);
    }
}
