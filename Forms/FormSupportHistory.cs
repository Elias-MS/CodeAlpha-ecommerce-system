using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Data;
using E_commerance_System.Models;

namespace E_commerance_System.Forms
{
    public class FormSupportHistory : Form
    {
        private User currentUser;
        private DataGridView dgvSupport;
        private TextBox txtDetail, txtReply;

        public FormSupportHistory(User user)
        {
            currentUser = user;
            InitializeComponent();
            LoadSupportHistory();
        }

        private void InitializeComponent()
        {
            this.Text = "💬 My Support History & Answers";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 245, 250);

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(18, 18, 24) };
            var lblTitle = new Label { 
                Text = "MY SUPPORT REQUESTS", ForeColor = Color.White, 
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 15), AutoSize = true 
            };
            pnlTop.Controls.Add(lblTitle);

            dgvSupport = new DataGridView {
                Dock = DockStyle.Top, Height = 250,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvSupport.SelectionChanged += DgvSupport_SelectionChanged;

            var pnlDetail = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            var lblMsg = new Label { Text = "📝 Your Message:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            txtDetail = new TextBox { Dock = DockStyle.Top, Height = 80, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, BackColor = Color.White };
            
            var lblSpacer = new Label { Dock = DockStyle.Top, Height = 15 };

            var lblAns = new Label { Text = "🤖 Admin Response:", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 105, 92) };
            txtReply = new TextBox { 
                Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, 
                ScrollBars = ScrollBars.Vertical, BackColor = Color.FromArgb(232, 242, 241),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 77, 64)
            };

            pnlDetail.Controls.AddRange(new Control[] { txtReply, lblAns, lblSpacer, txtDetail, lblMsg });

            var pnlSectionHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(20, 10, 20, 5) };
            var lblSectionTitle = new Label { Text = "📋 Your Support Tickets", Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(40, 40, 50), AutoSize = true, Location = new Point(20, 10) };
            var lblSectionSub = new Label { Text = "Track the status of your inquiries and view official responses below.", Font = new Font("Segoe UI", 8.25F), ForeColor = Color.Gray, AutoSize = true, Location = new Point(22, 32) };
            var lineSec = new Panel { Height = 1, BackColor = Color.FromArgb(230, 230, 235), Dock = DockStyle.Bottom };
            pnlSectionHeader.Controls.AddRange(new Control[] { lblSectionTitle, lblSectionSub, lineSec });

            this.Controls.AddRange(new Control[] { pnlDetail, dgvSupport, pnlSectionHeader, pnlTop });
        }

        private void LoadSupportHistory()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT ComplaintId, Subject, Status, CreatedDate as [Date Sent], AdminReply, ReplyDate FROM Complaints WHERE UserId = @uid ORDER BY CreatedDate DESC";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", currentUser.UserId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvSupport.DataSource = dt;
                        
                        if (dgvSupport.Columns.Contains("ComplaintId")) dgvSupport.Columns["ComplaintId"].Visible = false;
                        if (dgvSupport.Columns.Contains("AdminReply")) dgvSupport.Columns["AdminReply"].Visible = false;
                        if (dgvSupport.Columns.Contains("ReplyDate")) dgvSupport.Columns["ReplyDate"].Visible = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading history: " + ex.Message); }
        }

        private void DgvSupport_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSupport.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSupport.SelectedRows[0].Cells["ComplaintId"].Value);
                LoadDetails(id);
            }
        }

        private void LoadDetails(int id)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Message, AdminReply, ReplyDate FROM Complaints WHERE ComplaintId = @id";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDetail.Text = reader["Message"].ToString();
                                string reply = reader["AdminReply"].ToString();
                                if (string.IsNullOrEmpty(reply))
                                {
                                    txtReply.Text = "Waiting for admin response... ⏳";
                                    txtReply.ForeColor = Color.Gray;
                                }
                                else
                                {
                                    string date = reader["ReplyDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReplyDate"]).ToString("MMM dd, yyyy HH:mm") : "";
                                    txtReply.Text = $"[Replied on {date}]\r\n\r\n{reply}";
                                    txtReply.ForeColor = Color.FromArgb(0, 77, 64);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
