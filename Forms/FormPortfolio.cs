using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormPortfolio : Form
    {
        public FormPortfolio()
        {
            InitializeComponent();
            SetupPortfolioUI();
            LoadFeaturedProducts();
        }

        private void SetupPortfolioUI()
        {
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(20, 20, 25), Padding = new Padding(20, 10, 20, 10) };
            var lblTitle = new Label { Text = "PRODUCT PORTFOLIO", ForeColor = Color.White, Font = new Font("Segoe UI", 14F, FontStyle.Bold), AutoSize = true, Location = new Point(20, 15) };
            
            var pnlCurr = new FlowLayoutPanel { Dock = DockStyle.Right, Width = 250, FlowDirection = FlowDirection.LeftToRight };
            var lblC = new Label { Text = "Currency:", ForeColor = Color.Gray, Font = new Font("Segoe UI", 9F), Margin = new Padding(0, 10, 5, 0), AutoSize = true };
            var cmbC = new ComboBox { Width = 80, BackColor = Color.FromArgb(30, 30, 35), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            try {
                var rates = CurrencyService.GetAllRates();
                foreach (var r in rates) cmbC.Items.Add(r.CurrencyCode);
                cmbC.SelectedItem = CurrencyService.CurrentCurrency;
                cmbC.SelectedIndexChanged += (s, e) => {
                    CurrencyService.CurrentCurrency = cmbC.SelectedItem.ToString();
                    pnlGallery.Controls.Clear();
                    LoadFeaturedProducts();
                };
            } catch { }

            pnlCurr.Controls.AddRange(new Control[] { lblC, cmbC });
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(pnlCurr);
            this.Controls.Add(pnlTop);
        }

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void LoadFeaturedProducts()
        {
            try
            {
                var products = ProductService.GetFeaturedProducts();
                foreach (var p in products)
                {
                    AddProductCard(p);
                }
            }
            catch { }
        }

        private void AddProductCard(Product p)
        {
            var card = new Panel { Size = new Size(240, 360), Margin = new Padding(15), BackColor = Color.FromArgb(30, 30, 30), Padding = new Padding(10) };
            card.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.FromArgb(50, 50, 50), ButtonBorderStyle.Solid);

            var pb = new PictureBox { Size = new Size(220, 150), Dock = DockStyle.Top, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(40, 40, 45) };
            try
            {
                if (!string.IsNullOrEmpty(p.MainImagePath) && System.IO.File.Exists(p.MainImagePath))
                    pb.Image = Image.FromFile(p.MainImagePath);
                else
                    pb.Image = CreateDefaultImage();
            }
            catch { pb.Image = CreateDefaultImage(); }

            // Currency Conversion
            decimal displayPrice = CurrencyService.ConvertAmount(p.Price, CurrencyService.CurrentCurrency);
            string symbol = "Br";
            try {
                var rates = CurrencyService.GetAllRates();
                var rate = rates.Find(r => r.CurrencyCode == CurrencyService.CurrentCurrency);
                if (rate != null) symbol = rate.Symbol;
            } catch { }

            var lblName = new Label { Text = p.Name, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.White, Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
            var lblPrice = new Label { Text = $"{displayPrice:N2} {symbol}", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 150, 136), Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
            var lblDesc = new Label { Text = p.Description, Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, Dock = DockStyle.Fill, TextAlign = ContentAlignment.TopCenter, Padding = new Padding(0, 5, 0, 0) };

            var btnView = new Button { Text = "View Details", Dock = DockStyle.Bottom, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(255, 193, 7), ForeColor = Color.Black, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => MessageBox.Show("Please login to see full details and purchase!", "Login Required");

            card.Controls.AddRange(new Control[] { lblDesc, lblPrice, lblName, pb, btnView });
            pnlGallery.Controls.Add(card);
        }

        private Image CreateDefaultImage()
        {
            var bmp = new Bitmap(220, 150);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(45, 45, 50));
                g.DrawString("🛍️", new Font("Segoe UI", 30F), Brushes.Gray, new PointF(85, 45));
            }
            return bmp;
        }
    }
}
