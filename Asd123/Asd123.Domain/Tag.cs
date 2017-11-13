using System;
using System.Collections.Generic;
using System.Text;

namespace Asd123.Domain
{
    public class Tag : Entity
    {
        public string Text { get; set; }

        public ICollection<PictureTag> PictureTags { get; set; }
    }
}
