using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VirtualArtGallery.BusinessLayer.Repository;
using VirtualArtGallery.BusinessLayer.Service;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.UnitTesting
{
    [TestFixture]
    public class ArtworkManagement
    {
        // a. Test the ability to upload a new artwork to the gallery.
        [Test]
        public void UploadArtworkTest()
        {
            Artwork newArtwork = new Artwork() { Id = 1, Title = "Starry Night", Artist = "Vincent van Gogh" };
            ArtworkManager artworkManager = new ArtworkManager();

            string result = artworkManager.UploadArtwork(newArtwork);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("successfully uploaded"));
            Assert.That(result, Does.StartWith("Artwork"));
            Assert.That(result, Does.EndWith("successfully uploaded."));
        }

        // b. Verify that updating artwork details works correctly.
        [Test]
        public void UpdateArtworkTest()
        {
            Artwork updatedArtwork = new Artwork() { Id = 1, Title = "The Starry Night", Artist = "Vincent van Gogh" };
            ArtworkManager artworkManager = new ArtworkManager();

            bool result = artworkManager.UpdateArtworkDetails(updatedArtwork);

            Assert.That(result, Is.True);
        }

        // c. Test removing an artwork from the gallery.
        [Test]
        public void RemoveArtworkTest()
        {
            int artworkId = 1;
            ArtworkManager artworkManager = new ArtworkManager();

            bool result = artworkManager.RemoveArtwork(artworkId);

            Assert.That(result, Is.True);
        }

        // d. Check if searching for artworks returns the expected results.
        [Test]
        public void SearchArtworkTest()
        {
            string searchQuery = "Night";
            ArtworkManager artworkManager = new ArtworkManager();

            List<Artwork> searchResults = artworkManager.SearchArtworks(searchQuery);

            Assert.That(searchResults, Is.Not.Null);
            //Assert.That(searchResults.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(searchResults[0].Title, Does.Contain("Starry"));
        }
    }

    // Assuming you have a simple model for Artwork and a manager for Artwork-related operations
    public class Artwork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }

    public class ArtworkManager
    {
        private List<Artwork> artworkGallery = new List<Artwork>();

        public string UploadArtwork(Artwork artwork)
        {
            artworkGallery.Add(artwork);
            return "Artwork successfully uploaded.";
        }

        public bool UpdateArtworkDetails(Artwork artwork)
        {
            var existingArtwork = artworkGallery.Find(a => a.Id == artwork.Id);
            if (existingArtwork != null)
            {
                existingArtwork.Title = artwork.Title;
                existingArtwork.Artist = artwork.Artist;
                return true;
            }
            return false;
        }

        public bool RemoveArtwork(int artworkId)
        {
            Console.WriteLine(artworkId);
            var artwork = artworkGallery.Find(a => a.Id == artworkId);
            if (artwork != null)
            {
                artworkGallery.Remove(artwork);
                return true;
            }
            return false;
        }

        public List<Artwork> SearchArtworks(string query)
        {
            return artworkGallery.FindAll(a => a.Title.Contains(query));
        }
    }
}

