using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

// ============================================================
// FORM: SHOPPING CART — View, modify, and checkout cart items
// ============================================================
// Event-Driven Concepts:
//   • DataGridView CellClick → update/remove items
//   • Button Click → place order with transaction support
// ============================================================

namespace E_commerance_System.Forms
{
    public partial class FormCart : Form
    {
        private User currentUser;
        private string selectedProofPath = "";


        public FormCart(User user)
        {
            currentUser = user;
            InitializeComponent();
            
            // ... (existing bank population) ...
            
            // Temporarily unhook event to prevent premature firing during population
            this.cmbPaymentMethod.SelectedIndexChanged -= new System.EventHandler(this.CmbPaymentMethod_SelectedIndexChanged);

            // Populate payment methods
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.AddRange(new object[] { "Cash on Delivery", "Bank Transfer", "Telebirr", "M-Pesa" });
            cmbPaymentMethod.SelectedIndex = 0;
            
            // Populate banks
            cmbBankList.Items.Clear();
            cmbBankList.Items.AddRange(new object[] { "Commercial Bank of Ethiopia (CBE)", "Awash Bank", "Dashen Bank", "Abyssinia Bank" });
            cmbBankList.SelectedIndex = 0;

            txtShippingAddress.Text = currentUser.Address ?? "";
            txtPhone.Text = currentUser.Phone ?? "";

            // Re-hook event and trigger initial state
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.CmbPaymentMethod_SelectedIndexChanged);
            CmbPaymentMethod_SelectedIndexChanged(null, null);
            
            LoadCart();
        }

        private void BtnClearCart_Click(object sender, EventArgs e) { ShoppingCartService.ClearCart(); LoadCart(); }

        private void CmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMethod == null || cmbPaymentMethod.SelectedItem == null) return;
            
            string method = cmbPaymentMethod.SelectedItem.ToString();
            
            // Ensure all dependent controls exist
            if (lblBank == null || cmbBankList == null || lblTID == null || txtTransactionId == null) return;
            
            // Default: Hide all extra payment fields
            lblBank.Visible = false;
            cmbBankList.Visible = false;
            lblTID.Visible = false;
            txtTransactionId.Visible = false;

            if (method == "Cash on Delivery")
            {
                // Nothing extra to show
            }
            else if (method == "Bank Transfer")
            {
                lblBank.Visible = true;
                cmbBankList.Visible = true;
                lblTID.Visible = true;
                txtTransactionId.Visible = true;
                lblTID.Text = "🏦 Transaction ID / Ref:";
                lblTID.Location = new Point(420, 195);
                txtTransactionId.Location = new Point(420, 218);
            }
            else if (method == "Telebirr" || method == "M-Pesa")
            {
                lblTID.Visible = true;
                txtTransactionId.Visible = true;
                lblTID.Text = "📱 Transaction Reference Number:";
                lblTID.Location = new Point(420, 135);
                txtTransactionId.Location = new Point(420, 158);
            }
        }

        private void LoadCart()
        {
            var items = ShoppingCartService.GetCartItems();
            string currency = CurrencyService.CurrentCurrency;
            decimal rate = CurrencyService.GetRate(currency);
            string symbol = CurrencyService.GetSymbol(currency);

            var dt = new System.Data.DataTable();
            dt.Columns.Add("ProductId", typeof(int));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Subtotal", typeof(decimal));

            foreach (var item in items)
            {
                decimal displayPrice = rate != 0 ? item.Price / rate : item.Price;
                decimal displaySubtotal = rate != 0 ? item.Subtotal / rate : item.Subtotal;
                dt.Rows.Add(item.ProductId, item.ProductName, displayPrice, item.Quantity, displaySubtotal);
            }

            dgvCart.DataSource = dt;
            if (dgvCart.Columns.Contains("ProductId")) dgvCart.Columns["ProductId"].Visible = false;
            if (dgvCart.Columns.Contains("Price"))
            {
                dgvCart.Columns["Price"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["Price"].HeaderText = $"Price ({symbol})";
            }
            if (dgvCart.Columns.Contains("Subtotal"))
            {
                dgvCart.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["Subtotal"].HeaderText = $"Subtotal ({symbol})";
            }

            decimal totalETB = ShoppingCartService.GetCartTotal();
            decimal displayTotal = rate != 0 ? totalETB / rate : totalETB;

            lblSubtotal.Text = $"Subtotal: {displayTotal:N2} {symbol}";
            lblTotal.Text = $"Total: {displayTotal:N2} {symbol}";
            btnPlaceOrder.Enabled = items.Count > 0;
        }

        private void BtnUpdateQty_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0) { MessageBox.Show("Select an item first."); return; }
            int productId = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["ProductId"].Value);
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter new quantity:", "Update Quantity", dgvCart.SelectedRows[0].Cells["Qty"].Value.ToString());
            if (int.TryParse(input, out int newQty) && newQty > 0)
            { ShoppingCartService.UpdateQuantity(productId, newQty); LoadCart(); }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0) { MessageBox.Show("Select an item to remove."); return; }
            int productId = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["ProductId"].Value);
            string productName = dgvCart.SelectedRows[0].Cells["Product"].Value.ToString();
            if (MessageBox.Show($"Remove \"{productName}\" from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { ShoppingCartService.RemoveFromCart(productId); LoadCart(); }
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!ShoppingCartService.HasItems()) { MessageBox.Show("Your cart is empty."); return; }
            
            bool isDigitalPayment = cmbPaymentMethod.SelectedItem.ToString() != "Cash on Delivery";
            
            // Basic required fields
            if (string.IsNullOrWhiteSpace(txtShippingAddress.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length < 10)
            {
                MessageBox.Show("⚠️ Please provide a valid shipping address and a contact phone number (at least 10 digits).", "Invalid Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Digital payment specific validation
            if (isDigitalPayment)
            {
                if (string.IsNullOrWhiteSpace(txtTransactionId.Text))
                {
                    MessageBox.Show("⚠️ Please enter your Transaction Reference Number.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(selectedProofPath))
                {
                    MessageBox.Show("📸 Please attach a screenshot of your payment proof before placing the order.", "Attachment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            decimal rate = CurrencyService.GetRate(CurrencyService.CurrentCurrency);
            decimal displayTotal = rate != 0 ? ShoppingCartService.GetCartTotal() / rate : ShoppingCartService.GetCartTotal();

            if (MessageBox.Show($"Place order for {displayTotal:N2} {CurrencyService.CurrentCurrency}?", "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string paymentMethod = cmbPaymentMethod.SelectedItem.ToString();
                string fullPaymentMethod = paymentMethod;
                if (paymentMethod == "Bank Transfer")
                    fullPaymentMethod += $" ({cmbBankList.SelectedItem})";

                var order = ShoppingCartService.CreateOrderFromCart(currentUser.UserId, txtShippingAddress.Text.Trim(), null, fullPaymentMethod);
                if (order != null)
                {
                    order.PaymentReference = txtTransactionId.Text.Trim();
                    order.Phone = txtPhone.Text.Trim();
                    order.EstimatedDelivery = dtpDeliveryDate.Value;
                    order.CurrencyCode = CurrencyService.CurrentCurrency;
                    order.PaymentProofImage = selectedProofPath;
                }
                
                if (order != null && OrderService.CreateOrder(order))
                {
                    string summary = $"🎉 Order placed successfully!\n\n" +
                                     $"🆔 Order #: {order.OrderNumber}\n" +
                                     $"💰 Total: {displayTotal:N2} {CurrencyService.CurrentCurrency}\n" +
                                     $"💳 Payment: {order.PaymentMethod}\n" +
                                     $"🔑 Ref/Phone: {order.PaymentReference}\n" +
                                     $"📍 Shipping To: {order.ShippingAddress}\n" +
                                     $"📞 Contact: {order.Phone}\n" +
                                     $"📅 Delivery: {order.EstimatedDelivery:yyyy-MM-dd}\n\n" +
                                     "Thank you for your purchase!";

                    MessageBox.Show(summary, "Order Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShoppingCartService.ClearCart();
                    this.Close();
                }
                else { MessageBox.Show("❌ Failed to place order. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Order Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnBrowseProof_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Multiselect = true })
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Proofs");
                        if (!System.IO.Directory.Exists(directory)) System.IO.Directory.CreateDirectory(directory);

                        var savedPaths = new System.Collections.Generic.List<string>();
                        foreach (string file in ofd.FileNames)
                        {
                            string fileName = $"Proof_{Guid.NewGuid().ToString().Substring(0, 8)}_{DateTime.Now:yyyyMMdd_HHmmss}{System.IO.Path.GetExtension(file)}";
                            string destPath = System.IO.Path.Combine(directory, fileName);
                            System.IO.File.Copy(file, destPath, true);
                            savedPaths.Add(fileName); // Save ONLY the filename
                        }
                        selectedProofPath = string.Join(";", savedPaths);
                        lblProofPath.Text = $"{ofd.FileNames.Length} file(s) attached";
                        lblProofPath.ForeColor = Color.DarkGreen;
                        btnBrowseProof.Text = "✅ Proof Attached";
                        btnBrowseProof.BackColor = Color.FromArgb(0, 137, 123);
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }
    }
}
