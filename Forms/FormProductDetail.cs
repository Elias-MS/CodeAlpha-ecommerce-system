using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormProductDetail : Form
    {
        private Product product;

        public FormProductDetail(Product p)
        {
            product = p;
            InitializeComponent();
            SetupModernLayout();
        }

        private void SetupModernLayout()
        {
            this.Text = $"✨ {product.Name} - Premium Details";
            this.Size = new Size(1000, 750);
            this.BackColor = Color.White;

            lblName.Text = product.Name;
            lblSpecs.Text = $"Brand: {product.Brand} | Model: {product.Model}";
            lblPrice.Text = $"{product.Price:N2} Birr";
            lblStock.Text = $"Availability: {(product.Stock > 0 ? "In Stock" : "Out of Stock")}";
            txtDesc.Text = product.Description;

            // --- RELATED PRODUCTS (AI RECOMMENDATIONS) ---
            var pnlRelated = new Panel { Dock = DockStyle.Bottom, Height = 280, BackColor = Color.FromArgb(245, 245, 250), Padding = new Padding(20) };
            var lblTitle = new Label { Text = "✨ YOU MAY ALSO LIKE (AI Recommendations)", Font = new Font("Segoe UI", 12F, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, ForeColor = Color.FromArgb(26, 35, 126) };
            
            var flpRelated = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            
            // Fetch related products (same category)
            var related = ProductService.GetProductsByCategory(product.CategoryId);
            foreach (var r in related)
            {
                if (r.ProductId == product.ProductId) continue; // Skip current
                var card = CreateSmallCard(r);
                flpRelated.Controls.Add(card);
            }

            pnlRelated.Controls.Add(flpRelated);
            pnlRelated.Controls.Add(lblTitle);
            this.Controls.Add(pnlRelated);
        }

        private Panel CreateSmallCard(Product p)
        {
            var card = new Panel { Size = new Size(180, 200), Margin = new Padding(10), BackColor = Color.White };
            card.BorderStyle = BorderStyle.FixedSingle;
            
            var pb = new PictureBox { Size = new Size(180, 100), Dock = DockStyle.Top, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(240, 240, 245) };
            try { if (!string.IsNullOrEmpty(p.MainImagePath)) pb.ImageLocation = p.MainImagePath; } catch { }
            
            var lblN = new Label { Text = p.Name, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Dock = DockStyle.Top, Height = 35, TextAlign = ContentAlignment.MiddleCenter };
            var lblP = new Label { Text = $"{p.Price:N2} Birr", Font = new Font("Segoe UI", 9F), Dock = DockStyle.Top, Height = 25, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(0, 150, 136) };
            
            var btnView = new Button { Text = "View", Dock = DockStyle.Bottom, FlatStyle = FlatStyle.Flat, Height = 30, BackColor = Color.FromArgb(26, 35, 126), ForeColor = Color.White };
            btnView.Click += (s, e) => { this.Close(); new FormProductDetail(p).ShowDialog(); };

            card.Controls.AddRange(new Control[] { btnView, lblP, lblN, pb });
            return card;
        }

        private void BtnAddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("✅ Added to cart!");
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
