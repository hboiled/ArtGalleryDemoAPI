using System;
using System.Collections.Generic;
using System.Text;

namespace ArtGalleryDataLibrary.Models
{
    public interface IArtWork
    {
        int Id { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        int Year { get; set; }       
        string Genre { get; set; }        
        string Review { get; set; }        
        string Country { get; set; }
        string ImgPath { get; set; }
    }
}
