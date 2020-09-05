using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGalleryAPI.Models
{
    public class ArtWork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Review { get; set; }
        public string Country { get; set; }
        public string ImgPath { get; set; }
    }
}
