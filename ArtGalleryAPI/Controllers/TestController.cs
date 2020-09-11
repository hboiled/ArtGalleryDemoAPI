using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {        
        [Authorize]
        [HttpGet]
        public List<string> GetValues()
        {
            List<string> vals = new List<string>();
            vals.Add("abc");
            vals.Add("123");
            vals.Add("zzz");

            return vals;
        }
    }
}