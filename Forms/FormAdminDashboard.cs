using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Data;
using E_commerance_System.Models;
using E_commerance_System.Services;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// ============================================================
// FORM: ADMIN DASHBOARD — Central Admin Control Panel
// ============================================================
// Features: Product CRUD, Order management, Sales reports
// Uses TabControl for multi-section layout.
// Demonstrates event-driven CRUD with DataGridView.
// ============================================================

namespace E_commerance_System.Forms
{
    public partial class FormAdminDashboard : Form
    {
        private Admin currentAdmin;
        private string[] currentProofPaths;
        private int currentProofIndex = 0;
        private Button btnPrevProof, btnNextProof;
        private DataGridView dgvInventory, dgvComplaints;
        private Button btnNavDashboard, btnNavProducts, btnNavOrders, btnNavCustomers, btnNavInventory, btnNavReports, btnNavNotifications, btnNavComplaints;
        private TextBox txtCompSearch, txtCompDetail, txtAdminReply;
        private Label lblNotifBadge;
        private FlowLayoutPanel flpStats;
        private Label lblDeliveredOrders, lblCancelledOrders, lblActiveUsers, lblPendingComplaints;
        
        // Dynamic Action Components
        private Button btnAddProduct, btnEditProduct, btnDeleteProduct, btnAddCategory, btnToggleProduct, btnRefreshProducts;
        private Button btnUpdateStatus, btnApproveOrder, btnRejectOrder, btnRefreshOrders;
        private Button btnRefreshCustomers, btnAddStock;
        private ComboBox cmbReportPeriod, cmbProdCategory;
        private TextBox txtProdSearch;
        private Button btnRefreshReport, btnExportCSV;
        private TextBox txtAnnounceTitle, txtAnnounceContent;
        private Button btnPostAnnouncement, btnRefreshInv;

        public FormAdminDashboard(Admin admin)
        {
            currentAdmin = admin;
            InitializeComponent();
            SetupDashboardUI();
            LoadDashboardStats();
            LoadAllData();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                this.Close();
        }

        private Panel pnlMainContent;

        private void SetupDashboardUI()
        {
            this.Controls.Clear(); // CRITICAL: Clear designer controls first
            this.Text = "⚙️ Admin Dashboard  E-Commerce";
            this.Size = new Size(1300, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 250);


            // --- 2. HEADER (Dock Top Second) ---
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 135 };
            
            var pnlRow1 = new Panel { Dock = DockStyle.Top, Height = 75, BackColor = Color.Transparent };
            
            var lblBrand = new Label { 
                Text = "🛒 ADMIN SPEED  ", ForeColor = Color.FromArgb(0, 188, 212), 
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(20, 20), AutoSize = true, BackColor = Color.Transparent
            };
            
            var btnLogout = new Button {
                Text = "🚪 Logout",
                Location = new Point(1150, 20),
                Size = new Size(110, 40),
                BackColor = Color.FromArgb(211, 47, 47),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;
            
            pnlRow1.Controls.Add(lblBrand);
            pnlRow1.Controls.Add(btnLogout);

            btnNavDashboard = CreateSidebarButton("📊 Dashboard", 0);
            btnNavProducts = CreateSidebarButton("🏷️ Products", 1);
            btnNavOrders = CreateSidebarButton("📦 Order Mgmt", 2);
            btnNavCustomers = CreateSidebarButton("👥 Customers", 3);
            btnNavInventory = CreateSidebarButton("📦 Inventory", 4);
            btnNavReports = CreateSidebarButton("📈 Reports", 5);
            btnNavComplaints = CreateSidebarButton("📋 User Reports", 6);
            btnNavNotifications = CreateSidebarButton("📢 News Mgmt", 7);

            var pnlTopNav = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(45, 45, 48) };
            pnlTopNav.Controls.AddRange(new Control[] { btnNavNotifications, btnNavComplaints, btnNavReports, btnNavInventory, btnNavCustomers, btnNavOrders, btnNavProducts, btnNavDashboard });
            
            lblNotifBadge = new Label {
                Text = "0", BackColor = Color.Red, ForeColor = Color.White,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                Size = new Size(20, 20), Location = new Point(145, 5),
                TextAlign = ContentAlignment.MiddleCenter, Visible = false,
                Enabled = false // Allow clicks to pass through to button
            };
            btnNavComplaints.Controls.Add(lblNotifBadge);
            
            pnlHeader.Controls.AddRange(new Control[] { pnlTopNav, pnlRow1 });
            pnlHeader.Paint += (s, e) => {
                using (var b = new LinearGradientBrush(pnlHeader.ClientRectangle, Color.FromArgb(26, 35, 126), Color.FromArgb(63, 81, 181), 45F))
                    e.Graphics.FillRectangle(b, pnlHeader.ClientRectangle);
            };

            lblAdminWelcome = new Label { 
                Text = $"🛡️ Welcome, {currentAdmin.FullName}", 
                ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 13F),
                Location = new Point(25, 25), AutoSize = true, BackColor = Color.Transparent
            };

            btnLogout = new Button { 
                Text = "Logout", Location = new Point(1160, 20), Size = new Size(80, 38),
                FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(198, 40, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;

            cmbCurrency = new ComboBox { Location = new Point(1060, 25), Size = new Size(90, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(30, 30, 35), ForeColor = Color.White };
            CurrencyService.SyncCurrencySelector(cmbCurrency, () => {
                LoadDashboardStats();
                LoadAllData();
            });

            var btnAddCurrency = new Button {
                Text = "➕ Currency", Location = new Point(930, 20), Size = new Size(120, 38),
                FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(46, 125, 50),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnAddCurrency.FlatAppearance.BorderSize = 0;
            btnAddCurrency.Click += BtnAddCurrency_Click;

            var btnProfile = new Button { 
                Text = "Profile", Location = new Point(840, 20), Size = new Size(80, 38),
                FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = Color.FromArgb(0, 150, 136),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Click += (s, e) => new FormAdminProfile(currentAdmin).ShowDialog();

            pnlRow1.Controls.AddRange(new Control[] { lblAdminWelcome, btnLogout, cmbCurrency, btnAddCurrency, btnProfile });

            // === 3. MAIN CONTENT (Dock Fill Last) ===
            pnlMainContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(30) };
            

            tabMain = new TabControl { 
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons, ItemSize = new Size(0, 1), // Hidden
                SizeMode = TabSizeMode.Fixed
            };

            SetupTabs();

            pnlMainContent.Controls.Add(tabMain);

            // Add panels to form in correct Z-order (Top first, then Fill)
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlMainContent);
            pnlMainContent.BringToFront();
            
            HighlightButton(btnNavDashboard);
        }

        private Button CreateSidebarButton(string text, int index)
        {
            var btn = new Button {
                Text = text,
                Height = 60,
                Width = 180,
                Dock = DockStyle.Left,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(180, 180, 190),
                Font = new Font("Segoe UI Semibold", 10F),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 137, 123);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 50);
            
            btn.Click += (s, e) => {
                tabMain.SelectedIndex = index;
                HighlightButton(btn);
                if (index == 6) LoadComplaints(); // Auto-refresh when entering Alert Center
            };

            return btn;
        }

        private void HighlightButton(Button activeBtn)
        {
            if (activeBtn.Parent == null) return;
            foreach (Control c in activeBtn.Parent.Controls)
                if (c is Button b) {
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                }
            activeBtn.BackColor = Color.FromArgb(0, 137, 123);
            activeBtn.ForeColor = Color.White;
        }

        private void SetupTabs()
        {
            // --- Dashboard Overview Tab ---
            var tabOverview = new TabPage() { BackColor = Color.FromArgb(245, 245, 250), Padding = new Padding(0) };
            flpStats = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 280, Padding = new Padding(20), BackColor = Color.Transparent };
            
            lblTotalRevenue = CreateMetricLabel("ETB 0.00", "Total Revenue", Color.FromArgb(0, 150, 136), "💰");
            lblTotalOrders = CreateMetricLabel("0", "Total Orders", Color.FromArgb(26, 35, 126), "📦");
            lblTotalCustomers = CreateMetricLabel("0", "Customers", Color.FromArgb(63, 81, 181), "👥");
            lblTotalProducts = CreateMetricLabel("0", "Products", Color.FromArgb(0, 121, 107), "🏷️");
            lblDeliveredOrders = CreateMetricLabel("0", "Delivered", Color.FromArgb(46, 125, 50), "✅");
            lblCancelledOrders = CreateMetricLabel("0", "Cancelled", Color.FromArgb(198, 40, 40), "❌");
            lblActiveUsers = CreateMetricLabel("0", "Pending Orders", Color.FromArgb(255, 143, 0), "⏳");
            lblPendingComplaints = CreateMetricLabel("0", "User Reports", Color.FromArgb(198, 40, 40), "📋");

            flpStats.Controls.AddRange(new Control[] { 
                lblTotalRevenue.Parent, lblTotalOrders.Parent, lblTotalCustomers.Parent, 
                lblTotalProducts.Parent, lblDeliveredOrders.Parent, lblCancelledOrders.Parent, 
                lblActiveUsers.Parent, lblPendingComplaints.Parent 
            });

            var pnlWelcome = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            
            var splitDashboard = new SplitContainer { 
                Dock = DockStyle.Fill, Orientation = Orientation.Vertical, 
                SplitterDistance = 750, BorderStyle = BorderStyle.None,
                IsSplitterFixed = true
            };
            pnlWelcome.Controls.Add(splitDashboard);

            // LEFT SIDE: Charts & Performance
            var pnlAnalytics = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            var lblWelcomeTitle = new Label { 
                Text = "📈 Sales Performance & Analytics", 
                Font = new Font("Segoe UI Semibold", 14F), 
                ForeColor = Color.FromArgb(40, 40, 50), 
                Location = new Point(20, 15), AutoSize = true 
            };
            
            var pnlChartContainer = new Panel { 
                Location = new Point(20, 60), 
                Size = new Size(710, 420), 
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            
            var pbHero = new PictureBox {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Default
            };
            try {
                string heroPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "dashboard_hero.png");
                if (File.Exists(heroPath)) pbHero.Image = Image.FromFile(heroPath);
                else {
                    // Fallback to absolute path for now if bin/Debug doesn't have it yet
                    string absPath = @"c:\Users\User\source\Assginments\E-commerance System\Resources\dashboard_hero.png";
                    if (File.Exists(absPath)) pbHero.Image = Image.FromFile(absPath);
                }
            } catch { }

            var pnlOverlay = new Panel {
                Dock = DockStyle.Bottom, Height = 80, 
                BackColor = Color.FromArgb(180, 0, 0, 0) // Semi-transparent black
            };
            var lblOverlay = new Label {
                Text = "⚡ SYSTEM PERFORMANCE: OPTIMIZED\nGlobal store metrics are being tracked in real-time.",
                ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 15), AutoSize = true, BackColor = Color.Transparent
            };
            pnlOverlay.Controls.Add(lblOverlay);
            pbHero.Controls.Add(pnlOverlay);
            
            pnlChartContainer.Controls.Add(pbHero);

            pnlAnalytics.Controls.AddRange(new Control[] { lblWelcomeTitle, pnlChartContainer });
            splitDashboard.Panel1.Controls.Add(pnlAnalytics);

            // RIGHT SIDE: Recent Activity
            var pnlActivity = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), BackColor = Color.FromArgb(250, 250, 252) };
            var lblActTitle = new Label { Text = "🔔 Recent Activity", Font = new Font("Segoe UI Semibold", 12F), Location = new Point(15, 15), AutoSize = true };
            
            var flpActivity = new FlowLayoutPanel { 
                Location = new Point(15, 55), Size = new Size(300, 500), 
                FlowDirection = FlowDirection.TopDown, AutoScroll = true 
            };
            
            void AddActivity(string icon, string text, string time) {
                var item = new Panel { Size = new Size(270, 60), Margin = new Padding(0, 0, 0, 10), BackColor = Color.White };
                item.Controls.Add(new Label { Text = icon, Font = new Font("Segoe UI", 12F), Location = new Point(10, 15), AutoSize = true });
                item.Controls.Add(new Label { Text = text, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(45, 10), Size = new Size(220, 20) });
                item.Controls.Add(new Label { Text = time, Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray, Location = new Point(45, 32), AutoSize = true });
                flpActivity.Controls.Add(item);
            }

            AddActivity("📦", "New Order #1204 received", "2 mins ago");
            AddActivity("👤", "New user 'Sarah_99' registered", "15 mins ago");
            AddActivity("⚠️", "Low stock alert: Gaming Mouse", "1 hour ago");
            AddActivity("✅", "Order #1198 delivered", "3 hours ago");
            AddActivity("💬", "New support message from Alex", "5 hours ago");

            pnlActivity.Controls.AddRange(new Control[] { lblActTitle, flpActivity });
            splitDashboard.Panel2.Controls.Add(pnlActivity);

            tabOverview.Controls.Add(flpStats); // Top
            tabOverview.Controls.Add(pnlWelcome); // Fill
            tabMain.TabPages.Add(tabOverview);

            // --- Products Tab ---
            var tabProducts = new TabPage() { BackColor = Color.White, Padding = new Padding(0) };
            dgvProducts = CreateGrid(new Point(0, 0), new Size(980, 480));
            dgvProducts.Dock = DockStyle.Fill;
            var pnlProdButtons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 70, Padding = new Padding(10), BackColor = Color.FromArgb(245, 245, 250) };
            
            btnAddProduct = CreateActionButton("➕ Add Product", Color.FromArgb(46, 125, 50));
            btnAddProduct.Click += BtnAddProduct_Click;
            
            btnAddCategory = CreateActionButton("📁 New Category", Color.FromArgb(0, 121, 107));
            btnAddCategory.Click += (s, e) => new FormCategoryAdd().ShowDialog();
            
            btnEditProduct = CreateActionButton("✏️ Edit Product", Color.FromArgb(25, 118, 210));
            btnEditProduct.Click += BtnEditProduct_Click;

            btnDeleteProduct = CreateActionButton("🗑️ Delete Product", Color.FromArgb(198, 40, 40));
            btnDeleteProduct.Click += BtnDeleteProduct_Click;
            
            btnToggleProduct = CreateActionButton("🔄 Toggle Visibility", Color.FromArgb(117, 117, 117));
            btnToggleProduct.Click += BtnToggleProduct_Click;
            

            btnRefreshProducts = CreateActionButton("🔄 Refresh", Color.FromArgb(96, 125, 139));
            btnRefreshProducts.Click += (s, e) => LoadProducts();

            pnlProdButtons.Controls.AddRange(new Control[] { btnAddProduct, btnAddCategory, btnEditProduct, btnDeleteProduct, btnToggleProduct, btnRefreshProducts });
            
            // --- Search Panel for Products ---
            var pnlProdSearch = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(240, 242, 248), Padding = new Padding(10) };
            var lblCat = new Label { Text = "Category:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            cmbProdCategory = new ComboBox { Location = new Point(90, 18), Size = new Size(180, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            LoadProductCategories();
            cmbProdCategory.SelectedIndexChanged += (s, e) => FilterAdminProducts();

            var lblSearch = new Label { Text = "Search:", Location = new Point(300, 20), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtProdSearch = new TextBox { Location = new Point(360, 18), Size = new Size(250, 25) };
            
            var btnDoSearch = CreateActionButton("🔍 Search", Color.FromArgb(0, 121, 107));
            btnDoSearch.Location = new Point(620, 14);
            btnDoSearch.Size = new Size(120, 32);
            btnDoSearch.Click += (s, e) => FilterAdminProducts();

            pnlProdSearch.Controls.AddRange(new Control[] { lblCat, cmbProdCategory, lblSearch, txtProdSearch, btnDoSearch });

            tabProducts.Controls.Add(dgvProducts);
            tabProducts.Controls.Add(pnlProdSearch);
            tabProducts.Controls.Add(pnlProdButtons);
            tabMain.TabPages.Add(tabProducts);

            // --- Orders Tab ---
            var tabOrders = new TabPage() { BackColor = Color.White, AutoScroll = true, Padding = new Padding(0) };
            dgvOrders = CreateGrid(new Point(0, 0), new Size(980, 180));
            dgvOrders.Dock = DockStyle.Top;
            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;
            
            var pnlOrderControls = new Panel { Dock = DockStyle.Top, Height = 550, BackColor = Color.FromArgb(250, 250, 252), Padding = new Padding(20), AutoScroll = true };
            
            var lblStatus = new Label { Text = "Set Order Status:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            cmbOrderStatus = new ComboBox { Location = new Point(160, 18), Size = new Size(150, 28), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbOrderStatus.Items.AddRange(new object[] { "Pending", "Processing", "Delivered", "Cancelled" });
            
            btnUpdateStatus = CreateActionButton("Update", Color.FromArgb(25, 118, 210));
            btnUpdateStatus.Location = new Point(320, 14);
            btnUpdateStatus.Size = new Size(100, 35);
            btnUpdateStatus.Click += BtnUpdateStatus_Click;

            btnApproveOrder = CreateActionButton("✅ Approve", Color.FromArgb(46, 125, 50));
            btnApproveOrder.Location = new Point(430, 14);
            btnApproveOrder.Size = new Size(120, 35);
            btnApproveOrder.Click += (s, e) => HandleOrderDecision(true);

            btnRejectOrder = CreateActionButton("❌ Reject", Color.FromArgb(198, 40, 40));
            btnRejectOrder.Location = new Point(560, 14);
            btnRejectOrder.Size = new Size(120, 35);
            btnRejectOrder.Click += (s, e) => HandleOrderDecision(false);

            btnRefreshOrders = CreateActionButton("🔄 Sync", Color.FromArgb(96, 125, 139));
            btnRefreshOrders.Location = new Point(690, 14);
            btnRefreshOrders.Size = new Size(100, 35);
            btnRefreshOrders.Click += (s, e) => LoadOrders();

            pbPaymentProof = new PictureBox { Location = new Point(20, 70), Size = new Size(400, 250), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, Cursor = Cursors.Hand };
            pbPaymentProof.Click += (s, e) => ZoomImage();
            lblActualAddress = new Label { Location = new Point(440, 70), Size = new Size(500, 250), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15), BackColor = Color.White, Font = new Font("Consolas", 10F) };
            lblProofHeader = new Label { Text = "Payment Evidence", Location = new Point(20, 50), AutoSize = true };
            
            btnPrevProof = new Button { Text = "◀", Location = new Point(20, 330), Size = new Size(50, 30), FlatStyle = FlatStyle.Flat, Visible = false };
            btnNextProof = new Button { Text = "▶", Location = new Point(370, 330), Size = new Size(50, 30), FlatStyle = FlatStyle.Flat, Visible = false };
            btnPrevProof.Click += (s, e) => { currentProofIndex--; ShowCurrentImage(); };
            btnNextProof.Click += (s, e) => { currentProofIndex++; ShowCurrentImage(); };

            // Use SplitContainer for better layout control in Orders
            var splitOrders = new SplitContainer { 
                Dock = DockStyle.Fill, 
                Orientation = Orientation.Horizontal, 
                SplitterDistance = 250,
                BorderStyle = BorderStyle.None
            };
            
            pnlOrderControls.Controls.AddRange(new Control[] { lblStatus, cmbOrderStatus, btnUpdateStatus, btnApproveOrder, btnRejectOrder, btnRefreshOrders, pbPaymentProof, lblActualAddress, btnPrevProof, btnNextProof, lblProofHeader });
            
            splitOrders.Panel1.Controls.Add(dgvOrders);
            splitOrders.Panel2.Controls.Add(pnlOrderControls);
            dgvOrders.Dock = DockStyle.Fill;
            pnlOrderControls.Dock = DockStyle.Fill;

            tabOrders.Controls.Add(splitOrders);
            tabMain.TabPages.Add(tabOrders);

            // --- Customers Tab ---
            var tabCustomers = new TabPage() { BackColor = Color.White, Padding = new Padding(0) };
            dgvCustomers = CreateGrid(new Point(0, 0), new Size(980, 520));
            dgvCustomers.Dock = DockStyle.Fill;
            btnRefreshCustomers = CreateActionButton("🔄 Refresh User List", Color.FromArgb(96, 125, 139));
            var pnlCustBtns = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10), BackColor = Color.FromArgb(245, 245, 250) };
            btnRefreshCustomers.Location = new Point(10, 10);
            
            var btnToggleUser = CreateActionButton("👤 Toggle Active/Deactive", Color.FromArgb(211, 47, 47));
            btnToggleUser.Location = new Point(220, 10);
            btnToggleUser.Click += (s, e) => {
                if (dgvCustomers.SelectedRows.Count == 0) { MessageBox.Show("Please select a user."); return; }
                int userId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["User ID"].Value);
                string status = dgvCustomers.SelectedRows[0].Cells["Status"].Value.ToString();
                bool activate = status == "Inactive";
                if (UserService.ActivateDeactivateUser(userId, activate)) {
                    MessageBox.Show($"✅ User successfully {(activate ? "Activated" : "Deactivated")}!");
                    LoadCustomers();
                    LoadDashboardStats();
                }
            };
            pnlCustBtns.Controls.AddRange(new Control[] { btnRefreshCustomers, btnToggleUser });
            tabCustomers.Controls.Add(pnlCustBtns); // Bottom
            tabCustomers.Controls.Add(dgvCustomers); // Fill
            tabMain.TabPages.Add(tabCustomers);

            // --- Inventory Tab ---
            var tabInventory = new TabPage() { BackColor = Color.White, Padding = new Padding(0) };

            var pnlInvControls = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(240, 242, 248) };
            btnRefreshInv = CreateActionButton("🔄 Sync Inventory", Color.FromArgb(96, 125, 139));
            btnRefreshInv.Location = new Point(20, 12); btnRefreshInv.Size = new Size(180, 35);
            btnRefreshInv.Click += (s, e) => { 
                LoadInventory(); 
                MessageBox.Show("🔄 Inventory data synchronized with database!", "Sync Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            };

            btnAddStock = CreateActionButton("📦 Add Stock", Color.FromArgb(0, 150, 136));
            btnAddStock.Location = new Point(210, 12); btnAddStock.Size = new Size(180, 35);
            btnAddStock.Click += (s, e) => {
                if (dgvInventory.SelectedRows.Count == 0) { MessageBox.Show("Select a product to restock."); return; }
                int id = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["Product ID"].Value);
                string input = ShowInputDialog("Restock", "Enter additional stock quantity:");
                if (int.TryParse(input, out int added)) {
                    if (ProductService.UpdateStock(id, added)) { MessageBox.Show("✅ Stock updated!"); LoadInventory(); }
                }
            };
            pnlInvControls.Controls.AddRange(new Control[] { btnRefreshInv, btnAddStock });

            dgvInventory = CreateGrid(new Point(0, 0), new Size(980, 520));
            dgvInventory.Dock = DockStyle.Fill;

            tabInventory.Controls.Add(dgvInventory);
            tabInventory.Controls.Add(pnlInvControls);
            tabMain.TabPages.Add(tabInventory);

            // --- Reports Tab ---
            var tabReports = new TabPage() { BackColor = Color.White, Padding = new Padding(0) };
            dgvSalesReport = CreateGrid(new Point(0, 0), new Size(980, 520));
            dgvSalesReport.Dock = DockStyle.Fill;
            
            var pnlRepTop = new Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(10), BackColor = Color.FromArgb(245, 245, 250) };
            var lblPeriod = new Label { Text = "📅 Report Period:", Location = new Point(15, 20), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            cmbReportPeriod = new ComboBox { 
                Location = new Point(140, 18), Size = new Size(180, 28), 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbReportPeriod.Items.AddRange(new object[] { "Daily Sales", "Weekly Report", "Monthly Overview", "Annual Analysis" });
            cmbReportPeriod.SelectedIndex = 2; // Monthly default
            
            btnRefreshReport = CreateActionButton("📈 Generate Sales Report", Color.FromArgb(0, 150, 136));
            btnRefreshReport.Location = new Point(340, 14);
            btnRefreshReport.Size = new Size(160, 35);
            btnRefreshReport.Click += (s, e) => LoadSalesReport();

            btnExportCSV = CreateActionButton("📥 Export to CSV", Color.FromArgb(63, 81, 181));
            btnExportCSV.Location = new Point(510, 14);
            btnExportCSV.Size = new Size(160, 35);
            btnExportCSV.Click += (s, e) => ExportToCSV();

            pnlRepTop.Controls.AddRange(new Control[] { lblPeriod, cmbReportPeriod, btnRefreshReport, btnExportCSV });
            // Bottom Summary Panel
            var pnlRepBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(240, 242, 248), Padding = new Padding(20, 10, 20, 10) };
            lblReportSummary = new Label { 
                Text = "Select a period and click Generate to see statistics.", 
                Font = new Font("Segoe UI", 11F, FontStyle.Bold), 
                ForeColor = Color.FromArgb(26, 35, 126), 
                AutoSize = true, 
                Dock = DockStyle.Left 
            };
            pnlRepBottom.Controls.Add(lblReportSummary);

            tabReports.Controls.Add(dgvSalesReport); // Fill
            tabReports.Controls.Add(pnlRepTop); // Top
            tabReports.Controls.Add(pnlRepBottom); // Bottom
            tabMain.TabPages.Add(tabReports);

            // --- Alert Center (Complaints) Tab ---
            var tabComplaints = new TabPage("User Reports") { BackColor = Color.White };
            dgvComplaints = CreateGrid(new Point(0, 0), new Size(980, 300));
            dgvComplaints.Dock = DockStyle.Fill;
            
            // Top Filter Panel
            var pnlCompTop = new Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(10), BackColor = Color.FromArgb(240, 242, 248) };
            var lblSearchComp = new Label { Text = "🔍 Filter Alerts:", Location = new Point(15, 20), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtCompSearch = new TextBox { Location = new Point(120, 18), Size = new Size(300, 25), Font = new Font("Segoe UI", 10F) };
            var btnSearchComp = CreateActionButton("Filter", Color.FromArgb(0, 121, 107));
            btnSearchComp.Location = new Point(430, 14);
            btnSearchComp.Size = new Size(100, 32);
            btnSearchComp.Click += (s, e) => LoadComplaints(txtCompSearch.Text.Trim());
            pnlCompTop.Controls.AddRange(new Control[] { lblSearchComp, txtCompSearch, btnSearchComp });
            // Bottom Detail Panel
            var pnlCompBottom = new Panel { Dock = DockStyle.Bottom, Height = 250, Padding = new Padding(10), BackColor = Color.FromArgb(245, 245, 250) };
            
            var pnlCompSplit = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            pnlCompSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlCompSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            
            var pnlUserMsg = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var lblDetailTitle = new Label { Text = "📄 User Message:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtCompDetail = new TextBox { 
                Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, 
                ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 10F),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle
            };
            pnlUserMsg.Controls.Add(txtCompDetail);
            pnlUserMsg.Controls.Add(lblDetailTitle);

            var pnlAdminReply = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var lblReplyTitle = new Label { Text = "✍️ Your Response / Answer:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtAdminReply = new TextBox { 
                Dock = DockStyle.Fill, Multiline = true, 
                ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 10F),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle
            };
            pnlAdminReply.Controls.Add(txtAdminReply);
            pnlAdminReply.Controls.Add(lblReplyTitle);

            pnlCompSplit.Controls.Add(pnlUserMsg, 0, 0);
            pnlCompSplit.Controls.Add(pnlAdminReply, 1, 0);

            var pnlCompActions = new Panel { Dock = DockStyle.Right, Width = 220, Padding = new Padding(10) };
            
            var btnSendReply = CreateActionButton("💬 Send Answer", Color.FromArgb(25, 118, 210));
            btnSendReply.Width = 200;
            btnSendReply.Click += (s, e) => HandleComplaintAction("Answered");

            var btnResolve = CreateActionButton("✅ Resolve", Color.FromArgb(46, 125, 50));
            btnResolve.Width = 200;
            btnResolve.Location = new Point(10, 60);
            btnResolve.Click += (s, e) => HandleComplaintAction("Resolved");
            
            var btnDismiss = CreateActionButton("🗑️ Delete", Color.FromArgb(198, 40, 40));
            btnDismiss.Width = 200;
            btnDismiss.Location = new Point(10, 110);
            btnDismiss.Click += (s, e) => HandleComplaintAction("Deleted");

            pnlCompActions.Controls.AddRange(new Control[] { btnSendReply, btnResolve, btnDismiss });
            pnlCompBottom.Controls.AddRange(new Control[] { pnlCompSplit, pnlCompActions });

            dgvComplaints.SelectionChanged += (s, e) => {
                if (dgvComplaints.SelectedRows.Count > 0)
                {
                    txtCompDetail.Text = dgvComplaints.SelectedRows[0].Cells["Detailed Message"].Value.ToString();
                    txtAdminReply.Clear();
                }
                else
                {
                    txtCompDetail.Clear();
                    txtAdminReply.Clear();
                }
            };

            tabComplaints.Controls.Add(dgvComplaints);
            tabComplaints.Controls.Add(pnlCompTop);
            tabComplaints.Controls.Add(pnlCompBottom);
            tabMain.TabPages.Add(tabComplaints);

            // --- News & Announcements Tab ---
            var tabNotif = new TabPage("News & Announcements") { BackColor = Color.White };
            var pnlAnnounce = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30), BackColor = Color.White };
            
            var lblATitle = new Label { Text = "Announcement Title:", Location = new Point(30, 20), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            txtAnnounceTitle = new TextBox { Location = new Point(30, 50), Width = 500, Font = new Font("Segoe UI", 11F) };
            
            var lblACont = new Label { Text = "Message Content:", Location = new Point(30, 90), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            txtAnnounceContent = new TextBox { Multiline = true, Location = new Point(30, 120), Size = new Size(500, 150), Font = new Font("Segoe UI", 10F) };
            
            btnPostAnnouncement = CreateActionButton("📢 Post New Announcement", Color.FromArgb(25, 118, 210));
            btnPostAnnouncement.Location = new Point(30, 290); btnPostAnnouncement.Size = new Size(220, 40);
            btnPostAnnouncement.Click += BtnPostAnnouncement_Click;

            pnlAnnounce.Controls.AddRange(new Control[] { lblATitle, txtAnnounceTitle, lblACont, txtAnnounceContent, btnPostAnnouncement });
            tabNotif.Controls.Add(pnlAnnounce);
            tabMain.TabPages.Add(tabNotif);
        }

        // === GALLERY LOGIC ===
        private string GetProjectRootDirectory()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            while (!string.IsNullOrEmpty(dir))
            {
                if (System.IO.Directory.GetFiles(dir, "*.csproj").Length > 0)
                {
                    return dir;
                }
                dir = System.IO.Path.GetDirectoryName(dir);
            }
            return @"c:\Users\User\source\Assginments\E-commerance System";
        }

        private void ShowCurrentImage()
        {
            if (currentProofPaths == null || currentProofPaths.Length == 0) return;
            
            if (currentProofIndex < 0) currentProofIndex = currentProofPaths.Length - 1;
            if (currentProofIndex >= currentProofPaths.Length) currentProofIndex = 0;

            string path = currentProofPaths[currentProofIndex].Trim();
            string fullPath = System.IO.Path.IsPathRooted(path) ? path : System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Proofs", path);

            if (!System.IO.File.Exists(fullPath))
            {
                string projectRoot = GetProjectRootDirectory();
                fullPath = System.IO.Path.Combine(projectRoot, "Proofs", path);
            }

            if (System.IO.File.Exists(fullPath))
            {
                try {
                    if (pbPaymentProof.Image != null) pbPaymentProof.Image.Dispose();
                    using (var img = Image.FromFile(fullPath))
                    {
                        pbPaymentProof.Image = new Bitmap(img);
                    }
                    lblProofHeader.Text = $"🖼️ Evidence {currentProofIndex + 1} of {currentProofPaths.Length}";
                    btnPrevProof.Visible = btnNextProof.Visible = currentProofPaths.Length > 1;
                }
                catch { lblProofHeader.Text = "🖼️ Error loading image"; }
            }
            else { lblProofHeader.Text = "🖼️ Image file not found"; }
        }

        private void ZoomImage()
        {
            if (pbPaymentProof.Image != null)
            {
                Form f = new Form { Text = "Payment Evidence Full View", Size = new Size(900, 700), StartPosition = FormStartPosition.CenterParent };
                f.Controls.Add(new PictureBox { Image = pbPaymentProof.Image, Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom });
                f.ShowDialog();
            }
        }

        // === UI Helpers ===
        private Label CreateCard(Panel parent, string title, string value, int x, Color color)
        {
            var card = new Panel { Location = new Point(x, 10), Size = new Size(280, 70), BackColor = color };
            card.Paint += (s, e) => { using (var b = new LinearGradientBrush(card.ClientRectangle, color, ControlPaint.Dark(color, 0.15f), LinearGradientMode.Vertical)) e.Graphics.FillRectangle(b, card.ClientRectangle); };
            var lblT = new Label { Text = title, Location = new Point(15, 8), AutoSize = true, ForeColor = Color.FromArgb(220, 220, 240), Font = new Font("Segoe UI", 9F) };
            var lblV = new Label { Text = value, Location = new Point(15, 32), AutoSize = true, ForeColor = Color.White, Font = new Font("Segoe UI", 18F, FontStyle.Bold) };
            card.Controls.AddRange(new Control[] { lblT, lblV });
            parent.Controls.Add(card);
            return lblV;
        }

        private DataGridView CreateGrid(Point loc, Size sz)
        {
            var dgv = new DataGridView
            {
                Location = loc, Size = sz, ReadOnly = true, AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None, 
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                GridColor = Color.FromArgb(224, 224, 224),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(245, 248, 255) },
                DefaultCellStyle = new DataGridViewCellStyle { SelectionBackColor = Color.FromArgb(230, 240, 255), SelectionForeColor = PrimaryColor, Padding = new Padding(5, 0, 5, 0) },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { 
                    BackColor = PrimaryColor, 
                    ForeColor = Color.White, 
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                },
                EnableHeadersVisualStyles = false, 
                ColumnHeadersHeight = 45
            };
            dgv.RowTemplate.Height = 35;
            dgv.DataError += (s, e) => { e.ThrowException = false; };
            return dgv;
        }

        private Button CreateActionButton(string text, Color bg)
        {
            var btn = new Button {
                Text = text, FlatStyle = FlatStyle.Flat, ForeColor = Color.White,
                BackColor = bg, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Height = 42, Cursor = Cursors.Hand, AutoSize = true,
                Padding = new Padding(15, 0, 15, 0),
                MinimumSize = new Size(120, 42)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // === DATA LOADING ===

        private object ExecuteScalar(SqlConnection conn, string sql)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                var result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : result;
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = ProductService.GetAllProductsAdmin();
                var dt = new DataTable();
                dt.Columns.Add("Product ID", typeof(int));
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Stock", typeof(int));
                dt.Columns.Add("Brand", typeof(string));
                dt.Columns.Add("Active", typeof(string));

                string currency = CurrencyService.CurrentCurrency;
                decimal rate = CurrencyService.GetRate(currency);
                string symbol = CurrencyService.GetSymbol(currency);

                foreach (var p in products) {
                    decimal displayPrice = rate != 0 ? p.Price / rate : p.Price;
                    dt.Rows.Add(p.ProductId, p.ProductCode, p.Name, p.CategoryName, displayPrice, p.Stock, p.Brand, p.IsActive ? "✅ Yes" : "❌ No");
                }
                dgvProducts.DataSource = dt;
                dgvProducts.ColumnHeadersVisible = true;
                if (dgvProducts.Columns.Contains("Product ID")) dgvProducts.Columns["Product ID"].Visible = false;
                if (dgvProducts.Columns.Contains("Price"))
                {
                    dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N2";
                    dgvProducts.Columns["Price"].HeaderText = $"Price ({symbol})";
                }
                LoadDashboardStats();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void LoadOrders()
        {
            try
            {
                var orders = OrderService.GetAllOrders();
                var dt = new DataTable();
                dt.Columns.Add("OrderId", typeof(int));
                dt.Columns.Add("Order #", typeof(string));
                dt.Columns.Add("Order Date", typeof(string));
                dt.Columns.Add("Current Status", typeof(string));
                dt.Columns.Add("Shipping Address", typeof(string));
                dt.Columns.Add("Customer Phone", typeof(string));
                dt.Columns.Add("Total Payable", typeof(decimal));
                dt.Columns.Add("Payment Method", typeof(string));
                dt.Columns.Add("Ref #", typeof(string));

                string currency = CurrencyService.CurrentCurrency;
                decimal rate = CurrencyService.GetRate(currency);
                string symbol = CurrencyService.GetSymbol(currency);

                foreach (var o in orders) {
                    decimal displayTotal = rate != 0 ? o.TotalAmount / rate : o.TotalAmount;
                    dt.Rows.Add(o.OrderId, o.OrderNumber, o.OrderDate.ToString("yyyy-MM-dd HH:mm"), o.Status, o.ShippingAddress, o.Phone, displayTotal, o.PaymentMethod, o.PaymentReference);
                }
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = dt;
                dgvOrders.ColumnHeadersVisible = true;
                if (dgvOrders.Columns.Contains("OrderId")) dgvOrders.Columns["OrderId"].Visible = false;
                
                if (dgvOrders.Columns.Contains("Total Payable"))
                {
                    dgvOrders.Columns["Total Payable"].DefaultCellStyle.Format = "N2";
                    dgvOrders.Columns["Total Payable"].HeaderText = $"Total ({symbol})";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void LoadSalesReport()
        {
            try
            {
                using (var conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string currency = CurrencyService.CurrentCurrency;
                    decimal rate = CurrencyService.GetRate(currency);
                    string symbol = CurrencyService.GetSymbol(currency);

                    string periodFilter = "";
                    string selectedPeriod = cmbReportPeriod.SelectedItem?.ToString() ?? "Monthly Overview";

                    if (selectedPeriod == "Daily Sales")
                        periodFilter = "WHERE O.OrderDate >= CAST(GETDATE() AS DATE)";
                    else if (selectedPeriod == "Weekly Report")
                        periodFilter = "WHERE O.OrderDate >= DATEADD(day, -7, GETDATE())";
                    else if (selectedPeriod == "Monthly Overview")
                        periodFilter = "WHERE O.OrderDate >= DATEADD(day, -30, GETDATE())";
                    else if (selectedPeriod == "Annual Analysis")
                        periodFilter = "WHERE O.OrderDate >= DATEADD(year, -1, GETDATE())";

                    string sql = $@"SELECT O.OrderDate, U.Username, C.CategoryName, OD.ProductName, OD.Quantity, OD.UnitPrice, OD.TotalPrice
                                   FROM Orders O
                                   JOIN OrderDetails OD ON O.OrderId = OD.OrderId
                                   JOIN Users U ON O.UserId = U.UserId
                                   JOIN Products P ON OD.ProductId = P.ProductId
                                   JOIN Categories C ON P.CategoryId = C.CategoryId
                                   {periodFilter}
                                   ORDER BY O.OrderDate DESC";
                    
                    var dt = new DataTable();
                    dt.Columns.Add("Order Date", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Category", typeof(string));
                    dt.Columns.Add("Product Name", typeof(string));
                    dt.Columns.Add("Qty", typeof(int));
                    dt.Columns.Add("Unit Price (ETB)", typeof(decimal));
                    dt.Columns.Add("Total Amount (ETB)", typeof(decimal));
                    
                    bool isETB = currency == "ETB";
                    if (!isETB)
                    {
                        dt.Columns.Add($"Unit Price ({currency})", typeof(decimal));
                        dt.Columns.Add($"Total Amount ({currency})", typeof(decimal));
                    }

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal uPrice = Convert.ToDecimal(reader["UnitPrice"]);
                            decimal tPrice = Convert.ToDecimal(reader["TotalPrice"]);
                            
                            var rowData = new System.Collections.Generic.List<object> {
                                Convert.ToDateTime(reader["OrderDate"]).ToString("yyyy-MM-dd HH:mm"), 
                                reader["Username"].ToString(),
                                reader["CategoryName"].ToString(),
                                reader["ProductName"].ToString(),
                                Convert.ToInt32(reader["Quantity"]), 
                                uPrice,
                                tPrice
                            };

                            if (!isETB)
                            {
                                decimal displayUPrice = rate != 0 ? uPrice / rate : uPrice;
                                decimal displayTPrice = rate != 0 ? tPrice / rate : tPrice;
                                rowData.Add(displayUPrice);
                                rowData.Add(displayTPrice);
                            }

                            dt.Rows.Add(rowData.ToArray());
                        }
                    }

                    dgvSalesReport.DataSource = dt;
                    dgvSalesReport.ColumnHeadersVisible = true;
                    if (dgvSalesReport.Columns.Contains("Unit Price (ETB)")) dgvSalesReport.Columns["Unit Price (ETB)"].DefaultCellStyle.Format = "N2";
                    if (dgvSalesReport.Columns.Contains("Total Amount (ETB)")) dgvSalesReport.Columns["Total Amount (ETB)"].DefaultCellStyle.Format = "N2";
                    
                    if (!isETB)
                    {
                        if (dgvSalesReport.Columns.Contains($"Unit Price ({currency})")) dgvSalesReport.Columns[$"Unit Price ({currency})"].DefaultCellStyle.Format = "N2";
                        if (dgvSalesReport.Columns.Contains($"Total Amount ({currency})")) dgvSalesReport.Columns[$"Total Amount ({currency})"].DefaultCellStyle.Format = "N2";
                    }
                    
                    string totalSql = $@"SELECT ISNULL(SUM(O.TotalAmount),0) FROM Orders O {periodFilter}";
                    string countSql = $@"SELECT COUNT(DISTINCT O.OrderId) FROM Orders O {periodFilter}";
                    
                    var totalRev = ExecuteScalar(conn, totalSql);
                    var totalOrd = ExecuteScalar(conn, countSql);
                    decimal displayTotalRev = rate != 0 ? Convert.ToDecimal(totalRev) / rate : Convert.ToDecimal(totalRev);
                    
                    lblReportSummary.Text = $"{selectedPeriod} — Orders: {totalOrd} | Revenue: {displayTotalRev:N2} {symbol}";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void LoadInventory()
        {
            try
            {
                using (var conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string sql = @"SELECT P.ProductId as [Product ID], P.ProductCode as [Code], P.Name as [Name], 
                                   C.CategoryName as [Category], P.Price as [Price], P.Stock as [Stock], 
                                   P.MinStockLevel as [Reorder Level]
                                   FROM Products P 
                                   LEFT JOIN Categories C ON P.CategoryId = C.CategoryId
                                   ORDER BY P.Stock ASC";
                    
                    var dt = new DataTable();
                    using (var da = new SqlDataAdapter(sql, conn))
                        da.Fill(dt);

                    if (dgvInventory == null) return;
                    dgvInventory.DataSource = null; // Clear first
                    dgvInventory.DataSource = dt;
                    dgvInventory.Refresh();
                    dgvInventory.ColumnHeadersVisible = true;
                    if (dgvInventory.Columns.Contains("Product ID")) dgvInventory.Columns["Product ID"].Visible = false;
                    
                    // Formatting
                    if (dgvInventory.Columns.Contains("Price"))
                    {
                        dgvInventory.Columns["Price"].DefaultCellStyle.Format = "N2";
                        dgvInventory.Columns["Price"].HeaderText = $"Price ({CurrencyService.GetSymbol(CurrencyService.CurrentCurrency)})";
                    }
                    
                    // Highlight low stock
                    foreach (DataGridViewRow row in dgvInventory.Rows)
                    {
                        if (row.Cells["Stock"].Value != DBNull.Value && row.Cells["Reorder Level"].Value != DBNull.Value)
                        {
                            int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                            int min = Convert.ToInt32(row.Cells["Reorder Level"].Value);
                            if (stock <= min)
                                row.DefaultCellStyle.BackColor = Color.MistyRose;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading inventory: " + ex.Message); }
        }

        private void LoadCustomers()
        {
            try
            {
                var users = UserService.GetAllUsers();
                var dt = new DataTable();
                dt.Columns.Add("User ID", typeof(int));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("Full Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Currency", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Joined Date", typeof(string));

                foreach (var u in users)
                    dt.Rows.Add(u.UserId, u.Username, $"{u.FirstName} {u.LastName}", u.Email, u.Phone, u.PreferredCurrency, u.IsActive ? "Active" : "Inactive", u.CreatedDate.ToString("yyyy-MM-dd"));

                dgvCustomers.DataSource = dt;
                dgvCustomers.ColumnHeadersVisible = true;
                if (dgvCustomers.Columns.Contains("User ID")) dgvCustomers.Columns["User ID"].Visible = false;
                LoadDashboardStats();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // === EVENT HANDLERS ===
        private void DgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;
            try
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
                var order = OrderService.GetOrderById(orderId);

                if (order != null)
                {
                    // --- Delivery & Contact Info ---
                    if (lblActualAddress != null)
                    {
                        string orderType = (order.OrderDetails != null && order.OrderDetails.Count > 1) 
                            ? $"📦 Multiple ({order.OrderDetails.Count} items)" 
                            : "📦 Single Product";

                        lblActualAddress.Text = $"📍 Address:\n{order.ShippingAddress}\n\n" +
                                                $"📞 Phone: {order.Phone}\n\n" +
                                                $"📋 Type: {orderType}";
                    }

                    // --- Gallery Initialization ---
                    if (!string.IsNullOrEmpty(order.PaymentProofImage))
                    {
                        currentProofPaths = order.PaymentProofImage.Split(';');
                        currentProofIndex = 0;
                        ShowCurrentImage();
                    }
                    else
                    {
                        pbPaymentProof.Image = null;
                        currentProofPaths = null;
                        lblProofHeader.Text = "🖼️ No Payment Image Attached";
                        btnPrevProof.Visible = btnNextProof.Visible = false;
                    }
                    if (cmbOrderStatus.Items.Contains(order.Status)) cmbOrderStatus.SelectedItem = order.Status;
                }
            }
            catch { }
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0 || cmbOrderStatus.SelectedItem == null) { MessageBox.Show("Select an order and status."); return; }
            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
            if (OrderService.UpdateOrderStatus(orderId, cmbOrderStatus.SelectedItem.ToString()))
            { MessageBox.Show("✅ Order status updated!"); LoadOrders(); LoadDashboardStats(); }
            else MessageBox.Show("Failed to update status.");
        }

        private void HandleOrderDecision(bool approve)
        {
            if (dgvOrders.SelectedRows.Count == 0) { MessageBox.Show("Select an order first."); return; }
            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
            string orderNum = dgvOrders.SelectedRows[0].Cells["Order #"].Value.ToString();
            
            string newStatus = approve ? "Processing" : "Cancelled";
            string action = approve ? "APPROVE" : "REJECT";
            
            if (MessageBox.Show($"Are you sure you want to {action} Order #{orderNum}?", "Confirm Decision", MessageBoxButtons.YesNo, approve ? MessageBoxIcon.Question : MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (OrderService.UpdateOrderStatus(orderId, newStatus))
                {
                    MessageBox.Show($"✅ Order #{orderNum} has been {newStatus.ToLower()}!");
                    LoadOrders();
                    LoadDashboardStats();
                }
                else MessageBox.Show("Failed to update order.");
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            var form = new FormProductManage(null);
            if (form.ShowDialog() == DialogResult.OK) LoadProducts();
        }

        private void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) { MessageBox.Show("Please select a product to delete."); return; }
            int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Product ID"].Value);
            string name = dgvProducts.SelectedRows[0].Cells["Name"].Value.ToString();

            var result = MessageBox.Show($"⚠️ ARE YOU SURE?\n\nThis will PERMANENTLY DELETE '{name}' from the database.\nThis action cannot be undone.", 
                "Confirm Permanent Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (ProductService.PermanentDeleteProduct(id))
                {
                    MessageBox.Show("✅ Product deleted permanently.");
                    LoadProducts();
                    LoadDashboardStats();
                }
                else
                {
                    // Fallback to soft delete
                    var softResult = MessageBox.Show($"❌ Cannot permanently delete '{name}' because it is linked to existing orders.\n\nWould you like to DEACTIVATE (hide) it instead?", 
                        "Delete Restricted", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (softResult == DialogResult.Yes)
                    {
                        if (ProductService.DeleteProduct(id))
                        {
                            MessageBox.Show("✅ Product has been deactivated and hidden from customers.");
                            LoadProducts();
                        }
                    }
                }
            }
        }
        
        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) { MessageBox.Show("Please select a product."); return; }
            int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Product ID"].Value);
            var product = ProductService.GetProductById(id);
            if (product != null) { var form = new FormProductManage(product); if (form.ShowDialog() == DialogResult.OK) LoadProducts(); }
        }

        private void BtnToggleProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) { MessageBox.Show("Select a product."); return; }
            string name = dgvProducts.SelectedRows[0].Cells["Name"].Value.ToString();
            int id = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Product ID"].Value);
            string currentStatus = dgvProducts.SelectedRows[0].Cells["Active"].Value.ToString();
            bool currentlyActive = currentStatus.Contains("Yes");

            string action = currentlyActive ? "Deactivate" : "Activate";
            if (MessageBox.Show($"{action} \"{name}\"?", "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ProductService.ActivateDeactivateProduct(id, !currentlyActive)) LoadProducts();
                else MessageBox.Show($"Failed to {action.ToLower()} product.");
            }
        }
        private void ShowNotificationCenter()
        {
            new FormNotifications().ShowDialog();
        }

        private Button CreateHeaderButton(string text, int x)
        {
            var btn = new Button { Text = text, Location = new Point(x, 12), Size = new Size(100, 35), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(40, 53, 147), ForeColor = Color.White, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex].Name == "Current Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Delivered")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvOrders.Font, FontStyle.Bold);
                }
                else if (status == "Cancelled")
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
                else if (status == "Processing")
                {
                    e.CellStyle.ForeColor = Color.DarkBlue;
                }
            }
        }
        private void ExportToCSV()
        {
            if (dgvSalesReport.DataSource == null || dgvSalesReport.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = $"Sales_Report_{DateTime.Now:yyyyMMdd_HHmm}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new System.Text.StringBuilder();

                        // Header
                        var headers = dgvSalesReport.Columns.Cast<DataGridViewColumn>();
                        sb.AppendLine(string.Join(",", headers.Select(column => $"\"{column.HeaderText}\"")));

                        // Rows
                        foreach (DataGridViewRow row in dgvSalesReport.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => {
                                    string val = cell.Value?.ToString() ?? "";
                                    return $"\"{val.Replace("\"", "\"\"")}\"";
                                });
                                sb.AppendLine(string.Join(",", cells));
                            }
                        }

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                        MessageBox.Show("✅ Report exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private Label CreateMetricLabel(string value, string title, Color color, string icon)
        {
            var pnl = new Panel { Size = new Size(185, 100), BackColor = Color.White, Margin = new Padding(10) };
            pnl.Paint += (s, e) => {
                using(var p = new Pen(color, 4)) e.Graphics.DrawLine(p, 0, 0, pnl.Width, 0);
            };

            var lblVal = new Label { Text = value, Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold), ForeColor = Color.FromArgb(40, 40, 50), Location = new Point(10, 30), AutoSize = true };
            var lblT = new Label { Text = title, Font = new Font("Segoe UI", 8.5F), ForeColor = Color.Gray, Location = new Point(10, 65), AutoSize = true };
            var lblI = new Label { Text = icon, Font = new Font("Segoe UI", 20F), Location = new Point(135, 25), AutoSize = true };

            pnl.Controls.AddRange(new Control[] { lblVal, lblT, lblI });
            return lblVal;
        }

        private void LoadDashboardStats()
        {
            try
            {
                string currency = CurrencyService.CurrentCurrency;
                decimal rate = CurrencyService.GetRate(currency);
                string symbol = CurrencyService.GetSymbol(currency);

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("GetDashboardStats", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal totalRevenue = reader.IsDBNull(reader.GetOrdinal("TotalRevenue")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalRevenue"));
                                int totalOrders = reader.IsDBNull(reader.GetOrdinal("TotalOrders")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalOrders"));
                                int totalCustomers = reader.IsDBNull(reader.GetOrdinal("TotalCustomers")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalCustomers"));
                                int totalProducts = reader.IsDBNull(reader.GetOrdinal("TotalProducts")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalProducts"));
                                int deliveredCount = reader.IsDBNull(reader.GetOrdinal("DeliveredOrders")) ? 0 : reader.GetInt32(reader.GetOrdinal("DeliveredOrders"));
                                int cancelledCount = reader.IsDBNull(reader.GetOrdinal("CancelledOrders")) ? 0 : reader.GetInt32(reader.GetOrdinal("CancelledOrders"));
                                int pendingOrders = reader.IsDBNull(reader.GetOrdinal("PendingOrders")) ? 0 : reader.GetInt32(reader.GetOrdinal("PendingOrders"));

                                int pendingComplaints = reader.IsDBNull(reader.GetOrdinal("PendingComplaints")) ? 0 : reader.GetInt32(reader.GetOrdinal("PendingComplaints"));

                                decimal displayRevenue = rate != 0 ? totalRevenue / rate : totalRevenue;
                                lblTotalRevenue.Text = $"{displayRevenue:N2} {symbol}";
                                lblTotalOrders.Text = totalOrders.ToString();
                                lblTotalCustomers.Text = totalCustomers.ToString();
                                lblTotalProducts.Text = totalProducts.ToString();
                                lblDeliveredOrders.Text = deliveredCount.ToString();
                                lblCancelledOrders.Text = cancelledCount.ToString();
                                lblActiveUsers.Text = pendingOrders.ToString();
                                lblPendingComplaints.Text = pendingComplaints.ToString();
                                
                                // Update Notification Badge
                                if (pendingComplaints > 0)
                                {
                                    lblNotifBadge.Text = pendingComplaints > 99 ? "99+" : pendingComplaints.ToString();
                                    lblNotifBadge.Visible = true;
                                    lblNotifBadge.BringToFront();
                                }
                                else
                                {
                                    lblNotifBadge.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                if (ex.Message.Contains("GetDashboardStats") || ex.Message.Contains("PendingComplaints"))
                {
                    // Self-healing: Re-create the SP if it's missing or outdated
                    try {
                        using (var conn = DatabaseHelper.GetConnection()) {
                            conn.Open();
                            string drop = "IF OBJECT_ID('GetDashboardStats', 'P') IS NOT NULL DROP PROCEDURE GetDashboardStats;";
                            using (var cmd = new SqlCommand(drop, conn)) cmd.ExecuteNonQuery();

                            string script = @"CREATE PROCEDURE GetDashboardStats AS BEGIN SELECT (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalCustomers, (SELECT COUNT(*) FROM Products WHERE IsActive = 1) AS TotalProducts, (SELECT COUNT(*) FROM Orders) AS TotalOrders, (SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS TotalRevenue, (SELECT COUNT(*) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS DeliveredOrders, (SELECT COUNT(*) FROM Orders WHERE Status = 'Cancelled') AS CancelledOrders, (SELECT COUNT(*) FROM Orders WHERE Status = 'Pending') AS PendingOrders, (SELECT COUNT(*) FROM Reviews WHERE IsApproved = 0) AS PendingReviews, (SELECT COUNT(*) FROM Complaints WHERE Status = 'Pending') AS PendingComplaints END";
                            using (var cmd = new SqlCommand(script, conn)) cmd.ExecuteNonQuery();
                        }
                        LoadDashboardStats(); // Retry once
                        return;
                    } catch { }
                }
                MessageBox.Show("Stats Error: " + ex.Message); 
            }
        }
        private void LoadProductCategories()
        {
            try
            {
                cmbProdCategory.Items.Clear();
                cmbProdCategory.Items.Add("All Categories");
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT CategoryName FROM Categories", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) cmbProdCategory.Items.Add(reader["CategoryName"].ToString());
                    }
                }
                cmbProdCategory.SelectedIndex = 0;
            }
            catch { }
        }

        private void FilterAdminProducts()
        {
            try
            {
                string category = cmbProdCategory.SelectedItem?.ToString() ?? "All Categories";
                string search = txtProdSearch.Text.Trim().ToLower();

                var products = ProductService.GetAllProductsAdmin();
                var filtered = products.Where(p => 
                    (category == "All Categories" || p.CategoryName == category) &&
                    (string.IsNullOrEmpty(search) || 
                     p.Name.ToLower().Contains(search) || 
                     p.ProductCode.ToLower().Contains(search) || 
                     (p.Brand?.ToLower().Contains(search) ?? false))
                ).ToList();

                var dt = new DataTable();
                dt.Columns.Add("Product ID", typeof(int));
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Stock", typeof(int));
                dt.Columns.Add("Brand", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("Size", typeof(string));
                dt.Columns.Add("Active", typeof(string));

                string currency = CurrencyService.CurrentCurrency;
                decimal rate = CurrencyService.GetRate(currency);
                string symbol = CurrencyService.GetSymbol(currency);

                foreach (var p in filtered) {
                    decimal displayPrice = rate != 0 ? p.Price / rate : p.Price;
                    dt.Rows.Add(p.ProductId, p.ProductCode, p.Name, p.CategoryName, displayPrice, p.Stock, p.Brand, p.Model, p.Color, p.Size, p.IsActive ? "✅ Yes" : "❌ No");
                }
                dgvProducts.DataSource = dt;
                dgvProducts.ColumnHeadersVisible = true;
                if (dgvProducts.Columns.Contains("Product ID")) dgvProducts.Columns["Product ID"].Visible = false;
                if (dgvProducts.Columns.Contains("Price"))
                {
                    dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N2";
                    dgvProducts.Columns["Price"].HeaderText = $"Price ({symbol})";
                    dgvProducts.Columns["Price"].Width = 100;
                }
                if (dgvProducts.Columns.Contains("Stock")) dgvProducts.Columns["Stock"].Width = 70;
                if (dgvProducts.Columns.Contains("Brand")) dgvProducts.Columns["Brand"].Width = 100;
                if (dgvProducts.Columns.Contains("Model")) dgvProducts.Columns["Model"].Width = 100;
                if (dgvProducts.Columns.Contains("Color")) dgvProducts.Columns["Color"].Width = 80;
                if (dgvProducts.Columns.Contains("Size")) dgvProducts.Columns["Size"].Width = 60;
                if (dgvProducts.Columns.Contains("Active")) dgvProducts.Columns["Active"].Width = 80;
            }
            catch (Exception ex) { MessageBox.Show("Filter Error: " + ex.Message); }
        }


        private void BtnPostAnnouncement_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnnounceTitle.Text) || string.IsNullOrEmpty(txtAnnounceContent.Text))
            {
                MessageBox.Show("Please enter title and content.");
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO Announcements (Title, Content, CreatedBy) VALUES (@title, @content, @adminId)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", txtAnnounceTitle.Text);
                        cmd.Parameters.AddWithValue("@content", txtAnnounceContent.Text);
                        cmd.Parameters.AddWithValue("@adminId", currentAdmin.AdminId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("📢 Announcement posted successfully!");
                txtAnnounceTitle.Clear(); txtAnnounceContent.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }


        private void LoadComplaints(string filter = "")
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string whereClause = string.IsNullOrEmpty(filter) ? "" : "WHERE U.Username LIKE @f OR C.Subject LIKE @f OR C.Message LIKE @f";
                    string sql = $@"SELECT C.ComplaintId as [Alert ID], ISNULL(U.Username, 'Unknown User') as [Customer], C.Subject as [Topic], C.Message as [Detailed Message], C.Status, C.CreatedDate as [Received Date] 
                                   FROM Complaints C LEFT JOIN Users U ON C.UserId = U.UserId 
                                   {whereClause} 
                                   ORDER BY C.CreatedDate DESC";
                    
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(filter)) cmd.Parameters.AddWithValue("@f", "%" + filter + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvComplaints.DataSource = dt;
                        dgvComplaints.ColumnHeadersVisible = true;
                        if (dgvComplaints.Columns.Contains("Alert ID")) dgvComplaints.Columns["Alert ID"].Visible = false;
                        // Ensure Message column is visible but has a reasonable width
                        if (dgvComplaints.Columns.Contains("Detailed Message")) {
                            dgvComplaints.Columns["Detailed Message"].Visible = true;
                            dgvComplaints.Columns["Detailed Message"].Width = 300;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading complaints: " + ex.Message); }
        }

        private void HandleComplaintAction(string action)
        {
            if (dgvComplaints.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvComplaints.SelectedRows[0].Cells["Alert ID"].Value);

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "";
                    if (action == "Answered")
                    {
                        if (string.IsNullOrWhiteSpace(txtAdminReply.Text)) { MessageBox.Show("Please type an answer first."); return; }
                        sql = "UPDATE Complaints SET AdminReply = @reply, ReplyDate = GETDATE(), Status = 'Answered' WHERE ComplaintId = @id";
                    }
                    else if (action == "Resolved")
                    {
                        sql = "UPDATE Complaints SET Status = 'Resolved' WHERE ComplaintId = @id";
                    }
                    else
                    {
                        sql = "DELETE FROM Complaints WHERE ComplaintId = @id";
                    }
                    
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        if (action == "Answered") cmd.Parameters.AddWithValue("@reply", txtAdminReply.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"✅ Alert successfully {action}!");
                LoadComplaints();
                LoadDashboardStats();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private string ShowInputDialog(string title, string prompt)
        {
            Form promptForm = new Form() {
                Width = 400, Height = 180, FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title, StartPosition = FormStartPosition.CenterParent, BackColor = Color.White
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = prompt, AutoSize = true, Font = new Font("Segoe UI", 9F) };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340, Font = new Font("Segoe UI", 10F) };
            Button confirmation = new Button() { Text = "OK", Left = 260, Width = 100, Top = 90, DialogResult = DialogResult.OK, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(26, 35, 126), ForeColor = Color.White };
            confirmation.Click += (sender, e) => { promptForm.Close(); };
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.Controls.Add(textLabel);
            promptForm.AcceptButton = confirmation;
            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void BtnAddCurrency_Click(object sender, EventArgs e)
        {
            Form addCurrForm = new Form() {
                Width = 400, Height = 280, FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add New Currency", StartPosition = FormStartPosition.CenterParent, BackColor = Color.White
            };
            
            Label lblCode = new Label() { Left = 20, Top = 20, Text = "Currency Code (e.g., GBP):", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            TextBox txtCode = new TextBox() { Left = 20, Top = 45, Width = 340, Font = new Font("Segoe UI", 10F) };

            Label lblRate = new Label() { Left = 20, Top = 80, Text = "Rate to ETB (e.g., 0.015):", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            TextBox txtRate = new TextBox() { Left = 20, Top = 105, Width = 340, Font = new Font("Segoe UI", 10F) };

            Label lblSymbol = new Label() { Left = 20, Top = 140, Text = "Symbol (e.g., £):", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            TextBox txtSymbol = new TextBox() { Left = 20, Top = 165, Width = 340, Font = new Font("Segoe UI", 10F) };

            Button btnSave = new Button() { Text = "Save", Left = 160, Width = 100, Top = 200, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White };
            Button btnCancel = new Button() { Text = "Cancel", Left = 270, Width = 90, Top = 200, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(198, 40, 40), ForeColor = Color.White };

            btnCancel.Click += (s, ev) => addCurrForm.Close();
            btnSave.Click += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtRate.Text) || string.IsNullOrWhiteSpace(txtSymbol.Text)) {
                    MessageBox.Show("Please fill all fields."); return;
                }
                if (!decimal.TryParse(txtRate.Text, out decimal rate)) {
                    MessageBox.Show("Invalid rate format."); return;
                }
                if (CurrencyService.AddCurrency(txtCode.Text.ToUpper(), rate, txtSymbol.Text)) {
                    MessageBox.Show("✅ Currency added successfully!");
                    CurrencyService.SyncCurrencySelector(cmbCurrency, () => {
                        LoadDashboardStats(); LoadAllData();
                    });
                    addCurrForm.Close();
                } else {
                    MessageBox.Show("❌ Failed to add currency. It might already exist.");
                }
            };

            addCurrForm.Controls.AddRange(new Control[] { lblCode, txtCode, lblRate, txtRate, lblSymbol, txtSymbol, btnSave, btnCancel });
            addCurrForm.ShowDialog();
        }

        private void LoadAllData()
        {
            LoadProducts();
            LoadOrders();
            LoadCustomers();
            LoadInventory();
            LoadSalesReport();
            LoadComplaints();
            LoadDashboardStats();
        }
    }
}
