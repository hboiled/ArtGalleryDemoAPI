using ArtGalleryAPI.Helpers;
using ArtGalleryDataLibrary.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtGalleryDataLibrary.DataAccess
{
    public class ArtGalleryData<T> : IGalleryData<T> where T : IArtWork
    {
        readonly string connectionString;
        private SqliteDataAccess db = new SqliteDataAccess();

        public ArtGalleryData(IOptions<ConnectionStringOptions> options)
        {
            ConnectionStringOptions conStrOptions = options.Value;
            this.connectionString = conStrOptions.GalleryLite;
        }

        public List<T> GetAllWorks()
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from paintings";

            return db.LoadData<T, dynamic>(sql, new { }, connectionString);
        }

        public T GetWorkById(int id)
        {
            T output;

            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from paintings where Id = @Id";

            output = db.LoadData<T, dynamic>(sql, new { Id = id }, connectionString).FirstOrDefault();

            return output;
        }

        public void CreateWork(ArtWork work)
        {
            // save basic contact
            string sql = "insert into paintings (Title, Artist, Year, Genre, Review, Country, ImgPath) " +
                "values (@Title, @Artist, @Year, @Genre, @Review, @Country, @ImgPath);";


            db.SaveData(sql, new
            {
                Title = work.Title,
                Artist = work.Artist,
                Year = work.Year,
                Genre = work.Genre,
                Review = work.Review,
                Country = work.Country,
                ImgPath = work.ImgPath
            },
                connectionString);

        }

        public void UpdateWork(ArtWork work)
        {
            string sql = "update paintings set Title = @Title, Artist = @Artist, Year = @Year, Genre = @Genre, " +
                "Review = @Review, Country = @Country, ImgPath = @ImgPath where Id = @Id";
            db.SaveData(sql, work, connectionString);
        }

        public void RemoveWork(int workId)
        {
            string sql = "delete from paintings where Id = @Id";
            db.SaveData(sql, new { Id = workId}, connectionString);
        }

        // search operations
        public List<ArtWork> SearchTitleStartsWith(string query)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                "ImgPath from paintings where Title like @StartsWith";

            return db.LoadData<ArtWork, dynamic>(sql, new { StartsWith = query + "%" }, connectionString);
        }

        public List<ArtWork> FilterWorksByYear(int year)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                "ImgPath from paintings where Year = @Year";

            return db.LoadData<ArtWork, dynamic>(sql, new { Year = year }, connectionString);
        }

        public List<ArtWork> FilterWorksByCountry(string country)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                "ImgPath from paintings where Country = @Country";

            return db.LoadData<ArtWork, dynamic>(sql, new { Country = country }, connectionString);
        }

        public List<ArtWork> FilterWorksByArtist(string artist)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                "ImgPath from paintings where Artist = @Artist";

            return db.LoadData<ArtWork, dynamic>(sql, new { Artist = artist }, connectionString);
        }

        public List<ArtWork> FilterWorksByGenre(string genre)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                "ImgPath from paintings where Genre = @Genre";

            return db.LoadData<ArtWork, dynamic>(sql, new { Genre = genre }, connectionString);
        }

        // lists of filter categories

        public List<string> ArtistsAvailable()
        {
            string sql = "select distinct Artist from paintings";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
        public List<int> YearsAvailable()
        {
            string sql = "select distinct Year from paintings";

            return db.LoadData<int, dynamic>(sql, null, connectionString);
        }
        public List<string> GenresAvailable()
        {
            string sql = "select distinct Genre from paintings";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
        public List<string> CountriesAvailable()
        {
            string sql = "select distinct Country from paintings";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
    }
}
