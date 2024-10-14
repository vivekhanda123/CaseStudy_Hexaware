using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VirtualArtGallery.BusinessLayer.Repository;
using VirtualArtGallery.BusinessLayer.Service;
using VirtualArtGallery.Entity;
using VirtualArtGallery.BusinessLayer.Exceptions;

namespace VirtualArtGallery.UnitTesting
{
    [TestFixture]
    public class ArtworkManagement 
    {
        VirtualArtGalleryRepository virtualArtGalleryRepository;

        [SetUp]
        public void initialiazation()
        {
            virtualArtGalleryRepository = new VirtualArtGalleryRepository();
        }

        // a. Test the ability to upload a new artwork to the gallery.
        [Test]
        public void UploadArtworkTest()
        {
            var result = virtualArtGalleryRepository.AddArtwork(new Artwork()
            {
                Title = "Test Artwork",
                Description = "This is a test artwork description.",
                CreationDate = DateTime.Now,
                Medium = "Oil on Canvas",
                ImageURL = "test_image_url",
                ArtistID = 1
            });
            Assert.IsTrue(result);
            Assert.That(result, Is.Not.Null);

        }

        // b. Verify that updating artwork details works correctly.
        [Test]
        public void UpdateArtworkTest()
        {
            var result = virtualArtGalleryRepository.UpdateArtwork(new Artwork()
            {
                ArtworkID = 1,
                Title = "Initial Title",
                Description = "Initial Description",
                CreationDate = DateTime.Now,
                Medium = "Oil",
                ImageURL = "initial_url",
                ArtistID = 1
            });
            Assert.That(result, Is.True);
        }

        // c. Test removing an artwork from the gallery.  Not working 
        [Test]
        public void RemoveArtworkTest()
        {
            var result = virtualArtGalleryRepository.RemoveArtwork(2);  // Assuming artworkID = 1 exists
            Assert.IsTrue(result, "Artwork should be successfully removed.");
        }

        // d. Check if searching for artworks returns the expected results.
        [Test]
        public void SearchArtworkTest()
        {
            var searchResults = virtualArtGalleryRepository.SearchArtworks("Night");
            Assert.IsNotNull(searchResults, "Search results should not be null.");
            Assert.IsTrue(searchResults.Count > 0, "At least one artwork should match the search query.");
            Assert.IsTrue(searchResults[0].Title.Contains("Night"), "Search result title should contain 'Night'.");
        }

        [TearDown]
        public void TearDown()
        {

        }
    }  
}



