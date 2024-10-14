using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class FavoriteArtworkRepository : IFavoriteArtworkRepository
    {
        // Default Constructor
        public FavoriteArtworkRepository() { }

        FavoriteArtwork favoriteArtwork = new FavoriteArtwork();

        // Parametarized Constructor
        public FavoriteArtworkRepository(int UserID, int ArtworkID)
        {
            favoriteArtwork.UserID = UserID;
            favoriteArtwork.ArtworkID = ArtworkID;
        }
        public override string ToString()
        {
            return $"UserFavoriteArtwork [UserID={favoriteArtwork.UserID}, ArtworkID={favoriteArtwork.ArtworkID}]";
        }

    }
}