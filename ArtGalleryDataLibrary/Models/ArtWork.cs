using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGalleryDataLibrary.Models
{
    public abstract class ArtWork : IArtWork
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Genre { get; set; }
        //[Required]
        public string Review { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ImgPath { get; set; }
    }
}
