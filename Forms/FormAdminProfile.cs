using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormAdminProfile : Form
    {
        private Admin currentAdmin;

        public FormAdminProfile(Admin admin)
        {
            currentAdmin = admin;
            InitializeComponent();
            LoadAdminData();
        }

        private void BtnBack_Click(object sender, EventArgs e) => this.Close();

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please use the 'Forgot Password' flow or contact System Admin to reset password.", "Info");
        }

        private void LoadAdminData()
        {
            txtUsername.Text = currentAdmin.Username;
            txtFullName.Text = currentAdmin.FullName;
            txtEmail.Text = currentAdmin.Email;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            currentAdmin.FullName = txtFullName.Text.Trim();
            currentAdmin.Email = txtEmail.Text.Trim();

            if (AdminService.UpdateAdmin(currentAdmin))
            {
                MessageBox.Show("✅ Admin profile updated successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("❌ Failed to update admin profile.");
        }
    }
}
