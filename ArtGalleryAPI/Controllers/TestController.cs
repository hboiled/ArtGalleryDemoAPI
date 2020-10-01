using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        //private readonly IGalleryData data;

        public TestController(
            //IGalleryData data
            )
        {
            //this.data = data;
        }
        
        [HttpGet]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetTestData()
        {
            // TODO map types
            return null;// data.GetAllWorks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWork>> GetArtWork(int id)
        {
            return null;//data.GetWorkById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ArtWork>> PostArtWork(ArtGalleryDataLibrary.Models.ArtWork artWork)
        {
            //data.CreateWork(artWork);

            return CreatedAtAction("GetArtWork", new { id = artWork.Id }, artWork);
        }

        [HttpPut("{workId}")]
        public async Task<IActionResult> UpdateArtWork(int workId, ArtWork artWork)
        {
            if (workId != artWork.Id)
            {
                return BadRequest();
            }

            //data.UpdateWork(artWork);
            return NoContent();
        }

        [HttpDelete("{workId}")]
        public async Task<IActionResult> DeleteArtWork(int workId)
        {
            //data.RemoveWork(workId);
            return NoContent();
        }

        [HttpGet("search/{query}")]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetWorksStartingWith(string query)
        {
            return null;//data.SearchTitleStartsWith(query);
        }

        [Authorize(Roles = "Curator")]
        [HttpGet("year/{year}")]
        public List<ArtGalleryDataLibrary.Models.ArtWork> GetWorksByYear(int year)
        {
            return null;// data.FilterWorksByYear(year);
        }

        

    }
}