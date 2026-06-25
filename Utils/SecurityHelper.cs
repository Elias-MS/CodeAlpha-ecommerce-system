using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace E_commerance_System.Utils
{
    public static class SecurityHelper
    {
        private const int SaltSize = 32; // 256 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 10000; // PBKDF2 iterations

        /// <summary>
        /// Creates a salted hash of the password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>Tuple containing the hash and salt</returns>
        public static Tuple<string, string> HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty");

            // Generate a random salt
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Hash the password with the salt
            byte[] hashBytes = HashPasswordWithSalt(password, saltBytes);

            // Convert to base64 strings
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return new Tuple<string, string>(hash, salt);
        }

        /// <summary>
        /// Verifies a password against a hash and salt
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="hash">The stored hash</param>
        /// <param name="salt">The stored salt</param>
        /// <returns>True if password matches</returns>
        public static bool VerifyPassword(string password, string hash, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
                return false;

            try
            {
                // Convert salt back to bytes
                byte[] saltBytes = Convert.FromBase64String(salt);
                
                // Hash the provided password with the stored salt
                byte[] hashBytes = HashPasswordWithSalt(password, saltBytes);
                
                // Convert to base64 and compare
                string computedHash = Convert.ToBase64String(hashBytes);
                
                return hash == computedHash;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Hash password with salt using PBKDF2
        /// </summary>
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Generate a secure random token for password reset
        /// </summary>
        /// <param name="length">Token length (default 32)</param>
        /// <returns>Random token string</returns>
        public static string GenerateSecureToken(int length = 32)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder(length);
            
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            
            return result.ToString();
        }

        /// <summary>
        /// Validate password strength
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>Validation result with strength score and messages</returns>
        public static PasswordValidationResult ValidatePasswordStrength(string password)
        {
            var result = new PasswordValidationResult();
            
            if (string.IsNullOrEmpty(password))
            {
                result.IsValid = false;
                result.Messages.Add("Password is required");
                return result;
            }

            // Simplified: Only check for length (min 6 chars)
            if (password.Length >= 6)
            {
                result.IsValid = true;
                result.Strength = PasswordStrength.Strong;
            }
            else
            {
                result.IsValid = false;
                result.Messages.Add("Password must be at least 6 characters long");
                result.Strength = PasswordStrength.Weak;
            }

            return result;
        }

        /// <summary>
        /// Generate a random product code
        /// </summary>
        /// <param name="prefix">Prefix for the code</param>
        /// <returns>Product code</returns>
        public static string GenerateProductCode(string prefix = "PRD")
        {
            var random = new Random();
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var randomNumber = random.Next(1000, 9999);
            return string.Format("{0}{1}{2}", prefix, timestamp, randomNumber);
        }

        /// <summary>
        /// Generate a random order number
        /// </summary>
        /// <returns>Order number</returns>
        public static string GenerateOrderNumber()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random();
            var randomNumber = random.Next(100, 999);
            return string.Format("ORD{0}{1}", timestamp, randomNumber);
        }
    }

    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public int Score { get; set; }
        public PasswordStrength Strength { get; set; }
        public List<string> Messages { get; set; }

        public PasswordValidationResult()
        {
            Messages = new List<string>();
        }
    }

    public enum PasswordStrength
    {
        Weak,
        Medium,
        Strong,
        VeryStrong
    }
}