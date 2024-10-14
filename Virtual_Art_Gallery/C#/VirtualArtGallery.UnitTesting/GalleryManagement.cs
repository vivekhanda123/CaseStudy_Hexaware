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
    internal class GalleryManagement
    {
        [TestFixture]
        public class GalleryManagementTest
        {
            // a. Test creating a new gallery.
            [Test]
            public void CreateGalleryTest()
            {
                Gallery newGallery = new Gallery() { Id = 1, Name = "Modern Art Gallery" };
                GalleryManager galleryManager = new GalleryManager();

                string result = galleryManager.CreateGallery(newGallery);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("successfully created"));
                Assert.That(result, Does.StartWith("Gallery"));
                Assert.That(result, Does.EndWith("successfully created."));
            }

            // b. Verify that updating gallery information works correctly.
            [Test]
            public void UpdateGalleryTest()
            {
                Gallery updatedGallery = new Gallery() { Id = 1, Name = "Contemporary Art Gallery" };
                GalleryManager galleryManager = new GalleryManager();

                bool result = galleryManager.UpdateGalleryDetails(updatedGallery);

                Assert.That(result, Is.True);
            }

            // c. Test removing a gallery from the system.
            [Test]
            public void RemoveGalleryTest()
            {
                int galleryId = 1;
                GalleryManager galleryManager = new GalleryManager();

                bool result = galleryManager.RemoveGallery(galleryId);

                Assert.That(result, Is.True);
            }

            // d. Check if searching for galleries returns the expected results.
            [Test]
            public void SearchGalleryTest()
            {
                string searchQuery = "Modern Art";
                GalleryManager galleryManager = new GalleryManager();

                List<Gallery> searchResults = galleryManager.SearchGalleries(searchQuery);

                Assert.That(searchResults, Is.Not.Null);
                Assert.That(searchResults.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(searchResults[0].Name, Does.Contain("Modern"));
            }
        }

        // Assuming you have a simple model for Gallery and a manager for Gallery-related operations
        public class Gallery
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class GalleryManager
        {
            private List<Gallery> galleryCollection = new List<Gallery>();

            public string CreateGallery(Gallery gallery)
            {
                galleryCollection.Add(gallery);
                return "Gallery successfully created.";
            }

            public bool UpdateGalleryDetails(Gallery gallery)
            {
                var existingGallery = galleryCollection.Find(g => g.Id == gallery.Id);
                if (existingGallery != null)
                {
                    existingGallery.Name = gallery.Name;
                    return true;
                }
                return false;
            }

            public bool RemoveGallery(int galleryId)
            {
                var gallery = galleryCollection.Find(g => g.Id == galleryId);
                if (gallery != null)
                {
                    galleryCollection.Remove(gallery);
                    return true;
                }
                return false;
            }

            public List<Gallery> SearchGalleries(string query)
            {
                return galleryCollection.FindAll(g => g.Name.Contains(query));
            }
        }
    }
}
