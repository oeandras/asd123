using System;
using System.Collections.Generic;
using System.Text;

namespace Asd123.Domain
{
    public class PictureTag : Entity
    {
        public ImageInfo Image { get; set; }
        public Tag Tag { get; set; }
    }
}
