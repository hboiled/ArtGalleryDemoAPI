using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGalleryAPI.Areas.Identity.Data
{
    public class LoginModel
    {
        public string CuratorId { get; set; }
        public string Password { get; set; }
    }
}
