using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtGalleryDataLibrary.DataAccess;
using ArtGalleryDataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IGalleryData<Painting> data;

        public FilterController(IGalleryData<Painting> data)
        {            
            this.data = data;
        }

        [HttpGet("search/{query}")]
        public List<Painting> GetWorksStartingWith(string query)
        {
            return data.SearchTitleStartsWith(query);
        }

        [HttpGet("year/{year}")]
        public List<Painting> GetWorksByYear(int year)
        {
            return data.FilterWorksByYear(year);
        }

        [HttpGet("artist/{artist}")]
        public List<Painting> GetWorksByArtist(string artist)
        {
            return data.FilterWorksByArtist(artist);
        }

        [HttpGet("genre/{genre}")]
        public List<Painting> GetWorksByYear(string genre)
        {
            return data.FilterWorksByGenre(genre);
        }

        [HttpGet("country/{country}")]
        public List<Painting> GetWorksByCountry(string country)
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
    }
}