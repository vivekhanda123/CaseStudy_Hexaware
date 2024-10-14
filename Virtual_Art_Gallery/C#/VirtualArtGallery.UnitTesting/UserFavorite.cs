using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.BusinessLayer.Repository;
using VirtualArtGallery.BusinessLayer.Service;
using VirtualArtGallery.Entity;


namespace VirtualArtGallery.UnitTesting
{
    [TestFixture]
    public class UserFavorite
    {
        VirtualArtGalleryRepository virtualArtGalleryRepository;

        [SetUp]
        public void initialiazation()
        {
            virtualArtGalleryRepository = new VirtualArtGalleryRepository();
        }

        // a. Test adding artwork to favorites.
        [Test]
        public void AddArtworkToFavoriteTest()
        {

            var addResult = virtualArtGalleryRepository.AddArtwork(new Artwork()
            {
                Title = "Favorite Artwork",
                Description = "This artwork will be added to favorites.",
                CreationDate = DateTime.Now,
                Medium = "Oil",
                ImageURL = "favorite_url",
                ArtistID = 1
            });

            Assert.IsTrue(addResult, "Artwork should be added before adding to favorites.");
        }

        // b. Test removing artwork from favorites.
        [Test]
        public void RemoveArtworkFromFavoriteTest()
        {
            virtualArtGalleryRepository.AddArtwork(new Artwork()
            {
                Title = "Artwork to Remove from Favorites",
                Description = "This artwork will be removed from favorites.",
                CreationDate = DateTime.Now,
                Medium = "Oil",
                ImageURL = "remove_favorite_url",
                ArtistID = 1
            });

            // Add it to favorites first
            virtualArtGalleryRepository.AddArtworkToFavorite(1, 1);

            // Now remove it from favorites
            var result = virtualArtGalleryRepository.RemoveArtworkFromFavorite(1, 1); // Assuming UserID = 1 and ArtworkID = 1 exist
            Assert.IsTrue(result, "Artwork should be successfully removed from favorites.");
        }



        // g. Test retrieving user's favorite artworks
        [Test]
        public void GetUserFavoriteArtworksTest()
        {
            // Add an artwork to favorites
            virtualArtGalleryRepository.AddArtwork(new Artwork()
            {
                Title = "User Favorite Artwork",
                Description = "This artwork is marked as favorite.",
                CreationDate = DateTime.Now,
                Medium = "Oil",
                ImageURL = "user_favorite_url",
                ArtistID = 1
            });

            // Add it to favorites
            virtualArtGalleryRepository.AddArtworkToFavorite(1, 1); // Assuming UserID = 1 and ArtworkID = 1 exist

            // Now retrieve the user's favorite artworks
            var favoriteArtworks = virtualArtGalleryRepository.GetUserFavoriteArtworks(1);

            // Assert that the result contains at least one favorite artwork
            Assert.IsNotNull(favoriteArtworks, "Favorite artworks list should not be null.");
            Assert.IsTrue(favoriteArtworks.Count > 0, "User should have at least one favorite artwork.");
        }
        [TearDown]
        public void TearDown()
        {
            
        }
    }
}
