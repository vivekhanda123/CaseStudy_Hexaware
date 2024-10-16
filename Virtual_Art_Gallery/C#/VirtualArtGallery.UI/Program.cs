using System;
using System.Collections.Generic;
using VirtualArtGallery.Entity;
using VirtualArtGallery.BusinessLayer.Repository;
using VirtualArtGallery.BusinessLayer.Service;
using VirtualArtGallery.BusinessLayer.Exceptions;

namespace VirtualArtGallery.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IVirtualArtGalleryRepository galleryRepository = new VirtualArtGalleryRepository();
            IVirtualArtGalleryImpl galleryService = new VirtualArtGalleryImpl(galleryRepository);

            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the Virtual Art Gallery Management System");
                    Console.WriteLine("Select an operation:");
                    Console.WriteLine("1. Add Artwork");
                    Console.WriteLine("2. Update Artwork");
                    Console.WriteLine("3. Remove Artwork");
                    Console.WriteLine("4. Add Artwork to User Favorites");
                    Console.WriteLine("5. Remove Artwork from Favorites");
                    Console.WriteLine("6. List User Favorite Artworks");
                    Console.WriteLine("7. Search Artworks");
                    Console.WriteLine("8. Get Artwork by ID");
                    Console.WriteLine("9. Exit");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddArtwork(galleryService);
                            break;

                        case "2":
                            UpdateArtwork(galleryService);
                            break;

                        case "3":
                            RemoveArtwork(galleryService);
                            break;

                        case "4":
                            AddArtworkToFavorites(galleryService);
                            break;

                        case "5":
                            RemoveArtworkFromFavorites(galleryService);
                            break;

                        case "6":
                            ListUserFavoriteArtworks(galleryService);
                            break;

                        case "7":
                            SearchArtworks(galleryService);
                            break;

                        case "8":
                            GetArtworkById(galleryService);
                            break;

                        case "9":
                            exit = true;
                            Console.WriteLine("Exiting the application. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;
                    }
                }
                catch (ArtWorkNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (UserNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Add Artwork
        private static void AddArtwork(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Add Artwork ---");
                Console.Write("Enter Artwork Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter Creation Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime creationDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                Console.Write("Enter Medium (e.g., Oil on Canvas): ");
                string medium = Console.ReadLine();

                Console.Write("Enter Image URL: ");
                string imageUrl = Console.ReadLine();

              

                Artwork artwork = new Artwork
                {
                    Title = title,
                    Description = description,
                    CreationDate = creationDate,
                    Medium = medium,
                    ImageURL = imageUrl
                };

                bool result = galleryService.AddArtwork(artwork);
                Console.WriteLine(result ? "Artwork added successfully!" : "Failed to add artwork.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Update Artwork
        private static void UpdateArtwork(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Update Artwork ---");
                Console.Write("Enter Artwork ID: ");
                if (!int.TryParse(Console.ReadLine(), out int artworkId))
                {
                    Console.WriteLine("Invalid Artwork ID.");
                    return;
                }

                Console.Write("Enter New Artwork Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter New Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter New Creation Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime creationDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                Console.Write("Enter New Medium: ");
                string medium = Console.ReadLine();

                Console.Write("Enter New Image URL: ");
                string imageUrl = Console.ReadLine();

                Artwork artwork = new Artwork
                {
                    ArtworkID = artworkId,
                    Title = title,
                    Description = description,
                    CreationDate = creationDate,
                    Medium = medium,
                    ImageURL = imageUrl
                };

                bool result = galleryService.UpdateArtwork(artwork);
                Console.WriteLine(result ? "Artwork updated successfully!" : "Failed to update artwork.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Remove Artwork
        private static void RemoveArtwork(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Remove Artwork ---");
                Console.Write("Enter Artwork ID: ");
                if (!int.TryParse(Console.ReadLine(), out int artworkId))
                {
                    Console.WriteLine("Invalid Artwork ID.");
                    return;
                }

                bool result = galleryService.RemoveArtwork(artworkId);
                Console.WriteLine(result ? "Artwork removed successfully!" : "Failed to remove artwork.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Add Artwork to favorites 
        private static void AddArtworkToFavorites(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Add Artwork to User Favorites ---");
                Console.Write("Enter User ID: ");
                if (!int.TryParse(Console.ReadLine(), out int userId))
                {
                    Console.WriteLine("Invalid User ID.");
                    return;
                }

                Console.Write("Enter Artwork ID: ");
                if (!int.TryParse(Console.ReadLine(), out int artworkId))
                {
                    Console.WriteLine("Invalid Artwork ID.");
                    return;
                }

                bool result = galleryService.AddArtworkToFavorite(userId, artworkId);
                Console.WriteLine(result ? "Artwork added to favorites!" : "Failed to add artwork to favorites.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Remove artwork from favorites
        private static void RemoveArtworkFromFavorites(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Remove Artwork from Favorites ---");
                Console.Write("Enter User ID: ");
                if (!int.TryParse(Console.ReadLine(), out int userId))
                {
                    Console.WriteLine("Invalid User ID.");
                    return;
                }

                Console.Write("Enter Artwork ID: ");
                if (!int.TryParse(Console.ReadLine(), out int artworkId))
                {
                    Console.WriteLine("Invalid Artwork ID.");
                    return;
                }

                bool result = galleryService.RemoveArtworkFromFavorite(userId, artworkId);
                Console.WriteLine(result ? "Artwork removed from favorites!" : "Failed to remove artwork from favorites.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // List user favorite artworks 
        private static void ListUserFavoriteArtworks(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- List User Favorite Artworks ---");
                Console.Write("Enter User ID: ");
                if (!int.TryParse(Console.ReadLine(), out int userId))
                {
                    Console.WriteLine("Invalid User ID.");
                    return;
                }

                var favoriteArtworks = galleryService.GetUserFavoriteArtworks(userId);
                if (favoriteArtworks.Count > 0)
                {
                    Console.WriteLine("User's Favorite Artworks:");
                    foreach (var artwork in favoriteArtworks)
                    {
                        Console.WriteLine($"Artwork ID: {artwork.ArtworkID}, Title: {artwork.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("No favorite artworks found for this user.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Search Artworks 
        private static void SearchArtworks(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Search Artworks ---");
                Console.Write("Enter keyword: ");
                string keyword = Console.ReadLine();

                var artworks = galleryService.SearchArtworks(keyword);
                if (artworks.Count > 0)
                {
                    Console.WriteLine("Search Results:");
                    foreach (var artwork in artworks)
                    {
                        Console.WriteLine($"Artwork ID: {artwork.ArtworkID}, Title: {artwork.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("No artworks found for the given keyword.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Get artwork by ID
        private static void GetArtworkById(IVirtualArtGalleryImpl galleryService)
        {
            try
            {
                Console.WriteLine("\n--- Get Artwork by ID ---");
                Console.Write("Enter Artwork ID: ");

                // Use TryParse to safely convert the input to an integer
                if (!int.TryParse(Console.ReadLine(), out int artworkId))
                {
                    Console.WriteLine("Invalid Artwork ID. Please enter a valid integer.");
                    return;
                }

                // Call the method to get artwork by ID
                Artwork artwork = galleryService.GetArtworkById(artworkId);

                // Check if the artwork is found
                if (artwork != null)
                {
                    Console.WriteLine($"Artwork ID: {artwork.ArtworkID}");
                    Console.WriteLine($"Title: {artwork.Title}");
                    Console.WriteLine($"Description: {artwork.Description}");
                    Console.WriteLine($"Creation Date: {artwork.CreationDate}");
                    Console.WriteLine($"Medium: {artwork.Medium}");
                    Console.WriteLine($"Image URL: {artwork.ImageURL}");
                }
                else
                {
                    Console.WriteLine("Artwork not found.");
                }
            }
            catch (Exception ex)
            {
                // Display any errors encountered during the process
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
