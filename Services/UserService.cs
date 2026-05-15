using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using E_commerance_System.Data;
using E_commerance_System.Models;
using E_commerance_System.Utils;

namespace E_commerance_System.Services
{
    public class UserService
    {
        public static User AuthenticateUser(string username, string password)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @username";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["PasswordHash"].ToString();
                            string storedSalt = reader["Salt"].ToString();
                            bool isActive = Convert.ToBoolean(reader["IsActive"]);
                            
                            // Verify password
                            if (SecurityHelper.VerifyPassword(password, storedHash, storedSalt))
                            {
                                var user = new User
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Phone = reader["Phone"]?.ToString(),
                                    Address = reader["Address"]?.ToString(),
                                    City = reader["City"]?.ToString(),
                                    State = reader["State"]?.ToString(),
                                    ZipCode = reader["ZipCode"]?.ToString(),
                                    Country = reader["Country"]?.ToString(),
                                    IsActive = isActive,
                                    IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    PreferredCurrency = reader["PreferredCurrency"]?.ToString() ?? "ETB"
                                };

                                // Update last login date
                                if (isActive) UpdateLastLoginDate(user.UserId);
                                
                                return user;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static bool RegisterUser(User user, string password)
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
                        INSERT INTO Users (Username, PasswordHash, Salt, Email, FirstName, LastName, Phone, Address, City, State, ZipCode, Country, DateOfBirth, Gender, PreferredCurrency) 
                        VALUES (@username, @passwordHash, @salt, @email, @firstName, @lastName, @phone, @address, @city, @state, @zipCode, @country, @dateOfBirth, @gender, @currency)";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@passwordHash", hash);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@phone", user.Phone ?? "");
                        cmd.Parameters.AddWithValue("@address", user.Address ?? "");
                        cmd.Parameters.AddWithValue("@city", user.City ?? "");
                        cmd.Parameters.AddWithValue("@state", user.State ?? "");
                        cmd.Parameters.AddWithValue("@zipCode", user.ZipCode ?? "");
                        cmd.Parameters.AddWithValue("@country", user.Country ?? "Ethiopia");
                        cmd.Parameters.AddWithValue("@dateOfBirth", (object)user.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@gender", user.Gender ?? "");
                        cmd.Parameters.AddWithValue("@currency", user.PreferredCurrency ?? "ETB");
                        
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Re-throw to be caught by UI with specific message
                throw ex;
            }
        }

        public static bool IsUsernameExists(string username)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                
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
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @email";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool UpdateUser(User user)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Users 
                        SET Email = @email, FirstName = @firstName, LastName = @lastName, 
                            Phone = @phone, Address = @address, City = @city, State = @state, 
                            ZipCode = @zipCode, Country = @country, DateOfBirth = @dateOfBirth, 
                            Gender = @gender, PreferredCurrency = @currency, UpdatedDate = GETDATE()
                        WHERE UserId = @userId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", user.UserId);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@phone", user.Phone ?? "");
                        cmd.Parameters.AddWithValue("@address", user.Address ?? "");
                        cmd.Parameters.AddWithValue("@city", user.City ?? "");
                        cmd.Parameters.AddWithValue("@state", user.State ?? "");
                        cmd.Parameters.AddWithValue("@zipCode", user.ZipCode ?? "");
                        cmd.Parameters.AddWithValue("@country", user.Country ?? "Ethiopia");
                        cmd.Parameters.AddWithValue("@dateOfBirth", (object)user.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@gender", user.Gender ?? "");
                        cmd.Parameters.AddWithValue("@currency", user.PreferredCurrency ?? "ETB");
                        
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

        public static bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            try
            {
                // First verify current password
                var user = GetUserById(userId);
                if (user == null || !SecurityHelper.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
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
                        UPDATE Users 
                        SET PasswordHash = @passwordHash, Salt = @salt, UpdatedDate = GETDATE()
                        WHERE UserId = @userId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
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

        public static string GeneratePasswordResetToken(string email)
        {
            try
            {
                string token = SecurityHelper.GenerateSecureToken();
                DateTime expiry = DateTime.Now.AddHours(24); // Token valid for 24 hours

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Users 
                        SET PasswordResetToken = @token, PasswordResetExpiry = @expiry
                        WHERE Email = @email AND IsActive = 1";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@token", token);
                        cmd.Parameters.AddWithValue("@expiry", expiry);
                        cmd.Parameters.AddWithValue("@email", email);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0 ? token : null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool ResetPassword(string token, string newPassword)
        {
            try
            {
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
                        UPDATE Users 
                        SET PasswordHash = @passwordHash, Salt = @salt, 
                            PasswordResetToken = NULL, PasswordResetExpiry = NULL, UpdatedDate = GETDATE()
                        WHERE PasswordResetToken = @token AND PasswordResetExpiry > GETDATE() AND IsActive = 1";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@passwordHash", hash);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@token", token);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static User GetUserById(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserId = @userId";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Phone = reader["Phone"]?.ToString(),
                                Address = reader["Address"]?.ToString(),
                                City = reader["City"]?.ToString(),
                                State = reader["State"]?.ToString(),
                                ZipCode = reader["ZipCode"]?.ToString(),
                                Country = reader["Country"]?.ToString(),
                                DateOfBirth = reader["DateOfBirth"] as DateTime?,
                                Gender = reader["Gender"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]),
                                LastLoginDate = reader["LastLoginDate"] as DateTime?,
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]),
                                PreferredCurrency = reader["PreferredCurrency"]?.ToString() ?? "ETB"
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();
            
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users ORDER BY CreatedDate DESC";
                
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Phone = reader["Phone"]?.ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            IsEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]),
                            LastLoginDate = reader["LastLoginDate"] as DateTime?,
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
            }
            
            return users;
        }

        public static bool ActivateDeactivateUser(int userId, bool isActive)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Users SET IsActive = @isActive, UpdatedDate = GETDATE() WHERE UserId = @userId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
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

        public static bool UpdatePreferredCurrency(int userId, string currency)
        {
            try {
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    string sql = "UPDATE Users SET PreferredCurrency = @currency WHERE UserId = @userId";
                    using (var cmd = new SqlCommand(sql, conn)) {
                        cmd.Parameters.AddWithValue("@currency", currency);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            } catch { return false; }
        }

        private static void UpdateLastLoginDate(int userId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Users SET LastLoginDate = GETDATE() WHERE UserId = @userId";
                    
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
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
}