using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.BusinessLayer.Exceptions;
using VirtualArtGallery.BusinessLayer.Repository;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Service
{
    public class VirtualArtGalleryImpl : IVirtualArtGalleryImpl
    {
        private readonly IVirtualArtGalleryRepository _galleryRepository;

        // Constructor injection for the DAO layer (Repository layer)
        public VirtualArtGalleryImpl(IVirtualArtGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        // Artwork Management methods
        public bool AddArtwork(Artwork artwork)
        {
            return _galleryRepository.AddArtwork(artwork);
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            return _galleryRepository.UpdateArtwork(artwork);
        }

        public bool RemoveArtwork(int artworkID)
        {
            return _galleryRepository.RemoveArtwork(artworkID);
        }

        public Artwork GetArtworkById(int artworkID)
        {
            var artwork = _galleryRepository.GetArtworkById(artworkID);
            if (artwork == null)
            {
                throw new ArtWorkNotFoundException($"Artwork with ID {artworkID} not found.");
            }
            return artwork;
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            return _galleryRepository.SearchArtworks(keyword);
        }

        // User Favorites methods
        public bool AddArtworkToFavorite(int userID, int artworkID)
        {
            return _galleryRepository.AddArtworkToFavorite(userID, artworkID);
        }

        public bool RemoveArtworkFromFavorite(int userID, int artworkID)
        {
            return _galleryRepository.RemoveArtworkFromFavorite(userID, artworkID);
        }

        public List<Artwork> GetUserFavoriteArtworks(int userID)
        {
            var user =  _galleryRepository.GetUserFavoriteArtworks(userID);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userID} not found.");
            }

            return _galleryRepository.GetUserFavoriteArtworks(userID);
        }
    }
}
