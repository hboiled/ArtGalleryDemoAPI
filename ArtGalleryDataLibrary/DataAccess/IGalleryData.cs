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
    }
}