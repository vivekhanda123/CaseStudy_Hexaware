using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class ArtworkGalleryRepository : IArtworkGalleryRepository
    {
        // Default Constructor
        public ArtworkGalleryRepository() { }

        ArtworkGallery artworkGallery = new ArtworkGallery();

        public ArtworkGalleryRepository(int artworkID, int galleryID)
        {
            artworkGallery.ArtworkID = artworkID;
            artworkGallery.GalleryID = galleryID;
        }

        public override string ToString()
        {
            return $"ArtworkGallery [ArtworkID={artworkGallery.ArtworkID}, GalleryID={artworkGallery.GalleryID}]";
        }
    }
}
