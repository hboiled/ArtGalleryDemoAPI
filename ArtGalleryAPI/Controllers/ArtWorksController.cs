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
        private readonly GalleryContext _context;
        private readonly IGalleryData data;

        public ArtWorksController(GalleryContext context, IGalleryData data)
        {
            _context = context;
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
        public async Task<ActionResult<ArtWork>> DeleteArtWork(int id)
        {
            var artWork = await _context.ArtWork.FindAsync(id);
            if (artWork == null)
            {
                return NotFound();
            }

            _context.ArtWork.Remove(artWork);
            await _context.SaveChangesAsync();

            return artWork;
        }

        [HttpGet("search/{query}")]
        public List<ArtWork> GetWorksStartingWith(string query)
        {
            return data.SearchTitleStartsWith(query);
        }

        [HttpGet("year/{year}")]
        public List<ArtWork> GetWorksByYear(int year)
        {
            return data.FilterWorksByYear(year);
        }

        [HttpGet("artist/{artist}")]
        public List<ArtWork> GetWorksByArtist(string artist)
        {
            return data.FilterWorksByArtist(artist);
        }

        [HttpGet("genre/{genre}")]
        public List<ArtWork> GetWorksByYear(string genre)
        {
            return data.FilterWorksByGenre(genre);
        }

        [HttpGet("country/{country}")]
        public List<ArtWork> GetWorksByCountry(string country)
        {
            return data.FilterWorksByCountry(country);
        }

        [HttpGet("country")]
        public List<string> GetAllCountries()
        {
            return data.CountriesAvailable();
        }

        [HttpGet("genre")]
        public List<string> GetAllGenres()
        {
            return data.GenresAvailable();
        }

        [HttpGet("artist")]
        public List<string> GetAllArtists()
        {
            return data.ArtistsAvailable();
        }

        [HttpGet("year")]
        public List<int> GetAllYears()
        {
            return data.YearsAvailable();
        }

        private bool ArtWorkExists(int id)
        {
            return _context.ArtWork.Any(e => e.Id == id);
        }
    }
}
