using System;

namespace E_commerance_System.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Permissions { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public Admin()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            Role = "Admin";
        }

        // Permission checking methods
        public bool HasPermission(string permission)
        {
            if (string.IsNullOrEmpty(Permissions))
                return false;
            
            return Permissions.Contains("ALL") || Permissions.Contains(permission);
        }

        public bool IsSuperAdmin => Role == "SuperAdmin" || HasPermission("ALL");
        public bool CanManageProducts => HasPermission("PRODUCTS") || IsSuperAdmin;
        public bool CanManageUsers => HasPermission("USERS") || IsSuperAdmin;
        public bool CanManageOrders => HasPermission("ORDERS") || IsSuperAdmin;
        public bool CanViewReports => HasPermission("REPORTS") || IsSuperAdmin;
        public bool CanManageSystem => HasPermission("SYSTEM") || IsSuperAdmin;
    }
}