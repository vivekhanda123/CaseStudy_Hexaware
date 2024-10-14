using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    public interface IVirtualArtGalleryRepository
    {
        // Artwork Operations
        bool AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);

        // User Favorites
        bool AddArtworkToFavorite(int userID, int artworkID);
        bool RemoveArtworkFromFavorite(int userID, int artworkID);
        List<Artwork> GetUserFavoriteArtworks(int userID);
    }
}
