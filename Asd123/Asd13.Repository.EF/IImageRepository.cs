using Asd123.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asd13.Repository.EF
{
    public interface IImageRepository
    {
        Task<ImageUploadResult> UploadImage(byte[] imageBytes);
        Task EnqueueWorkItem(Guid imageId);

        Task Create(ImageInfo entity);
        Task<ImageInfo> FindByIdentifier(string userIdentifier);
        Task<IReadOnlyCollection<ImageInfo>> FindAll(Expression<Func<ImageInfo, bool>> filterExpression);
        Task<string> GetBase64StringFromBlob(string id);
    }
}
