using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGalleryAPI.Data;
using Microsoft.AspNetCore.Authorization;
using ArtGalleryDataLibrary.DataAccess;
using ArtGalleryDataLibrary.Models;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorksController : ControllerBase
    {
        private readonly IGalleryData data;

        public ArtWorksController(IGalleryData data)
        {
            this.data = data;
        }

        // GET: api/ArtWorks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtWork>>> GetArtWork()
        {
            return data.GetAllWorks();
        }

        // GET: api/ArtWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWork>> GetArtWork(int id)
        {
            var artWork = data.GetWorkById(id);

            if (artWork == null)
            {
                return NotFound();
            }

            return artWork;
        }

        // PUT: api/ArtWorks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Curator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtWork(int id, ArtWork artWork)
        {
            if (id != artWork.Id)
            {
                return BadRequest();
            }

            data.UpdateWork(artWork);

            return NoContent();
        }

        // POST: api/ArtWorks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ArtWork>> PostArtWork(ArtWork artWork)
        {
            data.CreateContact(artWork);

            return CreatedAtAction("GetArtWork", new { id = artWork.Id }, artWork);
        }

        // DELETE: api/ArtWorks/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteArtWork(int id)
        {
            data.RemoveWork(id);
        }

            
    }
}
