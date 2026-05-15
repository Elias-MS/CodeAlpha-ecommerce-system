using System;

namespace E_commerance_System.Models
{
    public class OrderHistory
    {
        public int HistoryId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string UpdateNotes { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdatedBy { get; set; }

        public OrderHistory()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
