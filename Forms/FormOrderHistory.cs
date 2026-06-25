using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

// ============================================================
// FORM: ORDER HISTORY — View past orders and their details
// ============================================================
// Event-Driven: SelectionChanged on orders grid triggers
// loading of order details in the second grid.
// ============================================================

namespace E_commerance_System.Forms
{
    public partial class FormOrderHistory : Form
    {
        private User currentUser;
        private string currentCurrency = "ETB";

        public FormOrderHistory(User user, string currency = "ETB")
        {
            currentUser = user;
            currentCurrency = currency;
            
            InitializeComponent();
            
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 126);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;

            dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 126);
            dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDetails.EnableHeadersVisualStyles = false;

            LoadOrders();
        }

        private void BtnBack_Click(object sender, EventArgs e) => this.Close();

        private void LoadOrders()
        {
            try
            {
                var orders = OrderService.GetOrdersByUserId(currentUser.UserId);
                var dt = new System.Data.DataTable();
                dt.Columns.Add("OrderId", typeof(int));
                dt.Columns.Add("Order #", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Order Type", typeof(string));
                dt.Columns.Add("Total", typeof(decimal));
                dt.Columns.Add("Currency", typeof(string));

                foreach (var o in orders)
                {
                    string statusIcon = o.Status;
                    if (o.Status == "Delivered") statusIcon = "✅ Delivered";
                    else if (o.Status == "Denied" || o.Status == "Cancelled") statusIcon = "❌ Cancelled";
                    else statusIcon = "⏳ Processing";

                    string orderType = o.TotalItems == 1 ? "Single Product" : $"Multiple ({o.TotalItems} items)";

                    dt.Rows.Add(o.OrderId, o.OrderNumber, o.OrderDate.ToString("yyyy-MM-dd HH:mm"), statusIcon, o.Phone, orderType, o.TotalAmount, o.CurrencyCode);
                }

                dgvOrders.DataSource = null;
                dgvOrders.DataSource = dt;
                
                if (dgvOrders.Columns.Contains("OrderId")) dgvOrders.Columns["OrderId"].Visible = false;
                if (dgvOrders.Columns.Contains("Total"))
                {
                    dgvOrders.Columns["Total"].DefaultCellStyle.Format = "N2";
                }

                if (orders.Count == 0)
                    lblDetailsTitle.Text = "No orders found. Start shopping!";
            }
            catch (Exception ex) { Console.WriteLine("Error loading orders: " + ex.Message); }
        }

        private void DgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;
            try
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
                string orderNum = dgvOrders.SelectedRows[0].Cells["Order #"].Value.ToString();
                var order = OrderService.GetOrderById(orderId);

                if (order != null && order.OrderDetails != null)
                {
                    var dt = new System.Data.DataTable();
                    dt.Columns.Add("Product Code", typeof(string));
                    dt.Columns.Add("Product Name", typeof(string));
                    dt.Columns.Add("Unit Price", typeof(decimal));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Total", typeof(decimal));

                    foreach (var d in order.OrderDetails)
                        dt.Rows.Add(d.ProductCode, d.ProductName, d.UnitPrice, d.Quantity, d.TotalPrice);

                    dgvDetails.DataSource = null;
                    dgvDetails.DataSource = dt;
                    if (dgvDetails.Columns.Contains("Unit Price")) dgvDetails.Columns["Unit Price"].DefaultCellStyle.Format = "N2";
                    if (dgvDetails.Columns.Contains("Total")) dgvDetails.Columns["Total"].DefaultCellStyle.Format = "N2";
                    
                    lblDetailsTitle.Text = $"📦 Details for Order #{orderNum} ({order.CurrencyCode}) | Placed on: {order.OrderDate:yyyy-MM-dd HH:mm}";
                }
            }
            catch { }
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) { MessageBox.Show("Please select an order to clear."); return; }
            
            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
            string orderNum = dgvOrders.SelectedRows[0].Cells["Order #"].Value.ToString();
            
            if (MessageBox.Show($"Are you sure you want to permanently delete order #{orderNum} from your history?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (OrderService.DeleteOrder(orderId))
                {
                    MessageBox.Show("✅ Order cleared successfully!");
                    LoadOrders();
                    dgvDetails.DataSource = null;
                }
                else { MessageBox.Show("❌ Failed to clear order."); }
            }
        }
    }
}
