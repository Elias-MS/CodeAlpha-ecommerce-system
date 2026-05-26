using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class ProductService
    {
        public static List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"SELECT p.*, c.CategoryName as CategoryName 
                                FROM Products p 
                                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                                WHERE p.IsActive = 1 
                                ORDER BY p.Name";
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(MapProductFromReader(reader));
                    }
                }
            }
            return products;
        }

        public static List<Product> GetFeaturedProducts()
        {
            var products = new List<Product>();
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_GetFeaturedProducts", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProductFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetFeaturedProducts: " + ex.Message);
            }
            return products;
        }

        public static List<Product> GetAllProductsAdmin()
        {
            var products = new List<Product>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"SELECT p.*, c.CategoryName as CategoryName 
                                FROM Products p 
                                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                                ORDER BY p.IsActive DESC, p.Name";
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(MapProductFromReader(reader));
                    }
                }
            }
            return products;
        }

        private static Product MapProductFromReader(SqlDataReader reader)
        {
            var p = new Product
            {
                ProductId = Convert.ToInt32(reader["ProductId"]),
                ProductCode = reader["ProductCode"]?.ToString(),
                Name = reader["Name"].ToString(),
                Description = reader["Description"]?.ToString(),
                Brand = reader["Brand"]?.ToString(),
                Model = reader["Model"]?.ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                MainImagePath = reader["MainImagePath"]?.ToString(),
                DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["DiscountPrice"]),
                DiscountExpiry = reader["DiscountExpiry"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["DiscountExpiry"]),
                IsFeatured = Convert.ToBoolean(reader["IsFeatured"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                Color = reader["Color"]?.ToString(),
                Size = reader["Size"]?.ToString()
            };

            // Resolve relative path to absolute path
            if (!string.IsNullOrEmpty(p.MainImagePath) && !System.IO.Path.IsPathRooted(p.MainImagePath))
            {
                p.MainImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p.MainImagePath.TrimStart('/', '\\'));
            }

            try {
                int ord = reader.GetOrdinal("CategoryName");
                p.CategoryName = reader[ord]?.ToString();
            } catch { }

            return p;
        }

        public static List<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"SELECT p.*, c.CategoryName 
                                FROM Products p 
                                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                                WHERE p.CategoryId = @categoryId AND p.IsActive = 1 
                                ORDER BY p.Name";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(MapProductFromReader(reader));
                        }
                    }
                }
            }
            return products;
        }

        public static Product GetProductById(int productId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"SELECT p.*, c.CategoryName 
                                FROM Products p 
                                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                                WHERE p.ProductId = @productId";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapProductFromReader(reader);
                    }
                }
            }
            return null;
        }

        public static bool AddProduct(Product product)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    string query = @"INSERT INTO Products (ProductCode, Name, Description, CategoryId, Brand, Model, Price, Stock, MainImagePath, DiscountPrice, DiscountExpiry, IsFeatured, IsActive) 
                                   VALUES (@code, @name, @desc, @catId, @brand, @model, @price, @stock, @img, @discount, @expiry, @isFeatured, @isActive)";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@code", product.ProductCode ?? Guid.NewGuid().ToString().Substring(0, 8));
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@desc", product.Description ?? "");
                        cmd.Parameters.AddWithValue("@catId", product.CategoryId);
                        cmd.Parameters.AddWithValue("@brand", product.Brand ?? "");
                        cmd.Parameters.AddWithValue("@model", product.Model ?? "");
                        cmd.Parameters.AddWithValue("@price", product.Price);
                        cmd.Parameters.AddWithValue("@stock", product.Stock);
                        cmd.Parameters.AddWithValue("@img", product.MainImagePath ?? "");
                        cmd.Parameters.AddWithValue("@discount", (object)product.DiscountPrice ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@expiry", (object)product.DiscountExpiry ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@isFeatured", product.IsFeatured);
                        cmd.Parameters.AddWithValue("@isActive", product.IsActive);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static bool UpdateProduct(Product product)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    string query = @"UPDATE Products 
                                   SET Name = @name, Description = @desc, CategoryId = @catId, 
                                       Brand = @brand, Model = @model, Price = @price, Stock = @stock, 
                                       MainImagePath = @img, DiscountPrice = @discount, DiscountExpiry = @expiry,
                                       IsFeatured = @isFeatured, IsActive = @isActive, UpdatedDate = GETDATE() 
                                   WHERE ProductId = @id";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@id", product.ProductId);
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@desc", product.Description ?? "");
                        cmd.Parameters.AddWithValue("@catId", product.CategoryId);
                        cmd.Parameters.AddWithValue("@brand", product.Brand ?? "");
                        cmd.Parameters.AddWithValue("@model", product.Model ?? "");
                        cmd.Parameters.AddWithValue("@price", product.Price);
                        cmd.Parameters.AddWithValue("@stock", product.Stock);
                        cmd.Parameters.AddWithValue("@img", product.MainImagePath ?? "");
                        cmd.Parameters.AddWithValue("@discount", (object)product.DiscountPrice ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@expiry", (object)product.DiscountExpiry ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@isFeatured", product.IsFeatured);
                        cmd.Parameters.AddWithValue("@isActive", product.IsActive);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static bool DeleteProduct(int productId)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    string query = "UPDATE Products SET IsActive = 0 WHERE ProductId = @id";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@id", productId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static bool PermanentDeleteProduct(int productId)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    // Try to delete. This will fail if there are foreign key constraints (e.g. in OrderItems)
                    string query = "DELETE FROM Products WHERE ProductId = @id";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@id", productId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static bool ActivateDeactivateProduct(int productId, bool activate)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    string query = "UPDATE Products SET IsActive = @active WHERE ProductId = @id";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.Parameters.AddWithValue("@active", activate);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static bool UpdateStock(int productId, int addedStock)
        {
            try {
                using (var connection = DatabaseHelper.GetConnection()) {
                    connection.Open();
                    string query = "UPDATE Products SET Stock = Stock + @added WHERE ProductId = @productId";
                    using (var cmd = new SqlCommand(query, connection)) {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@added", addedStock);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        public static List<Product> SearchProducts(string searchTerm)
        {
            var products = new List<Product>();
            using (var connection = DatabaseHelper.GetConnection()) {
                connection.Open();
                string query = @"SELECT p.*, c.CategoryName 
                                FROM Products p 
                                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                                WHERE p.IsActive = 1 AND 
                                (p.Name LIKE @term OR p.ProductCode LIKE @term OR p.Brand LIKE @term OR p.Model LIKE @term)";
                using (var cmd = new SqlCommand(query, connection)) {
                    cmd.Parameters.AddWithValue("@term", "%" + searchTerm + "%");
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) products.Add(MapProductFromReader(reader));
                    }
                }
            }
            return products;
        }
    }
}