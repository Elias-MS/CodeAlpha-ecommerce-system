using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

// ============================================================
// FORM: PRODUCT MANAGE — Add / Edit Product (Admin only)
// ============================================================
// Demonstrates: Form reuse pattern — same form for Add and Edit
// When product is null → Add mode; otherwise → Edit mode.
// ============================================================

namespace E_commerance_System.Forms
{
    public partial class FormProductManage : Form
    {
        private PictureBox pbPreview;
        private TextBox txtImagePath;
        private Button btnBrowse;
        private Product editProduct; // null = Add mode
        private bool isEditMode;
        private string lastCurrency = "ETB";

        public FormProductManage(Product product)
        {
            editProduct = product;
            isEditMode = product != null;
            InitializeComponent();
            SetupImageUI();
            
            LoadCurrencies();

            this.Text = isEditMode ? "✏️ Edit Product" : "➕ Add New Product";
            lblTitle.Text = isEditMode ? "✏️  Edit Product" : "➕  Add New Product";
            btnSave.Text = isEditMode ? "💾 Update Product" : "✅ Add Product";

            LoadCategories();
            if (isEditMode) PopulateFields();
        }

        private void LoadCurrencies()
        {
            try
            {
                var rates = CurrencyService.GetAllRates();
                cmbCurrency.Items.Clear();
                foreach (var r in rates)
                    cmbCurrency.Items.Add(r.CurrencyCode);
                
                if (cmbCurrency.Items.Count > 0) cmbCurrency.SelectedIndex = 0;
                cmbCurrency.SelectedIndexChanged += CmbCurrency_SelectedIndexChanged;
            }
            catch { }
        }

        private void CmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrice.Text, out decimal currentPrice)) return;

            string newCurrency = cmbCurrency.SelectedItem.ToString();
            if (newCurrency == lastCurrency) return;

            // Convert to ETB (Base) first using dynamic rates
            decimal lastRate = CurrencyService.GetRate(lastCurrency);
            decimal etbPrice = currentPrice * lastRate;

            // Convert from ETB to New Currency
            decimal newRate = CurrencyService.GetRate(newCurrency);
            decimal finalPrice = newRate != 0 ? etbPrice / newRate : etbPrice;

            txtPrice.Text = finalPrice.ToString("F2");
            lastCurrency = newCurrency;
        }

        private void SetupImageUI()
        {
            // Expand form width to fit image preview
            this.Width += 250;

            var lblImg = new Label { Text = "Product Image:", Location = new Point(480, 80), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.White };
            
            pbPreview = new PictureBox { 
                Location = new Point(480, 110), 
                Size = new Size(200, 180), 
                BorderStyle = BorderStyle.FixedSingle, 
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(30, 30, 35)
            };

            txtImagePath = new TextBox { Location = new Point(480, 300), Size = new Size(200, 25), ReadOnly = true, BackColor = Color.FromArgb(40, 40, 45), ForeColor = Color.White };
            
            btnBrowse = new Button { 
                Text = "Browse Image...", 
                Location = new Point(480, 335), 
                Size = new Size(200, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.Click += BtnBrowse_Click;

            this.Controls.Add(lblImg);
            this.Controls.Add(pbPreview);
            this.Controls.Add(txtImagePath);
            this.Controls.Add(btnBrowse);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    try { pbPreview.Image = Image.FromFile(ofd.FileName); }
                    catch { MessageBox.Show("Invalid image file."); }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }

        private void LoadCategories()
        {
            try
            {
                var categories = CategoryService.GetAllCategories();
                cmbCategory.Items.Clear();
                foreach (var cat in categories)
                    cmbCategory.Items.Add(new CategoryItem { Id = cat.CategoryId, Name = cat.CategoryName });
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Error loading categories: " + ex.Message); }
        }

        private void PopulateFields()
        {
            txtName.Text = editProduct.Name;
            txtDescription.Text = editProduct.Description;
            txtPrice.Text = editProduct.Price.ToString("F2");
            txtStock.Text = editProduct.Stock.ToString();
            txtBrand.Text = editProduct.Brand;
            txtModel.Text = editProduct.Model;
            chkIsActive.Checked = editProduct.IsActive;
            chkIsFeatured.Checked = editProduct.IsFeatured;
            txtImagePath.Text = editProduct.MainImagePath;

            if (!string.IsNullOrEmpty(editProduct.MainImagePath) && System.IO.File.Exists(editProduct.MainImagePath))
            {
                try { pbPreview.Image = Image.FromFile(editProduct.MainImagePath); }
                catch { }
            }

            // Select the matching category
            for (int i = 0; i < cmbCategory.Items.Count; i++)
            {
                if (((CategoryItem)cmbCategory.Items[i]).Id == editProduct.CategoryId)
                { cmbCategory.SelectedIndex = i; break; }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // === VALIDATION ===
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { MessageBox.Show("Product name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtName.Focus(); return; }

            if (cmbCategory.SelectedItem == null)
            { MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!decimal.TryParse(txtPrice.Text, out decimal enteredPrice) || enteredPrice <= 0)
            { MessageBox.Show("Please enter a valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPrice.Focus(); return; }

            string currency = cmbCurrency.SelectedItem.ToString();
            decimal rate = CurrencyService.GetRate(currency);
            decimal basePrice = enteredPrice * rate;

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            { MessageBox.Show("Please enter a valid stock quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtStock.Focus(); return; }

            try
            {
                var catItem = (CategoryItem)cmbCategory.SelectedItem;

                if (isEditMode)
                {
                    // UPDATE existing product
                    editProduct.Name = txtName.Text.Trim();
                    editProduct.Description = txtDescription.Text.Trim();
                    editProduct.CategoryId = catItem.Id;
                    editProduct.Price = basePrice;
                    editProduct.Stock = stock;
                    editProduct.Brand = txtBrand.Text.Trim();
                    editProduct.Model = txtModel.Text.Trim();
                    editProduct.IsActive = chkIsActive.Checked;
                    editProduct.IsFeatured = chkIsFeatured.Checked;
                    
                    if (!string.IsNullOrEmpty(txtImagePath.Text) && txtImagePath.Text != editProduct.MainImagePath)
                    {
                        editProduct.MainImagePath = SaveImageToResources(txtImagePath.Text);
                    }

                    if (ProductService.UpdateProduct(editProduct))
                    {
                        MessageBox.Show("✅ Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else MessageBox.Show("Failed to update product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // ADD new product
                    var product = new Product
                    {
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        CategoryId = catItem.Id,
                        Price = basePrice,
                        Stock = stock,
                        Brand = txtBrand.Text.Trim(),
                        Model = txtModel.Text.Trim(),
                        IsActive = chkIsActive.Checked,
                        IsFeatured = chkIsFeatured.Checked,
                        MainImagePath = SaveImageToResources(txtImagePath.Text)
                    };

                    if (ProductService.AddProduct(product))
                    {
                        MessageBox.Show("✅ Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else MessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string SaveImageToResources(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath)) return "";
            
            // If already a relative path to resources, keep it
            if (sourcePath.StartsWith("Resources")) return sourcePath;
            if (!System.IO.File.Exists(sourcePath)) return "";

            try
            {
                string destDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Products");
                if (!System.IO.Directory.Exists(destDir)) System.IO.Directory.CreateDirectory(destDir);

                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(sourcePath);
                string destPath = System.IO.Path.Combine(destDir, fileName);
                
                System.IO.File.Copy(sourcePath, destPath, true);
                
                // Return relative path for database storage
                return System.IO.Path.Combine("Resources", "Products", fileName);
            }
            catch { return ""; }
        }

        // Helper class to store category ID + name in ComboBox
        private class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() { return Name; }
        }
    }
}
