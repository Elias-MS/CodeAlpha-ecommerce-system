using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormUserManage : Form
    {
        public FormUserManage()
        {
            InitializeComponent();
            
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 126);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.EnableHeadersVisualStyles = false;

            LoadUsers();
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadUsers();
        private void BtnActivate_Click(object sender, EventArgs e) => UpdateUserStatus(true);
        private void BtnDeactivate_Click(object sender, EventArgs e) => UpdateUserStatus(false);
        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void LoadUsers()
        {
            try
            {
                var users = UserService.GetAllUsers();
                var dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("Full Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Currency", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                foreach (var u in users) dt.Rows.Add(u.UserId, u.Username, $"{u.FirstName} {u.LastName}", u.Email, u.PreferredCurrency, u.IsActive ? "Active" : "Inactive");
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void UpdateUserStatus(bool active)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user first.", "No Selection");
                return;
            }

            int id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["ID"].Value);
            string username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            string action = active ? "activate" : "deactivate";
            if (MessageBox.Show($"Are you sure you want to {action} account '{username}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (UserService.ActivateDeactivateUser(id, active))
                {
                    MessageBox.Show($"User account {username} successfully {(active ? "activated" : "deactivated")}!");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to update user status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
