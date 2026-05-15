using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormInventoryManage : Form
    {
        public FormInventoryManage()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadInventory();
        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void LoadInventory()
        {
            try
            {
                var products = ProductService.GetAllProducts();
                var dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Product Name", typeof(string));
                dt.Columns.Add("Current Stock", typeof(int));

                foreach (var p in products)
                {
                    dt.Rows.Add(p.ProductId, p.ProductCode, p.Name, p.Stock);
                }
                dgvInventory.DataSource = dt;
                if (dgvInventory.Columns.Contains("ID")) dgvInventory.Columns["ID"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnRestock_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0) { MessageBox.Show("Please select a product to restock."); return; }
            
            var row = dgvInventory.SelectedRows[0];
            int productId = Convert.ToInt32(row.Cells["ID"].Value);
            string name = row.Cells["Product Name"].Value.ToString();
            int currentStock = Convert.ToInt32(row.Cells["Current Stock"].Value);

            var form = new FormAddStock(name, currentStock);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int newTotal = currentStock + form.StockToAdd;
                if (ProductService.UpdateStock(productId, newTotal))
                {
                    MessageBox.Show($"✅ Successfully added {form.StockToAdd} to stock!");
                    LoadInventory();
                }
                else MessageBox.Show("❌ Failed to update stock.");
            }
        }
    }
}
