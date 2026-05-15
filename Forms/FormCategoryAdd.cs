using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormCategoryAdd : Form
    {
        public FormCategoryAdd()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a category name.");
                return;
            }

            var cat = new Category
            {
                CategoryName = txtName.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                IsActive = true,
                SortOrder = 0
            };

            if (CategoryService.AddCategory(cat))
            {
                MessageBox.Show("✅ Category added successfully!");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("❌ Failed to add category. It might already exist.");
            }
        }
    }
}
