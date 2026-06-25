using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Services
{
    public class CurrencyService
    {
        public static string CurrentCurrency { get; set; } = "ETB";

        public static void SyncCurrencySelector(ComboBox cmb, Action onCurrencyChanged)
        {
            try
            {
                var rates = GetAllRates();
                cmb.Items.Clear();
                foreach (var r in rates) cmb.Items.Add(r.CurrencyCode);
                
                cmb.SelectedItem = CurrentCurrency;
                cmb.SelectedIndexChanged += (s, e) => {
                    CurrentCurrency = cmb.SelectedItem.ToString();
                    onCurrencyChanged?.Invoke();
                };
            }
            catch { }
        }

        public static string GetSymbol(string code)
        {
            try {
                var rates = GetAllRates();
                var r = rates.Find(x => x.CurrencyCode == code);
                return r != null ? r.Symbol : "Br";
            } catch { return "Br"; }
        }

        public static List<CurrencyRate> GetAllRates()
        {
            var rates = new List<CurrencyRate>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM CurrencyRates ORDER BY CurrencyCode";
                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rates.Add(new CurrencyRate
                        {
                            CurrencyCode = reader["CurrencyCode"].ToString(),
                            RateToETB = Convert.ToDecimal(reader["RateToETB"]),
                            Symbol = reader["Symbol"].ToString(),
                            LastUpdated = Convert.ToDateTime(reader["LastUpdated"])
                        });
                    }
                }
            }
            return rates;
        }

        public static decimal GetRate(string code)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT RateToETB FROM CurrencyRates WHERE CurrencyCode = @code";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    object result = cmd.ExecuteScalar();
                    if (result != null) return Convert.ToDecimal(result);
                }
            }
            return 1.0m; // Default to 1:1 if not found
        }

        public static bool UpdateRate(string code, decimal rate)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE CurrencyRates SET RateToETB = @rate, LastUpdated = GETDATE() WHERE CurrencyCode = @code";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@rate", rate);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch { return false; }
        }

        public static bool AddCurrency(string code, decimal rate, string symbol)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol, LastUpdated) VALUES (@code, @rate, @symbol, GETDATE())";
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@rate", rate);
                        cmd.Parameters.AddWithValue("@symbol", symbol);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch { return false; }
        }

        public static decimal ConvertAmount(decimal amountInETB, string targetCurrency)
        {
            if (targetCurrency == "ETB") return amountInETB;
            decimal rate = GetRate(targetCurrency);
            if (rate == 0) return amountInETB;
            return amountInETB / rate;
        }
    }
}
