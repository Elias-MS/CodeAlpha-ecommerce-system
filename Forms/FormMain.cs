using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Data;
using E_commerance_System.Services;
using System.Data.SqlClient;

namespace E_commerance_System.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            SetupAdvancedUI();
        }

        private Label lblMarquee;
        private Timer marqueeTimer;

        private FlowLayoutPanel flowProducts;

        private void SetupAdvancedUI()
        {
            this.Text = "✨ E-Commerce Speed — Discover Premium Products";
            this.Size = new Size(1280, 800);
            this.BackColor = Color.FromArgb(10, 10, 12);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Hide designer components to avoid overlap
            if (this.menuStrip != null) this.menuStrip.Visible = false;
            if (this.pnlMission != null) this.pnlMission.Visible = false;

            this.Controls.Clear();

            // --- Welcome Marquee ---
            var pnlMarquee = new Panel { Dock = DockStyle.Top, Height = 35, BackColor = Color.FromArgb(0, 150, 136) };
            lblMarquee = new Label { 
                Text = "      welcome to E-commerance system delivery      ", 
                ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true, Location = new Point(pnlMarquee.Width, 6) 
            };
            pnlMarquee.Controls.Add(lblMarquee);
            this.Controls.Add(pnlMarquee);

            marqueeTimer = new Timer { Interval = 40 };
            marqueeTimer.Tick += (s, e) => {
                lblMarquee.Left -= 2;
                if (lblMarquee.Right < 0) lblMarquee.Left = pnlMarquee.Width;
            };
            marqueeTimer.Start();
            UpdateTickerText();


            // --- Navigation Bar ---
            var pnlNavBar = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.FromArgb(15, 15, 20), Padding = new Padding(30, 0, 30, 0) };
            var lblLogo = new Label { Text = "E-COMMERCE SPEED", ForeColor = Color.FromArgb(0, 150, 136), Font = new Font("Outfit", 18F, FontStyle.Bold), AutoSize = true, Location = new Point(30, 22) };
            
            var pnlMenu = new FlowLayoutPanel { Dock = DockStyle.Right, Width = 800, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 20, 0, 0) };
            
            // Global Currency Switcher
            var lblCurr = new Label { Text = "CURRENCY:", ForeColor = Color.Gray, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Margin = new Padding(0, 10, 5, 0), AutoSize = true };
            var cmbGlobalCurrency = new ComboBox { Width = 80, BackColor = Color.FromArgb(30, 30, 35), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 5, 20, 0) };
            try {
                var rates = CurrencyService.GetAllRates();
                foreach (var r in rates) cmbGlobalCurrency.Items.Add(r.CurrencyCode);
                cmbGlobalCurrency.SelectedItem = CurrencyService.CurrentCurrency;
                cmbGlobalCurrency.SelectedIndexChanged += (s, e) => {
                    CurrencyService.CurrentCurrency = cmbGlobalCurrency.SelectedItem.ToString();
                    RefreshFeaturedProducts();
                };
            } catch { }

            pnlMenu.Controls.AddRange(new Control[] {
                lblCurr, cmbGlobalCurrency,
                CreateNavLink("SHOP", MenuPortfolio_Click),
                CreateNavLink("LOGIN", MenuLogin_Click),
                CreateNavLink("REGISTER", MenuRegister_Click),
                CreateNavLink("EXIT", MenuExit_Click)
            });

            pnlNavBar.Controls.Add(lblLogo);
            pnlNavBar.Controls.Add(pnlMenu);
            this.Controls.Add(pnlNavBar);

            // --- Scrollable Content Container ---
            var pnlMainScroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.FromArgb(10, 10, 12) };
            this.Controls.Add(pnlMainScroll);

            // --- Hero Section ---
            pnlHero = new Panel();
            pnlHero.Dock = DockStyle.Top;
            pnlHero.Height = 500;
            pnlHero.BackColor = Color.FromArgb(20, 20, 25);
            
            try 
            {
                string imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", "hero_bg.jpg");
                if (System.IO.File.Exists(imgPath))
                {
                    pnlHero.BackgroundImage = Image.FromFile(imgPath);
                    pnlHero.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }

            pnlHero.Paint += (s, e) => {
                using (var b = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                    e.Graphics.FillRectangle(b, pnlHero.ClientRectangle);
            };

            var lblHeroTitle = new Label { 
                Text = "Upgrade Your Lifestyle\nWith SPEED Products", 
                Font = new Font("Segoe UI Semibold", 36F), 
                ForeColor = Color.White, 
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            var pnlHeroActions = new Panel { Dock = DockStyle.Bottom, Height = 120, BackColor = Color.Transparent };
            var btnShopLarge = new Button { 
                Text = "SHOP NOW  🚀", Size = new Size(250, 60), 
                Location = new Point((1280/2) - 125, 20),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(0, 150, 136), 
                ForeColor = Color.White, Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnShopLarge.FlatAppearance.BorderSize = 0;
            btnShopLarge.Click += MenuPortfolio_Click;
            pnlHeroActions.Controls.Add(btnShopLarge);

            pnlHero.Controls.Add(lblHeroTitle);
            pnlHero.Controls.Add(pnlHeroActions);

            // --- Featured Products Section ---
            var featuredSection = CreateFeaturedSection();
            featuredSection.Dock = DockStyle.Top;

            // --- Features Section ---
            pnlFeatures = new FlowLayoutPanel();
            pnlFeatures.Dock = DockStyle.Top; 
            pnlFeatures.Height = 250;
            pnlFeatures.BackColor = Color.FromArgb(10, 10, 12);
            pnlFeatures.Padding = new Padding(50, 40, 50, 40);
            
            AddModernFeature(pnlFeatures, "🚚", "Fast Delivery", "Global priority shipping.", Color.FromArgb(0, 150, 136));
            AddModernFeature(pnlFeatures, "🛡️", "Secure Checkout", "End-to-end encryption.", Color.FromArgb(63, 81, 181));
            AddModernFeature(pnlFeatures, "💎", "Premium Quality", "Handpicked for excellence.", Color.FromArgb(156, 39, 176));
            AddModernFeature(pnlFeatures, "🤝", "24/7 Support", "Always here for you.", Color.FromArgb(255, 152, 0));

            // --- Footer ---
            pnlFooter = new Panel();
            pnlFooter.Dock = DockStyle.Top; 
            pnlFooter.Height = 120;
            pnlFooter.BackColor = Color.FromArgb(15, 15, 20);
            
            var lblCopy = new Label { Text = "© 2026 E-Commerce speed. All rights reserved.", ForeColor = Color.Gray, Location = new Point(50, 50), AutoSize = true };
            pnlFooter.Controls.Add(lblCopy);

            // --- Add sections to scroll container in order ---
            pnlMainScroll.Controls.Add(pnlFooter);
            pnlMainScroll.Controls.Add(pnlFeatures);
            pnlMainScroll.Controls.Add(featuredSection);
            pnlMainScroll.Controls.Add(pnlHero);
        }

        private void RefreshFeaturedProducts()
        {
            if (flowProducts == null) return;
            flowProducts.Controls.Clear();
            try
            {
                var products = ProductService.GetFeaturedProducts();
                int count = 0;
                foreach (var p in products)
                {
                    if (count >= 5) break;
                    flowProducts.Controls.Add(CreateProductThumbnail(p));
                    count++;
                }
            }
            catch { }
        }

        private Panel CreateFeaturedSection()
        {
            var pnlFeatured = new Panel { Dock = DockStyle.Top, Height = 450, BackColor = Color.FromArgb(10, 10, 12), Padding = new Padding(30) };
            var lblTitle = new Label { Text = "⭐ FEATURED PRODUCTS", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Color.FromArgb(255, 215, 0), Dock = DockStyle.Top, Height = 50, TextAlign = ContentAlignment.MiddleLeft };
            
            flowProducts = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = false };
            RefreshFeaturedProducts();

            pnlFeatured.Controls.Add(flowProducts);
            pnlFeatured.Controls.Add(lblTitle);
            return pnlFeatured;
        }

        private Panel CreateProductThumbnail(Product p)
        {
            var card = new Panel { Size = new Size(220, 320), Margin = new Padding(10), BackColor = Color.FromArgb(25, 25, 30) };
            
            var pb = new PictureBox { Size = new Size(220, 180), Dock = DockStyle.Top, Name = "pbProduct", SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(30, 30, 35) };
            try
            {
                if (!string.IsNullOrEmpty(p.MainImagePath) && System.IO.File.Exists(p.MainImagePath))
                    pb.Image = Image.FromFile(p.MainImagePath);
                else
                    pb.Image = CreateDefaultProductImage();
            }
            catch { pb.Image = CreateDefaultProductImage(); }

            // Currency Conversion for Display
            decimal displayPrice = CurrencyService.ConvertAmount(p.Price, CurrencyService.CurrentCurrency);
            string symbol = "Br";
            try {
                var rates = CurrencyService.GetAllRates();
                var rate = rates.Find(r => r.CurrencyCode == CurrencyService.CurrentCurrency);
                if (rate != null) symbol = rate.Symbol;
            } catch { }

            var lblName = new Label { Text = p.Name, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.White, Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter, Padding = new Padding(5) };
            var lblPrice = new Label { Text = $"{displayPrice:N2} {symbol}", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 150, 136), Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
            
            var btnView = new Button { Text = "VIEW", Dock = DockStyle.Bottom, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(0, 150, 136), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => MenuPortfolio_Click(null, null);

            card.Controls.AddRange(new Control[] { lblName, lblPrice, btnView, pb });
            return card;
        }

        private Image CreateDefaultProductImage()
        {
            var bmp = new Bitmap(220, 180);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(40, 40, 45));
                g.DrawString("🛒", new Font("Segoe UI", 40F), Brushes.DimGray, new PointF(70, 50));
            }
            return bmp;
        }

        private void UpdateTickerText()
        {
            // Removed dynamic news fetching - Alert news is now for logged-in users only.
            lblMarquee.Text = "📢 welcome to speed E-commerance system delivery          |          Premium Products. Secured Payments.          |          Fast Shipping & Secure Checkout.";
        }

        private Button CreateNavLink(string text, EventHandler onClick)
        {
            var btn = new Button { Text = text, Size = new Size(120, 40), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(180, 180, 190), Font = new Font("Segoe UI Semibold", 10F), Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 30, 40);
            btn.Click += onClick;
            return btn;
        }

        private void AddModernFeature(FlowLayoutPanel p, string icon, string title, string desc, Color accent)
        {
            var card = new Panel { Size = new Size(260, 200), Margin = new Padding(20), BackColor = Color.FromArgb(20, 20, 25) };
            card.Controls.Add(new Label { Text = icon, Font = new Font("Segoe UI", 28F), Location = new Point(20, 20), AutoSize = true, ForeColor = accent });
            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(20, 85), AutoSize = true, ForeColor = Color.White });
            card.Controls.Add(new Label { Text = desc, Font = new Font("Segoe UI", 10F), Location = new Point(20, 120), Size = new Size(220, 60), ForeColor = Color.Gray });
            p.Controls.Add(card);
        }

        private void MenuLogin_Click(object sender, EventArgs e) => new FormLogin().ShowDialog();
        private void MenuRegister_Click(object sender, EventArgs e) => new FormRegister().ShowDialog();
        private void MenuPortfolio_Click(object sender, EventArgs e) => new FormPortfolio().ShowDialog();
        private void MenuExit_Click(object sender, EventArgs e) => Application.Exit();
        
        private void BtnGuest_Click(object sender, EventArgs e)
        {
            var guestUser = new User { UserId = 0, FirstName = "Guest", Username = "guest" };
            new FormCustomerDashboard(guestUser).ShowDialog();
        }
    }
}
