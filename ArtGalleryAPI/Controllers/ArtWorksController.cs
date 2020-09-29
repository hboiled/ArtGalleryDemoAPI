using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var works = data.GetAllWorks();

            if (works.Count <= 0)
            {
                return NoContent();
            }

            return works;
        }

        // GET: api/ArtWorks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWork>> GetArtWork(int id)
        {
            var artWork = data.GetWorkById(id);

            if (artWork == null)
            {
                return NotFound("Work does not exist");
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
                return BadRequest("Id sent did not match that of the work");
            }

            data.UpdateWork(artWork);

            return NoContent();
        }

        // POST: api/ArtWorks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Curator")]
        [HttpPost]
        public async Task<ActionResult<ArtWork>> PostArtWork(ArtWork artWork)
        {
            data.CreateContact(artWork);

            return CreatedAtAction("GetArtWork", new { id = artWork.Id }, artWork);
        }

        // DELETE: api/ArtWorks/5
        [Authorize(Roles = "Curator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteArtWork(int id)
        {
            var work = data.GetWorkById(id);

            if (work == null)
            {
                return BadRequest("Work does not exist");
            }

            data.RemoveWork(id);

            return NoContent();
        }

            
    }
}
