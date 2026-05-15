using System;

namespace E_commerance_System.Models
{
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; } // e.g., "ETB", "USD", "EUR"
        public decimal RateToETB { get; set; }   // 1 unit of this currency = X units of ETB
        public string Symbol { get; set; }        // e.g., "Br", "$", "€"
        public DateTime LastUpdated { get; set; }

        public CurrencyRate() { }

        public CurrencyRate(string code, decimal rate, string symbol)
        {
            CurrencyCode = code;
            RateToETB = rate;
            Symbol = symbol;
            LastUpdated = DateTime.Now;
        }
    }
}
