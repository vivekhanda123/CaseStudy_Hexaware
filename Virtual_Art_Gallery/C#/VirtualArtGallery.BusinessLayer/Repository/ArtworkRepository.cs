using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class ArtworkRepository : IArtworkRepository
    {
        // Default Constructor
        public ArtworkRepository() { }

        Artwork artwork = new Artwork();  

        //Parametarized constructor

        public ArtworkRepository(string title, string description, DateTime creationDate, string medium, string imageURL, int artistID, [Optional] int artworkID)
        {
            artwork.ArtworkID = artworkID;
            artwork.Title = title;
            artwork.Description = description;
            artwork.CreationDate = creationDate;
            artwork.Medium = medium;
            artwork.ImageURL = imageURL;
            artwork.ArtistID = artistID;
        }

        // ToString method

        public override string ToString()
        {
            return $"Artwork ID \t:\t{artwork.ArtworkID}\nTitle \t\t:\t{artwork.Title}\nDescription \t:\t{artwork.Description}\nCreation Date \t:\t{artwork.CreationDate}\nMedium \t\t:\t{artwork.Medium}\nImage URL \t:\t{artwork.ImageURL}\nArtist ID \t:\t{artwork.ArtistID}";
        }
        
    }
}
