using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using ArtGalleryAPI.Models;

namespace ArtGalleryAPI.Data
{
    public class GalleryContext : DbContext
    {
        public GalleryContext (DbContextOptions<GalleryContext> options)
            : base(options)
        {
        }

        //public DbSet<ArtGalleryAPI.Models.ArtWork> ArtWork { get; set; }
    }
}
