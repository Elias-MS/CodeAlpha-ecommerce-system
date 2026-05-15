using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class CategoryService
    {
        public static List<Category> GetAllCategories(bool includeAllOption = false)
        {
            var categories = new List<Category>();
            
            if (includeAllOption)
            {
                categories.Add(new Category { CategoryId = 0, CategoryName = "All Categories", IsActive = true });
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetAllCategories", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(MapCategoryFromReader(reader));
                    }
                }
            }
            
            return categories;
        }

        public static List<Category> GetMainCategories()
        {
            var categories = new List<Category>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetMainCategories", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(MapCategoryFromReader(reader));
                    }
                }
            }
            
            return categories;
        }

        public static List<Category> GetSubCategories(int parentCategoryId)
        {
            var categories = new List<Category>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetSubCategories", connection))
                {
                    cmd.Parameters.AddWithValue("@parentCategoryId", parentCategoryId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(MapCategoryFromReader(reader));
                        }
                    }
                }
            }
            
            return categories;
        }

        public static Category GetCategoryById(int categoryId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetCategoryById", connection))
                {
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCategoryFromReader(reader);
                        }
                    }
                }
            }
            
            return null;
        }

        public static Category GetCategoryByName(string categoryName)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Categories WHERE CategoryName = @name";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", categoryName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapCategoryFromReader(reader);
                    }
                }
            }
            return null;
        }

        private static Category MapCategoryFromReader(SqlDataReader reader)
        {
            return new Category
            {
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                CategoryName = reader["CategoryName"].ToString(),
                Description = reader["Description"]?.ToString(),
                ParentCategoryId = reader["ParentCategoryId"] as int?,
                ImagePath = reader["ImagePath"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                SortOrder = Convert.ToInt32(reader["SortOrder"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
            };
        }

        public static bool AddCategory(Category category)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    
                    using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_AddCategory", connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                        cmd.Parameters.AddWithValue("@description", category.Description ?? "");
                        cmd.Parameters.AddWithValue("@parentCategoryId", (object)category.ParentCategoryId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@imagePath", category.ImagePath ?? "");
                        cmd.Parameters.AddWithValue("@sortOrder", category.SortOrder);
                        
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UpdateCategory(Category category)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    
                    using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_UpdateCategory", connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryId", category.CategoryId);
                        cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                        cmd.Parameters.AddWithValue("@description", category.Description ?? "");
                        cmd.Parameters.AddWithValue("@parentCategoryId", (object)category.ParentCategoryId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@imagePath", category.ImagePath ?? "");
                        cmd.Parameters.AddWithValue("@sortOrder", category.SortOrder);
                        
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteCategory(int categoryId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    
                    using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_DeleteCategory", connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryId", categoryId);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCategoryNameExists(string categoryName, int? excludeCategoryId = null)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_IsCategoryNameExists", connection))
                {
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.Parameters.AddWithValue("@excludeCategoryId", (object)excludeCategoryId ?? DBNull.Value);
                    
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static List<Category> GetCategoriesWithProductCount()
        {
            var categories = new List<Category>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetCategoriesWithProductCount", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = MapCategoryFromReader(reader);
                        categories.Add(category);
                    }
                }
            }
            
            return categories;
        }

        public static bool UpdateCategorySortOrder(int categoryId, int sortOrder)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    
                    using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_UpdateCategorySortOrder", connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryId", categoryId);
                        cmd.Parameters.AddWithValue("@sortOrder", sortOrder);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<CategoryGroup> GetGroupedCategories()
        {
            var groups = new List<CategoryGroup>();
            var allCategories = new List<Category>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var cmd = DatabaseHelper.CreateStoredProcedureCommand("sp_GetCategoryHierarchy", connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cat = MapCategoryFromReader(reader);
                        allCategories.Add(cat);
                    }
                }
            }

            // Organize into hierarchy
            var mainCategories = allCategories.FindAll(c => c.ParentCategoryId == null);
            foreach (var main in mainCategories)
            {
                var group = new CategoryGroup
                {
                    MainCategory = main,
                    SubCategories = allCategories.FindAll(c => c.ParentCategoryId == main.CategoryId)
                };
                groups.Add(group);
            }

            return groups;
        }
    }

    public class CategoryGroup
    {
        public Category MainCategory { get; set; }
        public List<Category> SubCategories { get; set; } = new List<Category>();
    }
}