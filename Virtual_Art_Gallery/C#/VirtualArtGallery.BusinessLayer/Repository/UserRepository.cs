using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;

namespace VirtualArtGallery.BusinessLayer.Repository
{
    internal class UserRepository : IUserRepository
    {
        // Default constructor
        public UserRepository() { }

        User user = new User();

        // Parametarized constructor 
        public UserRepository(int userId, string userName, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture)
        {
            user.UserId = userId;
            user.UserName = userName;
            user.Password = password;
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.DateOfBirth = dateOfBirth;
            user.ProfilePicture = profilePicture;
        }

        public override string ToString()
        {
            return $"User ID \t:\t{user.UserId}\nUsername \t:\t{user.UserName}\nEmail \t\t:\t{user.Email}\nFirst Name \t:\t{user.FirstName}\nLast Name \t:\t{user.LastName}\nDate Of Birth \t:\t{user.DateOfBirth}\nProfile Picture\t:\t{user.ProfilePicture}";
        }

    }
}
