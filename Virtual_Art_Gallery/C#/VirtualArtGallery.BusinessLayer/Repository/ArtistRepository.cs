using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class ArtistRepository : IArtistRepository
    {
        //Default Constructor
        public ArtistRepository() { }

        Artist artist = new Artist();

        //Parameterised Constructor
        public ArtistRepository(int artistID, string name, string biography, DateTime birthDate, string nationality, string website, string contactInformation)
        {
            artist.ArtistID = artistID;
            artist.Name = name;
            artist.Biography = biography;
            artist.BirthDate = birthDate;
            artist.Nationality = nationality;
            artist.Website = website;
            artist.ContactInformation = contactInformation;
        }
        public override string ToString()
        {
            return $"Artist ID \t:\t{artist.ArtistID}\nName \t\t:\t{artist.Name}\nBiography \t:\t{artist.Biography}\nBirth Date \t:\t{artist.BirthDate}\nNationality \t:\t{artist.Nationality}\nWebsite \t:\t{artist.Website}\nContact \t:\t{artist.ContactInformation}";
        }
    }
}
