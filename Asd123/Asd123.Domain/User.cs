using System;
using System.Collections.Generic;
using System.Text;

namespace Asd123.Domain
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserIdentifier { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public string Hometown { get; set; }

        public List<ImageInfo> UploadedImages { get; set; }
    }
}
