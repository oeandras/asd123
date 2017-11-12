using Asd123.Domain;
using Asd123.Repository.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asd13.Repository.EF
{
    public class ImageRepository : GenericCrudRepository<ImageInfo>, IImageRepository
    {
        private CloudStorageAccount storageAccount;

        public ImageRepository(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            storageAccount = CloudStorageAccount.Parse(configuration["StorageConnectionString"]);
            Context = dbContext;
        }

        public async Task<ImageUploadResult> UploadImage(byte[] imageBytes)
        {
            // TODO: error handling
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            var fileId = Guid.NewGuid();
            var blob = container.GetBlockBlobReference(fileId.ToString());
            await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            return new ImageUploadResult
            {
                ImageId = fileId,
                ImageUri = blob.Uri
            };
        }

        public async Task EnqueueWorkItem(Guid imageId)
        {
            // TODO: error handling + retry policy
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("imageprocess");
            await queue.CreateIfNotExistsAsync();
            var message = new CloudQueueMessage(imageId.ToString());
            await queue.AddMessageAsync(message);
        }

        public async Task<ImageInfo> FindByIdentifier(string imageIdentifier)
        {
            var imageInfos = await FindAll(u => u.ImageId == imageIdentifier);


            return imageInfos.FirstOrDefault();
        }

        public async Task<string> GetBase64StringFromBlob(string id)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(id);
            string pic;
            using (MemoryStream ms = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(ms);
                pic = Convert.ToBase64String(ms.ToArray());
            }
            return pic;
        }
    }
}
