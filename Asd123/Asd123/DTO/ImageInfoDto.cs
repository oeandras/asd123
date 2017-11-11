using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asd123.DTO
{
    public class ImageInfoDto
    {
        public string UploadedBy { get; set; }

        public string Name { get; set; }

        public string ImageUri { get; set; }

        public string ImageId { get; set; }

        public DateTimeOffset UploadedAt { get; set; }

        public string Base64Picture { get; set; }
    }
}
