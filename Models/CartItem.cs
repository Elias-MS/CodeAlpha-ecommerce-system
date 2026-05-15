using System;

namespace E_commerance_System.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }

        // Navigation properties
        public Product Product { get; set; }

        public CartItem()
        {
            AddedDate = DateTime.Now;
            Quantity = 1;
        }

        // Computed properties
        public decimal Subtotal => Price * Quantity;
        public decimal EffectivePrice => Product?.EffectivePrice ?? Price;
        public decimal EffectiveSubtotal => EffectivePrice * Quantity;
        public bool HasDiscount => Product?.HasDiscount ?? false;
        public decimal DiscountAmount => HasDiscount ? (Price - EffectivePrice) * Quantity : 0;
        public string ProductImage => Product?.MainImagePath ?? "";
        public bool IsInStock => Product?.IsInStock ?? false;
        public int AvailableStock => Product?.Stock ?? 0;
        public bool IsQuantityValid => Quantity <= AvailableStock;
    }
}