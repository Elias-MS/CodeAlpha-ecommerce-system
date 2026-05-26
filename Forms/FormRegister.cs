using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;
using System.Text.RegularExpressions;
using System.Linq;

namespace E_commerance_System.Forms
{
    public partial class FormRegister : Form
    {
        public string RegisteredUsername { get; private set; }
        public string RegisteredPassword { get; private set; }
        private readonly Color DangerColor = Color.FromArgb(211, 47, 47);

        public FormRegister() { InitializeComponent(); }

        private void LnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => this.Close();

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string phone = txtPhone.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            // 1. Username Validation
            if (username.Length < 4) { ShowError("Username must be at least 4 characters."); return; }
            if (!Regex.IsMatch(username, "^[a-zA-Z0-9_]+$")) { ShowError("Username can only contain letters, numbers, and underscores."); return; }

            // 2. Email Validation
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) { ShowError("Please enter a valid email address."); return; }

            // 3. Password Complexity
            if (password.Length < 8) { ShowError("Password must be at least 8 characters."); return; }
            if (!password.Any(char.IsUpper)) { ShowError("Password must contain at least one uppercase letter."); return; }
            if (!password.Any(char.IsLower)) { ShowError("Password must contain at least one lowercase letter."); return; }
            if (!password.Any(char.IsDigit)) { ShowError("Password must contain at least one number."); return; }
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.? "" :{}|<>]")) { ShowError("Password must contain at least one special character."); return; }

            if (password != txtConfirmPass.Text) { ShowError("Passwords do not match."); return; }

            // 4. Name Validation
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Any(char.IsDigit)) { ShowError("First name must contain only letters."); return; }
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Any(char.IsDigit)) { ShowError("Last name must contain only letters."); return; }

            // 5. Phone Validation
            if (!Regex.IsMatch(phone, @"^\d{10,13}$")) { ShowError("Phone must be 10-13 digits long."); return; }

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
                    RegisteredUsername = user.Username;
                    RegisteredPassword = txtPassword.Text;
                    MessageBox.Show("🎉 Account created successfully!\nLogging you in automatically...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else ShowError("Registration failed. Please try again.");
            }
            catch (Exception ex) { ShowError("Error: " + ex.Message); }
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkShowPassword.Checked ? '\0' : '●';
            txtPassword.PasswordChar = passwordChar;
            txtConfirmPass.PasswordChar = passwordChar;
        }

        private void ShowError(string msg) { lblStatus.Text = "⚠️ " + msg; lblStatus.ForeColor = DangerColor; }
    }
}
