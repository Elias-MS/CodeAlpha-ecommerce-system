using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormRegister : Form
    {
        private readonly Color DangerColor = Color.FromArgb(211, 47, 47);

        public FormRegister() { InitializeComponent(); }

        private void LnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => this.Close();

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) { ShowError("Username is required."); return; }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6) { ShowError("Password must be at least 6 characters."); return; }
            if (txtPassword.Text != txtConfirmPass.Text) { ShowError("Passwords do not match."); return; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@")) { ShowError("Valid email is required."); return; }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text)) { ShowError("First name is required."); return; }
            if (string.IsNullOrWhiteSpace(txtLastName.Text)) { ShowError("Last name is required."); return; }

            try
            {
                lblStatus.Text = "Creating account..."; lblStatus.ForeColor = Color.Gray; Application.DoEvents();

                if (UserService.IsUsernameExists(txtUsername.Text.Trim())) { ShowError("Username already taken."); return; }
                if (UserService.IsEmailExists(txtEmail.Text.Trim())) { ShowError("Email already registered."); return; }

                var user = new User
                {
                    Username = txtUsername.Text.Trim(), Email = txtEmail.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(), LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(), Address = txtAddress.Text.Trim()
                };

                if (UserService.RegisterUser(user, txtPassword.Text))
                {
                    MessageBox.Show("🎉 Account created successfully!\nYou can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else ShowError("Registration failed. Please try again.");
            }
            catch (Exception ex) { ShowError("Error: " + ex.Message); }
        }

        private void ShowError(string msg) { lblStatus.Text = "⚠️ " + msg; lblStatus.ForeColor = DangerColor; }
    }
}
