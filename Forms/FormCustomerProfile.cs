using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormCustomerProfile : Form
    {
        private User currentUser;
        private ComboBox cmbCurrency;

        public FormCustomerProfile(User user)
        {
            currentUser = user;
            InitializeComponent();
            SetupProfileUI();
            LoadUserData();
        }

        private void SetupProfileUI()
        {
            // Adding Currency Selector to the existing layout
            var lblC = new Label { Text = "Preferred Currency:", Location = new Point(20, 240), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            cmbCurrency = new ComboBox { Location = new Point(20, 260), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            
            CurrencyService.SyncCurrencySelector(cmbCurrency, null);
            
            // Re-layout other buttons if necessary, or just add
            this.Controls.Add(lblC);
            this.Controls.Add(cmbCurrency);
            
            btnSave.Location = new Point(20, 310);
            btnBack.Location = new Point(160, 310);
        }

        private void BtnBack_Click(object sender, EventArgs e) => this.Close();

        private void LoadUserData()
        {
            txtFirstName.Text = currentUser.FirstName;
            txtLastName.Text = currentUser.LastName;
            txtEmail.Text = currentUser.Email;
            txtPhone.Text = currentUser.Phone;
            txtAddress.Text = currentUser.Address;
            txtCity.Text = currentUser.City;
            
            if (!string.IsNullOrEmpty(currentUser.PreferredCurrency))
                cmbCurrency.SelectedItem = currentUser.PreferredCurrency;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            currentUser.FirstName = txtFirstName.Text;
            currentUser.LastName = txtLastName.Text;
            currentUser.Email = txtEmail.Text;
            currentUser.Phone = txtPhone.Text;
            currentUser.Address = txtAddress.Text;
            currentUser.City = txtCity.Text;
            currentUser.PreferredCurrency = cmbCurrency.SelectedItem?.ToString() ?? "ETB";

            if (UserService.UpdateUser(currentUser))
            {
                CurrencyService.CurrentCurrency = currentUser.PreferredCurrency;
                MessageBox.Show("✅ Profile and Currency updated successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("Failed to update profile.");
        }
    }
}
