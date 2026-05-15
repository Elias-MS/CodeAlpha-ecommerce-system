using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormWishlist : Form
    {
        private User currentUser;
        private string currentCurrency;
        private decimal exchangeRate;

        public FormWishlist(User user, string currency = "ETB")
        {
            currentUser = user;
            currentCurrency = currency;

            exchangeRate = 1.0m;
            if (currentCurrency == "USD") exchangeRate = 1.0m / 55.0m;
            else if (currentCurrency == "EUR") exchangeRate = 1.0m / 60.0m;

            InitializeComponent();
            
            dgvWishlist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181);
            dgvWishlist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvWishlist.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvWishlist.EnableHeadersVisualStyles = false;

            LoadWishlist();
        }

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void LoadWishlist()
        {
            var items = WishlistService.GetUserWishlist(currentUser.UserId);
            var dt = new System.Data.DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ProductId", typeof(int));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Added Date", typeof(DateTime));

            foreach (var item in items)
            {
                dt.Rows.Add(item.WishlistId, item.ProductId, item.Product.Name, item.Product.Price * exchangeRate, item.Product.Stock > 0 ? "In Stock" : "Out of Stock", item.AddedDate);
            }

            dgvWishlist.DataSource = dt;
            if (dgvWishlist.Columns.Contains("ID")) dgvWishlist.Columns["ID"].Visible = false;
            if (dgvWishlist.Columns.Contains("ProductId")) dgvWishlist.Columns["ProductId"].Visible = false;
            if (dgvWishlist.Columns.Contains("Price"))
            {
                dgvWishlist.Columns["Price"].DefaultCellStyle.Format = "N2";
                dgvWishlist.Columns["Price"].HeaderText = $"Price ({currentCurrency})";
            }
            
            lblCount.Text = $"{items.Count} item{(items.Count != 1 ? "s" : "")} saved";
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvWishlist.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvWishlist.SelectedRows[0].Cells["ID"].Value);
            
            if (WishlistService.RemoveFromWishlist(id))
            {
                LoadWishlist();
            }
        }

        private void BtnToCart_Click(object sender, EventArgs e)
        {
            if (dgvWishlist.SelectedRows.Count == 0) return;
            int prodId = Convert.ToInt32(dgvWishlist.SelectedRows[0].Cells["ProductId"].Value);
            int wishId = Convert.ToInt32(dgvWishlist.SelectedRows[0].Cells["ID"].Value);
            
            var product = ProductService.GetProductById(prodId);
            if (product != null && product.Stock > 0)
            {
                ShoppingCartService.AddToCart(product, 1);
                WishlistService.RemoveFromWishlist(wishId);
                LoadWishlist();
                MessageBox.Show("Product moved to cart!", "Success");
            }
            else
            {
                MessageBox.Show("Product is out of stock.", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
    }
}
