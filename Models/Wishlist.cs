using System;

namespace E_commerance_System.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedDate { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Product Product { get; set; }

        public Wishlist()
        {
            AddedDate = DateTime.Now;
        }

        // Computed properties
        public string ProductName => Product?.Name ?? "";
        public decimal ProductPrice => Product?.EffectivePrice ?? 0;
        public bool IsProductAvailable => Product?.IsInStock ?? false;
        public string ProductImage => Product?.MainImagePath ?? "";
    }
}