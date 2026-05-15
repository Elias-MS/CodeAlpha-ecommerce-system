using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Data;
using System.Data.SqlClient;

namespace E_commerance_System.Forms
{
    public class FormComplaint : Form
    {
        private User currentUser;
        private TextBox txtSubject, txtMessage;
        private Button btnSubmit;

        public FormComplaint(User user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "🚩 Submit a Complaint";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var lblHeader = new Label { Text = "What's the issue?", Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            
            var lblSub = new Label { Text = "Subject:", Location = new Point(20, 70), AutoSize = true };
            txtSubject = new TextBox { Location = new Point(20, 95), Width = 340, Font = new Font("Segoe UI", 10F) };
            
            var lblMsg = new Label { Text = "Details:", Location = new Point(20, 140), AutoSize = true };
            txtMessage = new TextBox { Location = new Point(20, 165), Width = 340, Height = 150, Multiline = true, Font = new Font("Segoe UI", 10F) };
            
            btnSubmit = new Button { 
                Text = "📤 Submit Complaint", 
                Location = new Point(20, 340), 
                Size = new Size(340, 45), 
                BackColor = Color.FromArgb(198, 40, 40), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            this.Controls.AddRange(new Control[] { lblHeader, lblSub, txtSubject, lblMsg, txtMessage, btnSubmit });
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text) || string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO Complaints (UserId, Subject, Message, Status) VALUES (@uid, @sub, @msg, 'Pending')";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", currentUser.UserId);
                        cmd.Parameters.AddWithValue("@sub", txtSubject.Text);
                        cmd.Parameters.AddWithValue("@msg", txtMessage.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("✅ Your complaint has been sent to the admin. We will review it shortly.");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}
