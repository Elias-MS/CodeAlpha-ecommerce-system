using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Models;
using E_commerance_System.Services;

// ============================================================
// FORM: LOGIN — Entry Point of the Application
// ============================================================

namespace E_commerance_System.Forms
{
    public partial class FormLogin : Form
    {
        private readonly Color PrimaryColor = Color.FromArgb(26, 35, 126);
        private readonly Color AccentColor = Color.FromArgb(0, 137, 123);
        private readonly Color DangerColor = Color.FromArgb(211, 47, 47);
        private readonly Color LightBg = Color.FromArgb(245, 245, 250);
        private readonly Color CardBg = Color.White;

        public FormLogin() 
        { 
            InitializeComponent(); 
            AddForgotPasswordLink();
        }

        private void AddForgotPasswordLink()
        {
            var lnkForgot = new LinkLabel
            {
                Text = "Forgot Password?",
                Location = new Point(50, 420), // Positioned below login fields/buttons
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                LinkColor = PrimaryColor,
                ActiveLinkColor = AccentColor,
                Cursor = Cursors.Hand
            };
            lnkForgot.LinkClicked += (s, e) =>
            {
                var forgotForm = new FormForgotPassword();
                forgotForm.ShowDialog(this);
            };
            this.Controls.Add(lnkForgot);
            lnkForgot.BringToFront();
        }

        private void LnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var reg = new FormRegister();
            if (reg.ShowDialog(this) == DialogResult.OK)
            {
                txtUsername.Text = reg.RegisteredUsername;
                txtPassword.Text = reg.RegisteredPassword;
                lblStatus.Text = "✨ Registration successful! Logging in...";
                lblStatus.ForeColor = AccentColor;
                BtnLogin_Click(null, null); // Automatically trigger login
            }
        }

        private void PnlHeader_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(pnlHeader.ClientRectangle, PrimaryColor, Color.FromArgb(40, 53, 147), LinearGradientMode.Horizontal))
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblStatus.Text = "⚠️ Please enter your credentials.";
                return;
            }
        
            try
            {
                lblStatus.Text = "Authenticating secure session...";
                lblStatus.ForeColor = Color.Gray;
                Application.DoEvents();
        
                // 1. Try Admin Authentication (Hidden from public UI)
                Admin admin = AdminService.AuthenticateAdmin(txtUsername.Text.Trim(), txtPassword.Text);
                if (admin != null)
                {
                    lblStatus.Text = "✅ Admin access granted!";
                    lblStatus.ForeColor = AccentColor;
                    this.Hide();
                    var adminDashboard = new FormAdminDashboard(admin);
                    adminDashboard.FormClosed += (s, ev) => { txtPassword.Clear(); lblStatus.Text = ""; this.Show(); };
                    adminDashboard.Show();
                    return;
                }
        
                // 2. Try Customer Authentication
                User user = UserService.AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text);
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        if (MessageBox.Show("Your account is deactivated. Please contact the administrator to resolve this issue. \n\nWould you like to send a report now?", "Inactive Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string userMessage = ShowInputDialog("Create Support Report", "Please describe your issue:");
                            if (!string.IsNullOrWhiteSpace(userMessage))
                            {
                                if (NotificationService.SendReportToAdmin(user.UserId, "Deactivated Account Issue", $"User '{user.Username}' says: {userMessage}"))
                                {
                                    MessageBox.Show("✅ Your report has been sent to the admin! Please wait for review.", "Report Sent");
                                }
                                else
                                {
                                    MessageBox.Show("❌ Failed to send report. Please try again later.", "Error");
                                }
                            }
                        }
                        lblStatus.Text = "❌ Account deactivated.";
                        lblStatus.ForeColor = DangerColor;
                        return;
                    }
        
                    lblStatus.Text = "✅ Welcome back!";
                    lblStatus.ForeColor = AccentColor;
                    this.Hide();
                    var dashboard = new FormCustomerDashboard(user);
                    dashboard.FormClosed += (s, ev) => { txtPassword.Clear(); lblStatus.Text = ""; this.Show(); };
                    dashboard.Show();
                }
                else 
                { 
                    lblStatus.Text = "❌ Invalid username or password."; 
                    lblStatus.ForeColor = DangerColor; 
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ " + ex.Message;
                lblStatus.ForeColor = DangerColor;
            }
        }
        private string ShowInputDialog(string title, string prompt)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();

            form.Text = title;
            label.Text = prompt;
            
            buttonOk.Text = "Send Report";
            buttonOk.DialogResult = DialogResult.OK;

            label.SetBounds(20, 20, 360, 25);
            textBox.SetBounds(20, 50, 360, 100);
            textBox.Multiline = true;
            buttonOk.SetBounds(280, 160, 100, 30);

            form.ClientSize = new Size(400, 210);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.AcceptButton = buttonOk;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
