using ArtGalleryDataLibrary.Models;
using System.Collections.Generic;

namespace ArtGalleryDataLibrary.DataAccess
{
    public interface IGalleryData<T>
    {
        List<T> GetAllWorks();
        T GetWorkById(int id);
        void CreateWork(T work);
        void UpdateWork(T work);
        void RemoveWork(int workId);

        // search operations, extract to another interface>
        List<T> SearchTitleStartsWith(string query);
        List<T> FilterWorksByYear(int year);
        List<T> FilterWorksByArtist(string artist);
        List<T> FilterWorksByGenre(string genre);
        List<T> FilterWorksByCountry(string country);

        // for filter display options

        List<string> ArtistsAvailable();
        List<int> YearsAvailable();
        List<string> GenresAvailable();
        List<string> CountriesAvailable();
    }
}