using System;
using System.Collections.Generic;
using System.Text;

namespace Asd123.Domain
{
    public class ImageInfo : Entity
    {
        //UserIdentifier
        public string UploadedBy { get; set; }

        public string Name { get; set; }

        public string ImageUri { get; set; }

        public string ImageId { get; set; }
    }
}
