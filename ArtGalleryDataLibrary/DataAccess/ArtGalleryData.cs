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
        private string table;

        public ArtGalleryData(IOptions<ConnectionStringOptions> options)
        {
            ConnectionStringOptions conStrOptions = options.Value;
            this.connectionString = conStrOptions.GalleryLite;
            setTargetTable();
        }

        private void setTargetTable()
        {
            switch (typeof(T).Name)
            {
                case "Sculpture":
                    table = "sculptures";
                    break;
                case "Painting":
                    table = "paintings";
                    break;
            }
        }

        public List<T> GetAllWorks()
        {
            string sql = $"select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from { table }";

            return db.LoadData<T, dynamic>(sql, new { }, connectionString);
        }

        public T GetWorkById(int id)
        {
            T output;

            string sql = $"select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from { table } where Id = @Id";

            output = db.LoadData<T, dynamic>(sql, new { Id = id }, connectionString).FirstOrDefault();

            return output;
        }

        public void CreateWork(T work)
        {
            // save basic contact
            string sql = $"insert into { table } (Title, Artist, Year, Genre, Review, Country, ImgPath) " +
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

        public void UpdateWork(T work)
        {
            string sql = $"update { table } set Title = @Title, Artist = @Artist, Year = @Year, Genre = @Genre, " +
                "Review = @Review, Country = @Country, ImgPath = @ImgPath where Id = @Id";
            db.SaveData(sql, work, connectionString);
        }

        public void RemoveWork(int workId)
        {
            string sql = $"delete from { table } where Id = @Id";
            db.SaveData(sql, new { Id = workId}, connectionString);
        }

        // search operations
        public List<T> SearchTitleStartsWith(string query)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                $"ImgPath from { table } where Title like @StartsWith";

            return db.LoadData<T, dynamic>(sql, new { StartsWith = query + "%" }, connectionString);
        }

        public List<T> FilterWorksByYear(int year)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                $"ImgPath from { table } where Year = @Year";

            return db.LoadData<T, dynamic>(sql, new { Year = year }, connectionString);
        }

        public List<T> FilterWorksByCountry(string country)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                $"ImgPath from { table } where Country = @Country";

            return db.LoadData<T, dynamic>(sql, new { Country = country }, connectionString);
        }

        public List<T> FilterWorksByArtist(string artist)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                $"ImgPath from { table } where Artist = @Artist";

            return db.LoadData<T, dynamic>(sql, new { Artist = artist }, connectionString);
        }

        public List<T> FilterWorksByGenre(string genre)
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, " +
                $"ImgPath from { table } where Genre = @Genre";

            return db.LoadData<T, dynamic>(sql, new { Genre = genre }, connectionString);
        }

        // lists of filter categories

        public List<string> ArtistsAvailable()
        {
            string sql = $"select distinct Artist from { table }";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
        public List<int> YearsAvailable()
        {
            string sql = $"select distinct Year from { table }";

            return db.LoadData<int, dynamic>(sql, null, connectionString);
        }
        public List<string> GenresAvailable()
        {
            string sql = $"select distinct Genre from { table }";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
        public List<string> CountriesAvailable()
        {
            string sql = $"select distinct Country from { table }";

            return db.LoadData<string, dynamic>(sql, null, connectionString);
        }
    }
}
