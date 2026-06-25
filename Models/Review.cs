using System;

namespace E_commerance_System.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public bool IsApproved { get; set; }
        public int HelpfulCount { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }

        public Review()
        {
            CreatedDate = DateTime.Now;
            IsApproved = false;
            HelpfulCount = 0;
            IsVerifiedPurchase = false;
        }

        // Validation
        public bool IsValidRating => Rating >= 1 && Rating <= 5;
        
        // Display properties
        public string RatingStars
        {
            get
            {
                string stars = "";
                for (int i = 1; i <= 5; i++)
                {
                    stars += i <= Rating ? "★" : "☆";
                }
                return stars;
            }
        }

        public string ReviewerName => User?.FullName ?? "Anonymous";
        public string ProductName => Product?.Name ?? "";
        public string StatusText => IsApproved ? "Approved" : "Pending Approval";
        public string VerificationText => IsVerifiedPurchase ? "Verified Purchase" : "Unverified";
    }
}