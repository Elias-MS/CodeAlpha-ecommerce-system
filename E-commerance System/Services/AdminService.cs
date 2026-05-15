using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;
using E_commerance_System.Utils;

namespace E_commerance_System.Services
{
    public class AdminService
    {
        public static Admin AuthenticateAdmin(string username, string password)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Admins WHERE Username = @username AND IsActive = 1";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string storedSalt = reader["Salt"].ToString();
                            
                            // For initial setup, allow plain text password "admin123"
                            bool isValidPassword = false;
                            if (storedHash == "admin123" && password == "admin123")
                            {
                                isValidPassword = true;
                            }
                            else
                            {
                                isValidPassword = SecurityHelper.VerifyPassword(password, storedHash, storedSalt);
                            }
                            
                            if (isValidPassword)
                            {
                                var admin = new Admin
                                {
                                    AdminId = Convert.ToInt32(reader["AdminId"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    Permissions = reader["Permissions"]?.ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                };

                                // Update last login date
                                UpdateLastLoginDate(admin.AdminId);
                                
                                return admin;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static bool CreateAdmin(Admin admin, string password, int createdBy)
        {
            try
            {
                // Validate password strength
                var passwordValidation = SecurityHelper.ValidatePasswordStrength(password);
                if (!passwordValidation.IsValid)
                {
                    throw new ArgumentException($"Password validation failed: {string.Join(", ", passwordValidation.Messages)}");
                }

                // Hash password
                var hashResult = SecurityHelper.HashPassword(password);
                string hash = hashResult.Item1;
                string salt = hashResult.Item2;

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Admins (Username, PasswordHash, Salt, Email, FullName, Role, Permissions, CreatedBy) 
                        VALUES (@username, @passwordHash, @salt, @email, @fullName, @role, @permissions, @createdBy)";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", admin.Username);
                        cmd.Parameters.AddWithValue("@passwordHash", hash);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@email", admin.Email);
                        cmd.Parameters.AddWithValue("@fullName", admin.FullName);
                        cmd.Parameters.AddWithValue("@role", admin.Role);
                        cmd.Parameters.AddWithValue("@permissions", admin.Permissions ?? "");
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);
                        
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

        public static bool UpdateAdmin(Admin admin)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Admins 
                        SET Email = @email, FullName = @fullName, Role = @role, Permissions = @permissions
                        WHERE AdminId = @adminId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@adminId", admin.AdminId);
                        cmd.Parameters.AddWithValue("@email", admin.Email);
                        cmd.Parameters.AddWithValue("@fullName", admin.FullName);
                        cmd.Parameters.AddWithValue("@role", admin.Role);
                        cmd.Parameters.AddWithValue("@permissions", admin.Permissions ?? "");
                        
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

        public static bool ChangeAdminPassword(int adminId, string currentPassword, string newPassword)
        {
            try
            {
                // First verify current password
                var admin = GetAdminById(adminId);
                if (admin == null)
                {
                    return false;
                }

                // For initial setup, allow changing from plain text password
                bool isCurrentPasswordValid = false;
                if (admin.PasswordHash == "admin123" && currentPassword == "admin123")
                {
                    isCurrentPasswordValid = true;
                }
                else
                {
                    isCurrentPasswordValid = SecurityHelper.VerifyPassword(currentPassword, admin.PasswordHash, admin.Salt);
                }

                if (!isCurrentPasswordValid)
                {
                    return false;
                }

                // Validate new password
                var passwordValidation = SecurityHelper.ValidatePasswordStrength(newPassword);
                if (!passwordValidation.IsValid)
                {
                    return false;
                }

                // Hash new password
                var hashResult = SecurityHelper.HashPassword(newPassword);
                string hash = hashResult.Item1;
                string salt = hashResult.Item2;

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Admins 
                        SET PasswordHash = @passwordHash, Salt = @salt
                        WHERE AdminId = @adminId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@adminId", adminId);
                        cmd.Parameters.AddWithValue("@passwordHash", hash);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        
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

        public static Admin GetAdminById(int adminId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Admins WHERE AdminId = @adminId";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@adminId", adminId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Admin
                            {
                                AdminId = Convert.ToInt32(reader["AdminId"]),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                Email = reader["Email"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Role = reader["Role"].ToString(),
                                Permissions = reader["Permissions"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                LastLoginDate = reader["LastLoginDate"] as DateTime?,
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                CreatedBy = reader["CreatedBy"] as int?
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<Admin> GetAllAdmins()
        {
            var admins = new List<Admin>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Admins ORDER BY CreatedDate DESC";
                
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new Admin
                        {
                            AdminId = Convert.ToInt32(reader["AdminId"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Role = reader["Role"].ToString(),
                            Permissions = reader["Permissions"]?.ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            LastLoginDate = reader["LastLoginDate"] as DateTime?,
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
            }
            
            return admins;
        }

        public static bool ActivateDeactivateAdmin(int adminId, bool isActive)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Admins SET IsActive = @isActive WHERE AdminId = @adminId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@adminId", adminId);
                        cmd.Parameters.AddWithValue("@isActive", isActive);
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

        public static bool IsUsernameExists(string username)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Admins WHERE Username = @username";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool IsEmailExists(string email)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Admins WHERE Email = @email";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static DashboardStats GetDashboardStatistics()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var cmd = new SqlCommand("GetDashboardStats", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DashboardStats
                            {
                                TotalCustomers = Convert.ToInt32(reader["TotalCustomers"]),
                                TotalProducts = Convert.ToInt32(reader["TotalProducts"]),
                                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                TodayOrders = Convert.ToInt32(reader["TodayOrders"]),
                                TodayRevenue = Convert.ToDecimal(reader["TodayRevenue"]),
                                LowStockProducts = Convert.ToInt32(reader["LowStockProducts"]),
                                PendingReviews = Convert.ToInt32(reader["PendingReviews"])
                            };
                        }
                    }
                }
            }
            return new DashboardStats();
        }

        private static void UpdateLastLoginDate(int adminId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Admins SET LastLoginDate = GETDATE() WHERE AdminId = @adminId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@adminId", adminId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                // Log error but don't fail authentication
            }
        }
    }

    public class DashboardStats
    {
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TodayOrders { get; set; }
        public decimal TodayRevenue { get; set; }
        public int LowStockProducts { get; set; }
        public int PendingReviews { get; set; }
    }
}