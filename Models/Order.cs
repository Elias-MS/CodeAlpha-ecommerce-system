using System;
using System.Collections.Generic;

namespace E_commerance_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string PaymentReference { get; set; }
        public string CurrencyCode { get; set; } // Stores "ETB", "USD", etc.
        public string PaymentProofImage { get; set; } // Path to proof screenshot
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Navigation properties
        public User Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Payment> Payments { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            Status = "Pending";
            PaymentStatus = "Pending";
            OrderDetails = new List<OrderDetail>();
            Payments = new List<Payment>();
            OrderNumber = GenerateOrderNumber();
        }

        // Computed properties
        public int TotalItems => OrderDetails?.Count ?? 0;
        public int TotalQuantity
        {
            get
            {
                int total = 0;
                if (OrderDetails != null)
                {
                    foreach (var item in OrderDetails)
                        total += item.Quantity;
                }
                return total;
            }
        }

        public bool IsCompleted => Status == "Delivered";
        public bool IsCancelled => Status == "Cancelled";
        public bool IsPending => Status == "Pending";
        public bool IsProcessing => Status == "Processing";
        public bool IsShipped => Status == "Shipped";
        public bool IsDelivered => Status == "Delivered";

        public bool IsPaymentCompleted => PaymentStatus == "Completed" || PaymentStatus == "Paid";
        public bool IsPaymentPending => PaymentStatus == "Pending";
        public bool IsPaymentFailed => PaymentStatus == "Failed";

        private string GenerateOrderNumber()
        {
            return $"ORD{DateTime.Now:yyyyMMdd}{DateTime.Now.Ticks.ToString().Substring(10)}";
        }

        public void CalculateTotals()
        {
            SubTotal = 0;
            if (OrderDetails != null)
            {
                foreach (var detail in OrderDetails)
                {
                    SubTotal += detail.TotalPrice;
                }
            }
            TotalAmount = SubTotal + TaxAmount + ShippingCost - DiscountAmount;
        }
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }

        public OrderDetail()
        {
            DiscountAmount = 0;
        }

        public decimal EffectivePrice => UnitPrice - DiscountAmount;
        public void CalculateTotal()
        {
            TotalPrice = (UnitPrice - DiscountAmount) * Quantity;
        }
    }
}