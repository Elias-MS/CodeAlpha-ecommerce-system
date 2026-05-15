using System;
using System.Collections.Generic;

namespace E_commerance_System.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
            IsActive = true;
            SortOrder = 0;
            CreatedDate = DateTime.Now;
            SubCategories = new List<Category>();
            Products = new List<Product>();
        }

        public bool HasSubCategories => SubCategories?.Count > 0;
        public bool IsSubCategory => ParentCategoryId.HasValue;
    }
}