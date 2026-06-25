using System;

namespace E_commerance_System.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public string Parameters { get; set; }
        public int? GeneratedBy { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string FilePath { get; set; }
        public long? FileSize { get; set; }
        public string Status { get; set; }

        public Report()
        {
            GeneratedDate = DateTime.Now;
            Status = "Generated";
        }

        // Report types
        public static class ReportTypes
        {
            public const string DailySales = "Daily Sales";
            public const string MonthlySales = "Monthly Sales";
            public const string ProductSales = "Product Sales";
            public const string CustomerActivity = "Customer Activity";
            public const string InventoryReport = "Inventory Report";
            public const string RevenueReport = "Revenue Report";
            public const string OrderReport = "Order Report";
        }

        // Computed properties
        public string FileSizeFormatted
        {
            get
            {
                if (!FileSize.HasValue) return "Unknown";
                
                string[] sizes = { "B", "KB", "MB", "GB" };
                double len = FileSize.Value;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                return $"{len:0.##} {sizes[order]}";
            }
        }

        public bool IsGenerated => Status == "Generated";
        public bool IsProcessing => Status == "Processing";
        public bool HasError => Status == "Error";
    }

    // Report data models
    public class SalesReportData
    {
        public DateTime Date { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalProfit { get; set; }
        public int ProductsSold { get; set; }
    }

    public class ProductSalesData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int QuantitySold { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
        public int ViewCount { get; set; }
    }

    public class CustomerActivityData
    {
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }

    public class InventoryData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int CurrentStock { get; set; }
        public int MinStockLevel { get; set; }
        public string StockStatus { get; set; }
        public decimal Value { get; set; }
        public DateTime LastRestocked { get; set; }
    }
}