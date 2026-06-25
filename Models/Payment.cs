using System;

namespace E_commerance_System.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentGateway { get; set; }
        public string GatewayResponse { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Order Order { get; set; }

        public Payment()
        {
            PaymentDate = DateTime.Now;
            PaymentStatus = "Pending";
        }

        // Payment methods enum-like constants
        public static class PaymentMethods
        {
            public const string CashOnDelivery = "Cash on Delivery";
            public const string CreditCard = "Credit Card";
            public const string DebitCard = "Debit Card";
            public const string BankTransfer = "Bank Transfer";
            public const string MobileMoney = "Mobile Money";
            public const string PayPal = "PayPal";
        }

        // Payment status enum-like constants
        public static class PaymentStatuses
        {
            public const string Pending = "Pending";
            public const string Processing = "Processing";
            public const string Completed = "Completed";
            public const string Failed = "Failed";
            public const string Cancelled = "Cancelled";
            public const string Refunded = "Refunded";
        }

        // Computed properties
        public bool IsCompleted => PaymentStatus == PaymentStatuses.Completed;
        public bool IsPending => PaymentStatus == PaymentStatuses.Pending;
        public bool IsFailed => PaymentStatus == PaymentStatuses.Failed;
        public bool IsCashOnDelivery => PaymentMethod == PaymentMethods.CashOnDelivery;
        public bool IsOnlinePayment => !IsCashOnDelivery;
    }
}