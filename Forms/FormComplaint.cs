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
            this.Text = "❓ Get Help / Ask a Question";
            this.Size = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var lblHeader = new Label { Text = "How can we help you today?", Font = new Font("Segoe UI", 14F, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            
            var lblSub = new Label { Text = "Subject / Topic:", Location = new Point(20, 70), AutoSize = true };
            txtSubject = new TextBox { Location = new Point(20, 95), Width = 390, Font = new Font("Segoe UI", 10F) };
            
            var lblMsg = new Label { Text = "Message Details:", Location = new Point(20, 140), AutoSize = true };
            txtMessage = new TextBox { Location = new Point(20, 165), Width = 390, Height = 180, Multiline = true, Font = new Font("Segoe UI", 10F) };
            
            btnSubmit = new Button { 
                Text = "🚀 Send Message", 
                Location = new Point(20, 370), 
                Size = new Size(390, 50), 
                BackColor = Color.FromArgb(0, 121, 107), 
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
                MessageBox.Show("✅ Your message has been sent to the admin. You can track our response in 'My Support History'.");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}
