using ArtGalleryDataLibrary.Models;
using System.Collections.Generic;

namespace ArtGalleryDataLibrary.DataAccess
{
    public interface IGalleryData
    {
        List<ArtWork> GetAllWorks();
        ArtWork GetWorkById(int id);
        void CreateContact(ArtWork work);
        void UpdateWork(ArtWork work);
        void RemoveWork(int workId);

        // search operations, extract to another interface>
        List<ArtWork> SearchTitleStartsWith(string query);
        List<ArtWork> FilterWorksByYear(int year);
        List<ArtWork> FilterWorksByArtist(string artist);
        List<ArtWork> FilterWorksByGenre(string genre);
        List<ArtWork> FilterWorksByCountry(string country);

        // for filter display options

        List<string> ArtistsAvailable();
        List<int> YearsAvailable();
        List<string> GenresAvailable();
        List<string> CountriesAvailable();
    }
}