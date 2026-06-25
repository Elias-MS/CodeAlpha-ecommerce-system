using System;

namespace E_commerance_System.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string PreferredCurrency { get; set; }

        public User()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = true;
            IsEmailVerified = false;
            Country = "Ethiopia";
            PreferredCurrency = "ETB";
        }

        // Computed properties
        public string FullName => $"{FirstName} {LastName}".Trim();
        public int Age => DateOfBirth.HasValue ? DateTime.Now.Year - DateOfBirth.Value.Year : 0;
        public string FullAddress => $"{Address}, {City}, {State} {ZipCode}, {Country}".Trim(' ', ',');
        public bool IsPasswordResetValid => PasswordResetExpiry.HasValue && PasswordResetExpiry > DateTime.Now;
    }
}