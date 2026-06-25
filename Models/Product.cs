using System;
using System.Collections.Generic;

namespace E_commerance_System.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public DateTime? DiscountExpiry { get; set; }
        public decimal? CostPrice { get; set; }
        public int Stock { get; set; }
        public int MinStockLevel { get; set; }
        public decimal? Weight { get; set; }
        public string Dimensions { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public string Warranty { get; set; }
        public string MainImagePath { get; set; }
        public string ImageGallery { get; set; }
        public string Tags { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; } = true;
        public int ViewCount { get; set; }
        public int SalesCount { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }

        // Navigation properties
        public Category Category { get; set; }
        public List<Review> Reviews { get; set; }
        public List<string> ImageList { get; set; }

        public Product()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = true;
            IsFeatured = false;
            MinStockLevel = 5;
            ViewCount = 0;
            SalesCount = 0;
            AverageRating = 0;
            ReviewCount = 0;
            Reviews = new List<Review>();
            ImageList = new List<string>();
        }

        // Computed properties
        public bool HasDiscount => DiscountPrice.HasValue && DiscountPrice < Price && (!DiscountExpiry.HasValue || DiscountExpiry > DateTime.Now);
        public decimal EffectivePrice => HasDiscount ? DiscountPrice.Value : Price;
        public decimal DiscountPercentage => HasDiscount ? Math.Round(((Price - DiscountPrice.Value) / Price) * 100, 2) : 0;
        public bool IsInStock => Stock > 0;
        public bool IsLowStock => Stock <= MinStockLevel;
        public string StockStatus => IsInStock ? (IsLowStock ? "Low Stock" : "In Stock") : "Out of Stock";
        public List<string> GetImageList()
        {
            var images = new List<string>();
            if (!string.IsNullOrEmpty(MainImagePath))
                images.Add(MainImagePath);
            
            if (!string.IsNullOrEmpty(ImageGallery))
            {
                var galleryImages = ImageGallery.Split(',');
                foreach (var img in galleryImages)
                {
                    if (!string.IsNullOrWhiteSpace(img))
                        images.Add(img.Trim());
                }
            }
            return images;
        }
    }
}