using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Entity;
using VirtualArtGallery.Util;


namespace VirtualArtGallery.BusinessLayer.Repository
{
    public class VirtualArtGalleryRepository : IVirtualArtGalleryRepository
    {
        // Add data inside Artwork
        public bool AddArtwork(Artwork artwork)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL) VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artwork: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Update the data of Artwork
        public bool UpdateArtwork(Artwork artwork)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL WHERE ArtworkID = @ArtworkID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating artwork: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Remove Artwork
        public bool RemoveArtwork(int artworkID)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = "DELETE FROM Artwork WHERE ArtworkID = @ArtworkID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing artwork: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Get Artwork By Id
        public Artwork GetArtworkById(int artworkID)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = (string)reader["Title"],
                                Description = (string)reader["Description"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = (string)reader["Medium"],
                                ImageURL = (string)reader["ImageURL"],
                                ArtistID = (int)reader["ArtistID"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting artwork by ID: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return null; // Return null if not found
        }

        // Search Artworks by keywords or description
        public List<Artwork> SearchArtworks(string keyword)
        {
            var artworks = new List<Artwork>();
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artworks.Add(new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = (string)reader["Title"],
                                Description = (string)reader["Description"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = (string)reader["Medium"],
                                ImageURL = (string)reader["ImageURL"],
                                ArtistID = (int)reader["ArtistID"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching artworks: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return artworks;
        }

        // User Favorites methods
        public bool AddArtworkToFavorite(int userID, int artworkID)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            // Assuming you have a UserFavorites table to handle user favorites
            try
            {
                string query = "INSERT INTO User_Favorite_Artwork (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artwork to favorites: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Remove Artwork from favorite
        public bool RemoveArtworkFromFavorite(int userID, int artworkID)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            // Assuming you have a UserFavorites table to handle user favorites
            try
            {
                string query = "DELETE FROM User_Favorite_Artwork WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing artwork from favorites: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Get the list of User Favorite Artworks 
        public List<Artwork> GetUserFavoriteArtworks(int userID)
        {
            var favoriteArtworks = new List<Artwork>();
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                //SqlConnection conn = DBConnection.getDBConnection();
                string query = "SELECT a.* FROM Artwork a INNER JOIN User_Favorite_Artwork uf ON a.ArtworkID = uf.ArtworkID WHERE uf.UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            favoriteArtworks.Add(new Artwork
                            {
                                ArtworkID = (int)reader["ArtworkID"],
                                Title = (string)reader["Title"],
                                Description = (string)reader["Description"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                Medium = (string)reader["Medium"],
                                ImageURL = (string)reader["ImageURL"],
                                ArtistID = (int)reader["ArtistID"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user's favorite artworks: {ex.Message}");
            }
            finally
            {

                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return favoriteArtworks;
        }
    }
}
