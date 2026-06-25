using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using E_commerance_System.Services;
using E_commerance_System.Data;
using System.Data.SqlClient;

namespace E_commerance_System.Forms
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
            
            // Unhook to prevent premature firing
            this.cmbReportType.SelectedIndexChanged -= new System.EventHandler(this.BtnGenerate_Click);
            this.cmbCurrency.SelectedIndexChanged -= new System.EventHandler(this.CmbCurrency_SelectedIndexChanged);

            cmbReportType.Items.Clear();
            cmbReportType.Items.AddRange(new object[] { 
                "Sales Overview", 
                "Product Performance", 
                "Customer Activity", 
                "Inventory Status" 
            });

            cmbCurrency.Items.Clear();
            cmbCurrency.Items.AddRange(new object[] { "ETB", "USD", "EUR" });
            
            cmbReportType.SelectedIndex = 0;
            cmbCurrency.SelectedIndex = 0;

            // Re-hook
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.BtnGenerate_Click);
            this.cmbCurrency.SelectedIndexChanged += new System.EventHandler(this.CmbCurrency_SelectedIndexChanged);
            
            BtnGenerate_Click(null, null);
        }

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();
        private void CmbCurrency_SelectedIndexChanged(object sender, EventArgs e) => BtnGenerate_Click(null, null);

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = $"Report_{cmbReportType.SelectedItem}_{DateTime.Now:yyyyMMdd}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new System.Text.StringBuilder();
                        var headers = dgvReport.Columns.Cast<System.Windows.Forms.DataGridViewColumn>();
                        sb.AppendLine(string.Join(",", headers.Select(c => $"\"{c.HeaderText}\"")));

                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                var cells = row.Cells.Cast<DataGridViewCell>().Select(c => $"\"{(c.Value?.ToString() ?? "").Replace("\"", "\"\"")}\"");
                                sb.AppendLine(string.Join(",", cells));
                            }
                        }

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                        MessageBox.Show("✅ Report exported successfully!");
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.SelectedItem?.ToString() ?? "Sales Overview";
            string currency = cmbCurrency.SelectedItem?.ToString() ?? "ETB";
            decimal rate = 1.0m;
            
            // Exchange rates (could be fetched from CurrencyRates table if populated)
            if (currency == "USD") rate = 1.0m / 55.0m;
            else if (currency == "EUR") rate = 1.0m / 60.0m;

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "";
                    
                    switch (reportType)
                    {
                        case "Sales Overview":
                            query = $@"SELECT CAST(OrderDate AS DATE) as [Date], 
                                       COUNT(OrderId) as [Total Orders], 
                                       SUM(TotalAmount * {rate}) as [Revenue ({currency})]
                                       FROM Orders 
                                       GROUP BY CAST(OrderDate AS DATE) 
                                       ORDER BY [Date] DESC";
                            break;
                        case "Product Performance":
                            query = $@"SELECT ProductName as [Product], 
                                       SUM(Quantity) as [Units Sold], 
                                       SUM(TotalPrice * {rate}) as [Total Sales ({currency})]
                                       FROM OrderDetails 
                                       GROUP BY ProductName 
                                       ORDER BY [Units Sold] DESC";
                            break;
                        case "Customer Activity":
                            query = $@"SELECT u.Username, 
                                       COUNT(o.OrderId) as [Orders Placed], 
                                       SUM(o.TotalAmount * {rate}) as [Total Spent ({currency})]
                                       FROM Users u
                                       LEFT JOIN Orders o ON u.UserId = o.UserId
                                       GROUP BY u.Username
                                       ORDER BY [Total Spent ({currency})] DESC";
                            break;
                        case "Inventory Status":
                            query = $@"SELECT Name, ProductCode, Stock, Price * {rate} as [Price ({currency})]
                                       FROM Products 
                                       WHERE IsActive = 1
                                       ORDER BY Stock ASC";
                            break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
