using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;
using System.Data.SqlClient;
using E_commerance_System.Data;

namespace E_commerance_System.Forms
{
    public partial class FormCustomerDashboard : Form
    {
        private User currentUser;
        private Label lblTicker;
        private Timer tickerTimer;


        public FormCustomerDashboard(User user)
        {
            currentUser = user;
            InitializeComponent();
            SetupModernUI();
            LoadCategories();
            UpdateCartCount();

            if (!string.IsNullOrEmpty(currentUser.PreferredCurrency))
                CurrencyService.CurrentCurrency = currentUser.PreferredCurrency;

            CurrencyService.SyncCurrencySelector(cmbCurrency, () => {
                if (currentUser.UserId != 0) {
                    UserService.UpdatePreferredCurrency(currentUser.UserId, CurrencyService.CurrentCurrency);
                    currentUser.PreferredCurrency = CurrencyService.CurrentCurrency;
                }
                LoadProducts();
            });
            
            LoadProducts();
        }

        private void SetupModernUI()
        {
            this.Size = new Size(1280, 850);
            this.BackColor = Color.FromArgb(245, 245, 250);

            // Row 1: Navigation & Logo
            var pnlNav = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(18, 18, 24) };
            var lblLogo = new Label { Text = "🛒 ELITE STORE", ForeColor = Color.FromArgb(0, 150, 136), Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };
            pnlNav.Controls.Add(lblLogo);

            var flpNavButtons = new FlowLayoutPanel { Location = new Point(200, 0), Size = new Size(1000, 60), FlowDirection = FlowDirection.LeftToRight };
            CreateTopNavButton(flpNavButtons, "🏠 Home", (s, e) => LoadProducts());
            CreateTopNavButton(flpNavButtons, "❤️ Wishlist", BtnWishlist_Click);
            CreateTopNavButton(flpNavButtons, "📋 My Orders", BtnOrders_Click);
            CreateTopNavButton(flpNavButtons, "👤 Profile", BtnProfile_Click);
            CreateTopNavButton(flpNavButtons, "🚪 Logout", BtnLogout_Click);
            pnlNav.Controls.Add(flpNavButtons);


            // Row 2: Search & Filter (Cleanly structured)
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Height = 70;
            pnlSearch.BackColor = Color.White;
            pnlSearch.Padding = new Padding(15);
            
            lblCat.Location = new Point(20, 25);
            cmbCategory.Location = new Point(90, 22);
            cmbCategory.Width = 180;
            
            lblSrch.Location = new Point(300, 25);
            txtSearch.Location = new Point(360, 22);
            txtSearch.Width = 300;
            
            btnSearch.Location = new Point(670, 18);
            btnSearch.Size = new Size(110, 35);
            
            btnProfile.Visible = false; // Resolved: Removed duplicate profile button to fix overlap
            btnShowAll.Visible = false; // Resolved: Removed duplicate show all button
            
            btnCart.Location = new Point(1050, 15);
            btnCart.Size = new Size(160, 40);
            btnCart.BackColor = Color.FromArgb(26, 35, 126);
            btnCart.ForeColor = Color.White;
            btnCart.FlatStyle = FlatStyle.Flat;
            btnCart.FlatAppearance.BorderSize = 0;
            btnCart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Grid Styling (Single/Multiple selection optimized)
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = true; 
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.ColumnHeadersHeight = 45;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 126);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.EnableHeadersVisualStyles = false;

            // Bottom Panel (Resolved Overlap)
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Height = 85;
            pnlBottom.BackColor = Color.White;
            
            lblProductInfo.Location = new Point(20, 30);
            lblProductInfo.Text = "💡 Tip: Hold Ctrl to select multiple items (Clothes, etc.) to add them all at once!";
            lblProductInfo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblProductInfo.ForeColor = Color.FromArgb(0, 121, 107);
            
            lblQty.Visible = false; // Hidden for bulk select mode
            numQuantity.Visible = false; // Hidden for bulk select mode
            
            btnAddToCart.Text = "🛒 Add Selected to Cart";
            btnAddToCart.Location = new Point(980, 20);
            btnAddToCart.Size = new Size(240, 45);
            btnAddToCart.BackColor = Color.FromArgb(0, 137, 123);
            
            btnAddToWishlist.Text = "❤️ Wishlist";
            btnAddToWishlist.Location = new Point(800, 20);
            btnAddToWishlist.Size = new Size(160, 45);
            btnAddToWishlist.BackColor = Color.FromArgb(63, 81, 181);

            var lblAnnounce = new Label {
                Text = "📢 Stay updated with the latest news & announcements!",
                Location = new Point(250, 62),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic)
            };
            var lblComplaint = new LinkLabel { 
                Text = "🚩 Have a Problem? Submit a Complaint", 
                Location = new Point(20, 60), 
                AutoSize = true, 
                LinkColor = Color.FromArgb(198, 40, 40),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ActiveLinkColor = Color.Red,
                VisitedLinkColor = Color.FromArgb(198, 40, 40)
            };
            lblComplaint.Click += (s, e) => {
                if (currentUser.UserId == 0) MessageBox.Show("Please login to submit a complaint.");
                else new FormComplaint(currentUser).ShowDialog();
            };
            pnlBottom.Controls.Add(lblComplaint);
            pnlBottom.Controls.Add(lblAnnounce);

            this.Controls.Clear();
            this.Controls.Add(dgvProducts);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlSearch);
            this.Controls.Add(pnlNav);
            
            // Notification Bar
            var pnlNotifBar = new Panel { Dock = DockStyle.Top, Height = 30, BackColor = Color.FromArgb(20, 20, 25), Padding = new Padding(10, 5, 10, 5) };
            lblTicker = new Label { Text = "Loading announcements...", ForeColor = Color.FromArgb(255, 215, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(pnlNotifBar.Width, 5) };
            pnlNotifBar.Controls.Add(lblTicker);
            this.Controls.Add(pnlNotifBar);
            pnlNotifBar.SendToBack();

            tickerTimer = new Timer { Interval = 50 };
            tickerTimer.Tick += (s, e) => {
                lblTicker.Left -= 2;
                if (lblTicker.Right < 0) lblTicker.Left = pnlNotifBar.Width;
            };

            tickerTimer.Start();
            UpdateTickerText();

            // Only show news and announcements for registered users (User Only)
            if (currentUser.UserId == 0)
            {
                pnlNotifBar.Visible = false;
            }
            else
            {
                CheckForAnnouncements();
            }
        }


        private Button CreateTopNavButton(Control parent, string text, EventHandler onClick)
        {
            var btn = new Button {
                Text = text,
                Width = 140,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 10F),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 40, 50);
            btn.Click += onClick;
            parent.Controls.Add(btn);
            return btn;
        }


        private void UpdateTickerText()
        {
            if (currentUser.UserId == 0) return;
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 Content FROM Announcements ORDER BY CreatedDate DESC";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null) lblTicker.Text = "🌟 NEWS: " + result.ToString() + "          |          Happy Shopping! 🛍️";
                        else lblTicker.Text = "🌟 Welcome to E-Commerce Elite! Check back here for latest deals and announcements. 🌟";
                    }
                }
            }
            catch { lblTicker.Text = "🌟 Welcome to our Store! 🌟"; }
        }

        private void BtnProfile_Click(object sender, EventArgs e) => new FormCustomerProfile(currentUser).ShowDialog();
        private void BtnShowAll_Click(object sender, EventArgs e) { txtSearch.Clear(); cmbCategory.SelectedIndex = 0; LoadProducts(); }
        private void DgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ID"].Value);
                var p = ProductService.GetProductById(id);
                if (p != null) new FormProductDetail(p).ShowDialog();
            }
        }
        private void BtnWishlist_Click(object sender, EventArgs e)
        {
            if (currentUser.UserId == 0) { MessageBox.Show("Please login to manage your wishlist.", "Guest Mode"); return; }
            new FormWishlist(currentUser, CurrencyService.CurrentCurrency).ShowDialog();
        }
        private void CmbCurrency_SelectedIndexChanged(object sender, EventArgs e) 
        {
            string currency = cmbCurrency.SelectedItem?.ToString() ?? "ETB";
            if (currentUser.UserId != 0) 
            {
                UserService.UpdatePreferredCurrency(currentUser.UserId, currency);
                currentUser.PreferredCurrency = currency;
            }
            LoadProducts();
        }

        // === DATA LOADING METHODS ===
        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");
            try
            {
                var categories = CategoryService.GetAllCategories();
                foreach (var cat in categories) 
                {
                    cmbCategory.Items.Add(cat.CategoryName);
                    // Also add to sidebar? Maybe later
                }
            }
            catch { }
            cmbCategory.SelectedIndex = 0;
        }

        private void LoadProducts(System.Collections.Generic.List<Product> products = null)
        {
            try
            {
                if (products == null) products = ProductService.GetAllProducts();
                
                string currency = CurrencyService.CurrentCurrency;
                decimal rate = CurrencyService.GetRate(currency);
                string symbol = CurrencyService.GetSymbol(currency);

                var dt = new System.Data.DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Stock", typeof(int));
                dt.Columns.Add("Brand", typeof(string));

                foreach (var p in products)
                {
                    // p.Price is in ETB (Base). To get target currency: Price / RateToETB
                    decimal displayPrice = rate != 0 ? p.Price / rate : p.Price;
                    dt.Rows.Add(p.ProductId, p.ProductCode, p.Name, p.CategoryName, displayPrice, p.Stock, p.Brand);
                }

                dgvProducts.DataSource = dt;
                if (dgvProducts.Columns.Contains("ID")) dgvProducts.Columns["ID"].Visible = false;
                if (dgvProducts.Columns.Contains("Price"))
                {
                    dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N2";
                    dgvProducts.Columns["Price"].HeaderText = $"Price ({symbol})";
                }
            }
            catch { }
        }

        private void CheckForAnnouncements()
        {
            if (currentUser.UserId == 0) return;
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 Title, Content FROM Announcements WHERE IsActive = 1 ORDER BY CreatedDate DESC";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string title = reader["Title"].ToString();
                            string msg = reader["Content"].ToString();
                            
                            // Show as a discrete notification or message box on login
                            Timer t = new Timer { Interval = 2000 }; // Delay slightly
                            t.Tick += (s, e) => {
                                t.Stop();
                                MessageBox.Show($"📢 ANNOUNCEMENT: {title}\n\n{msg}", "Store News", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            t.Start();
                        }
                    }
                }
            }
            catch { }
        }

        private void AddToCart(Product p, int qty)
        {
            if (currentUser.UserId == 0) { MessageBox.Show("Login to add to cart."); return; }
            if (p.Stock < qty) { MessageBox.Show("Not enough stock."); return; }
            ShoppingCartService.AddToCart(p, qty);
            UpdateCartCount();
            MessageBox.Show($"✅ {p.Name} added to cart!");
        }

        private void AddToWishlist(int productId)
        {
            if (currentUser.UserId == 0) { MessageBox.Show("Login to use wishlist."); return; }
            if (WishlistService.AddToWishlist(currentUser.UserId, productId)) MessageBox.Show("✅ Added to wishlist!");
        }

        private void UpdateCartCount()
        {
            int count = ShoppingCartService.GetCartItemCount();
            lblCartCount.Text = $"🛒 Cart: {count} item{(count != 1 ? "s" : "")}";
        }

        // === EVENT HANDLERS ===
        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex <= 0) { LoadProducts(); return; }
            try
            {
                string catName = cmbCategory.SelectedItem.ToString();
                var cat = CategoryService.GetCategoryByName(catName);
                if (cat != null) LoadProducts(ProductService.GetProductsByCategory(cat.CategoryId));
            }
            catch { }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) { LoadProducts(); return; }
            try { LoadProducts(ProductService.SearchProducts(txtSearch.Text.Trim())); }
            catch (Exception ex) { MessageBox.Show("Search error: " + ex.Message); }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (currentUser.UserId == 0)
            {
                MessageBox.Show("Please Login or Register to add items to your cart and place orders.", "Guest Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvProducts.SelectedRows.Count == 0)
            { MessageBox.Show("Please select one or more products first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                int addedCount = 0;
                foreach (DataGridViewRow row in dgvProducts.SelectedRows)
                {
                    int productId = Convert.ToInt32(row.Cells["ID"].Value);
                    var product = ProductService.GetProductById(productId);

                    if (product != null && product.Stock > 0)
                    {
                        ShoppingCartService.AddToCart(product, 1);
                        addedCount++;
                    }
                }

                if (addedCount > 0)
                {
                    UpdateCartCount();
                    var result = MessageBox.Show($"✅ Added {addedCount} product(s) to your cart!\n\nWould you like to go to your cart now?", 
                        "Added to Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        BtnCart_Click(null, null);
                    }
                }
                else { MessageBox.Show("Selected products are out of stock."); }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnAddToWishlist_Click(object sender, EventArgs e)
        {
            if (currentUser.UserId == 0) { MessageBox.Show("Please login to use the Wishlist.", "Guest Mode"); return; }
            if (dgvProducts.SelectedRows.Count == 0) return;

            try
            {
                int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ID"].Value);
                if (WishlistService.AddToWishlist(currentUser.UserId, productId))
                {
                    MessageBox.Show("✅ Added to Wishlist!", "Success");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnCart_Click(object sender, EventArgs e)
        {
            if (currentUser.UserId == 0)
            {
                MessageBox.Show("Please Login to view your shopping cart.", "Guest Mode");
                return;
            }
            string currency = cmbCurrency.SelectedItem?.ToString() ?? "ETB";
            var cartForm = new FormCart(currentUser);
            cartForm.ShowDialog(this);
            UpdateCartCount();
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            if (currentUser.UserId == 0)
            {
                MessageBox.Show("Please Login to view your order history.", "Guest Mode");
                return;
            }
            string currency = cmbCurrency.SelectedItem?.ToString() ?? "ETB";
            var ordersForm = new FormOrderHistory(currentUser, currency);
            ordersForm.ShowDialog(this);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShoppingCartService.ClearCart();
                this.Close();
            }
        }
    }
}
