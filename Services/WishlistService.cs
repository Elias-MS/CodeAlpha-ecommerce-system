using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class WishlistService
    {
        public static List<Wishlist> GetUserWishlist(int userId)
        {
            var wishlist = new List<Wishlist>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT w.*, p.Name as ProductName, p.Price, p.Stock, p.MainImagePath 
                    FROM Wishlist w
                    JOIN Products p ON w.ProductId = p.ProductId
                    WHERE w.UserId = @userId
                    ORDER BY w.AddedDate DESC";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new Wishlist
                            {
                                WishlistId = Convert.ToInt32(reader["WishlistId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                AddedDate = Convert.ToDateTime(reader["AddedDate"]),
                                Product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Name = reader["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    MainImagePath = reader["MainImagePath"]?.ToString()
                                }
                            };
                            wishlist.Add(item);
                        }
                    }
                }
            }
            
            return wishlist;
        }

        public static bool AddToWishlist(int userId, int productId)
        {
            if (IsInWishlist(userId, productId)) return true;

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Wishlist (UserId, ProductId, AddedDate) VALUES (@userId, @productId, GETDATE())";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        public static bool RemoveFromWishlist(int wishlistId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Wishlist WHERE WishlistId = @id";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", wishlistId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        public static bool IsInWishlist(int userId, int productId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Wishlist WHERE UserId = @userId AND ProductId = @productId";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
