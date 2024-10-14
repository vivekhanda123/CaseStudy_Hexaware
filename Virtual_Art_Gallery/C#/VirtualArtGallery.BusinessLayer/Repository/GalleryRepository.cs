using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class GalleryRepository : IGalleryRepository
    {
        // Default Constructor
        public GalleryRepository() { }

        Gallery gallery = new Gallery();

        // Parametarized Constructor
        public GalleryRepository(string name, string description,
                       string location, int curatorID, string openingHours, [Optional] int galleryId)
        {
            gallery.GalleryId = galleryId;
            gallery.Name = name;
            gallery.Description = description;
            gallery.Location = location;
            gallery.CuratorID = curatorID;
            gallery.OpeningHours = openingHours;
        }

        public override string ToString()
        {
            return $"Gallery ID \t:\t{gallery.GalleryId}\nName \t\t:\t{gallery.Name}\nDescription \t:\t{gallery.Description}\nLocation \t:\t{gallery.Location}\nCurator ID \t:\t{gallery.CuratorID}\nOpening Hours \t:\t{gallery.OpeningHours}";
        }
    }
}
