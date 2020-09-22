using ArtGalleryDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtGalleryDataLibrary.DataAccess
{
    public class ArtGalleryData : IGalleryData
    {
        readonly string connectionString;
        private SqliteDataAccess db = new SqliteDataAccess();

        public ArtGalleryData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ArtWork> GetAllWorks()
        {
            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from artworks";

            return db.LoadData<ArtWork, dynamic>(sql, new { }, connectionString);
        }

        public ArtWork GetWorkById(int id)
        {
            ArtWork output;

            string sql = "select Id, Title, Artist, Year, Genre, Review, Country, ImgPath from artworks where Id = @Id";

            output = db.LoadData<ArtWork, dynamic>(sql, new { Id = id }, connectionString).FirstOrDefault();

            return output;
        }

        public void CreateContact(ArtWork work)
        {
            // save basic contact
            string sql = "insert into artworks (Title, Artist, Year, Genre, Review, Country, ImgPath) " +
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
            string sql = "update artworks set Title = @Title, Artist = @Artist, Year = @Year, Genre = @Genre, " +
                "Review = @Review, Country = @Country, ImgPath = @ImgPath where Id = @Id";
            db.SaveData(sql, work, connectionString);
        }

        public void RemoveWork(int workId)
        {
            string sql = "delete from artworks where Id = @Id";
            db.SaveData(sql, new { Id = workId}, connectionString);
        }
    }
}
