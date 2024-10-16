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
                string query = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL) VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    //cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

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
                                ArtworkID = reader["ArtworkID"] != DBNull.Value ? (int)reader["ArtworkID"] : 0,
                                Title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : string.Empty,
                                Description = reader["Description"] != DBNull.Value ? (string)reader["Description"] : string.Empty,
                                CreationDate = reader["CreationDate"] != DBNull.Value ? (DateTime)reader["CreationDate"] : DateTime.MinValue,
                                Medium = reader["Medium"] != DBNull.Value ? (string)reader["Medium"] : string.Empty,
                                ImageURL = reader["ImageURL"] != DBNull.Value ? (string)reader["ImageURL"] : string.Empty,
                                ArtistID = reader["ArtistID"] != DBNull.Value ? (int)reader["ArtistID"] : 0
                            };
                        }
                        else
                        {
                            Console.WriteLine($"Artwork with ID {artworkID} not found.");
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur
                Console.WriteLine($"Error getting artwork by ID: {ex.Message}");
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
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
                            var artwork = new Artwork();

                            // Use safe casting to handle potential nulls or invalid types
                            artwork.ArtworkID = reader["ArtworkID"] != DBNull.Value ? Convert.ToInt32(reader["ArtworkID"]) : 0;
                            artwork.Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : string.Empty;
                            artwork.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
                            artwork.CreationDate = reader["CreationDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreationDate"]) : DateTime.MinValue;
                            artwork.Medium = reader["Medium"] != DBNull.Value ? reader["Medium"].ToString() : string.Empty;
                            artwork.ImageURL = reader["ImageURL"] != DBNull.Value ? reader["ImageURL"].ToString() : string.Empty;
                            artwork.ArtistID = reader["ArtistID"] != DBNull.Value ? Convert.ToInt32(reader["ArtistID"]) : 0;

                            artworks.Add(artwork);
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
