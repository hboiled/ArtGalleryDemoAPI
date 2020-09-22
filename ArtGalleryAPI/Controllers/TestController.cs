using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtGalleryAPI.Models;
using ArtGalleryDataLibrary.DataAccess;
using ArtGalleryDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IGalleryData data;

        public TestController(IGalleryData data)
        {
            this.data = data;
        }
        
        [HttpGet]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetTestData()
        {
            // TODO map types
            return data.GetAllWorks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtGalleryDataLibrary.Models.ArtWork>> GetArtWork(int id)
        {
            return data.GetWorkById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ArtGalleryDataLibrary.Models.ArtWork>> PostArtWork(ArtGalleryDataLibrary.Models.ArtWork artWork)
        {
            data.CreateContact(artWork);

            return CreatedAtAction("GetArtWork", new { id = artWork.Id }, artWork);
        }

        [HttpPut("{workId}")]
        public async Task<IActionResult> UpdateArtWork(int workId, ArtGalleryDataLibrary.Models.ArtWork artWork)
        {
            if (workId != artWork.Id)
            {
                return BadRequest();
            }

            data.UpdateWork(artWork);
            return NoContent();
        }

        [HttpDelete("{workId}")]
        public async Task<IActionResult> DeleteArtWork(int workId)
        {
            data.RemoveWork(workId);
            return NoContent();
        }

        [HttpGet("search/{query}")]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetWorksStartingWith(string query)
        {
            return data.SearchTitleStartsWith(query);
        }

        [HttpGet("filter/{year}")]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetWorksByYear(int year)
        {
            return data.FilterWorksByYear(year);
        }
    }
}