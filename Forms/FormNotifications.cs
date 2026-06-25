using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Data;

namespace E_commerance_System.Forms
{
    public partial class FormNotifications : Form
    {
        public FormNotifications()
        {
            InitializeComponent();
            LoadNotifications();
        }

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadNotifications();
        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        private void LoadNotifications()
        {
            try {
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    string query = "SELECT NotificationId as ID, Type, Message, CreatedDate as Date, IsRead FROM Notifications ORDER BY CreatedDate DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvNotifications.DataSource = dt;
                    if (dgvNotifications.Columns.Contains("ID")) dgvNotifications.Columns["ID"].Visible = false;
                }
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnMarkRead_Click(object sender, EventArgs e)
        {
            if (dgvNotifications.SelectedRows.Count == 0) return;
            try {
                int id = Convert.ToInt32(dgvNotifications.SelectedRows[0].Cells["ID"].Value);
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE Notifications SET IsRead = 1 WHERE NotificationId = @id", conn)) {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadNotifications();
            } catch { }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvNotifications.SelectedRows.Count == 0) { MessageBox.Show("Please select an alert to delete."); return; }
            
            try {
                int id = Convert.ToInt32(dgvNotifications.SelectedRows[0].Cells["ID"].Value);
                if (MessageBox.Show("Delete this alert?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var conn = DatabaseHelper.GetConnection()) {
                        conn.Open();
                        using (var cmd = new SqlCommand("DELETE FROM Notifications WHERE NotificationId = @id", conn)) {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadNotifications();
                }
            } catch { }
        }


        private void BtnDetail_Click(object sender, EventArgs e)
        {
            if (dgvNotifications.SelectedRows.Count == 0) return;
            
            string type = dgvNotifications.SelectedRows[0].Cells["Type"].Value.ToString();
            string message = dgvNotifications.SelectedRows[0].Cells["Message"].Value.ToString();
            string date = dgvNotifications.SelectedRows[0].Cells["Date"].Value.ToString();
            int id = Convert.ToInt32(dgvNotifications.SelectedRows[0].Cells["ID"].Value);

            // Auto mark as read when viewed
            try {
                using (var conn = DatabaseHelper.GetConnection()) {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE Notifications SET IsRead = 1 WHERE NotificationId = @id", conn)) {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadNotifications();
            } catch { }

            Form detail = new Form {
                Text = "Alert Detail", Size = new Size(500, 400),
                StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(245, 245, 250),
                FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false
            };

            Panel card = new Panel {
                Location = new Point(20, 20), Size = new Size(445, 310),
                BackColor = Color.White
            };
            card.Paint += (s, ev) => ControlPaint.DrawBorder(ev.Graphics, card.ClientRectangle, Color.FromArgb(220, 220, 230), ButtonBorderStyle.Solid);

            var lblType = new Label { Text = $"🔔 {type}", Font = new Font("Segoe UI", 12F, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true, ForeColor = Color.FromArgb(26, 35, 126) };
            var lblDate = new Label { Text = $"📅 {date}", Font = new Font("Segoe UI", 9F), Location = new Point(15, 45), AutoSize = true, ForeColor = Color.Gray };
            
            var txtMsg = new TextBox {
                Text = message, Location = new Point(15, 80), Size = new Size(415, 200),
                Multiline = true, ReadOnly = true, BorderStyle = BorderStyle.None,
                BackColor = Color.White, Font = new Font("Segoe UI", 11F)
            };

            card.Controls.AddRange(new Control[] { lblType, lblDate, txtMsg });
            
            Button btnOk = new Button {
                Text = "Understood", Location = new Point(365, 335), Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(26, 35, 126), ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += (s, ev) => detail.Close();

            detail.Controls.AddRange(new Control[] { card, btnOk });
            detail.ShowDialog();
        }
    }
}
