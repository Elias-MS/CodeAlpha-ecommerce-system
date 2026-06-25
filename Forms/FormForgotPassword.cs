using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public class FormForgotPassword : Form
    {
        private Panel pnlMain;
        private Label lblTitle, lblSubTitle, lblStatus;
        private TextBox txtEmail, txtCode, txtNewPassword, txtConfirmPassword;
        private Button btnSendCode, btnVerifyCode, btnResetPassword;
        private LinkLabel lnkBackToLogin;

        private readonly Color PrimaryColor = Color.FromArgb(26, 35, 126);
        private readonly Color AccentColor = Color.FromArgb(0, 137, 123);
        private readonly Color DangerColor = Color.FromArgb(211, 47, 47);

        private string generatedCode = "";
        private int verifiedUserId = 0;
        private bool isUserAdmin = false;

        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(245, 245, 250);

            // Close button
            var btnClose = new Button { Text = "✕", Location = new Point(450, 10), Size = new Size(40, 40), FlatStyle = FlatStyle.Flat, ForeColor = Color.Gray, Font = new Font("Segoe UI", 12F, FontStyle.Bold), Cursor = Cursors.Hand };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            // Main Panel
            pnlMain = new Panel
            {
                Location = new Point(50, 50),
                Size = new Size(400, 500),
                BackColor = Color.White
            };
            pnlMain.Paint += PnlMain_Paint;

            lblTitle = new Label { Text = "Reset Password", Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = PrimaryColor, Location = new Point(30, 30), AutoSize = true };
            lblSubTitle = new Label { Text = "Enter your email to receive a verification code.", Font = new Font("Segoe UI", 10F), ForeColor = Color.Gray, Location = new Point(35, 75), AutoSize = true };
            lblStatus = new Label { Text = "", Font = new Font("Segoe UI Semibold", 9F), ForeColor = DangerColor, Location = new Point(35, 100), AutoSize = true, MaximumSize = new Size(330, 0) };

            txtEmail = CreateTextBox("Enter your registered email address", 130);
            btnSendCode = CreateButton("Send Verification Code", 190, AccentColor);
            btnSendCode.Click += BtnSendCode_Click;

            txtCode = CreateTextBox("Enter 6-digit code", 130);
            txtCode.Visible = false;
            btnVerifyCode = CreateButton("Verify Code", 190, PrimaryColor);
            btnVerifyCode.Visible = false;
            btnVerifyCode.Click += BtnVerifyCode_Click;

            txtNewPassword = CreateTextBox("Enter new password", 130);
            txtNewPassword.PasswordChar = '●';
            txtNewPassword.Visible = false;

            txtConfirmPassword = CreateTextBox("Confirm new password", 190);
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Visible = false;

            btnResetPassword = CreateButton("Reset Password", 250, AccentColor);
            btnResetPassword.Visible = false;
            btnResetPassword.Click += BtnResetPassword_Click;

            lnkBackToLogin = new LinkLabel { Text = "Back to Login", Location = new Point(155, 450), Font = new Font("Segoe UI Semibold", 9F), LinkColor = PrimaryColor, AutoSize = true };
            lnkBackToLogin.LinkClicked += (s, e) => this.Close();

            pnlMain.Controls.AddRange(new Control[] { lblTitle, lblSubTitle, lblStatus, txtEmail, btnSendCode, txtCode, btnVerifyCode, txtNewPassword, txtConfirmPassword, btnResetPassword, lnkBackToLogin });
            this.Controls.Add(pnlMain);
        }

        private TextBox CreateTextBox(string placeholder, int y)
        {
            var txt = new TextBox
            {
                Location = new Point(35, y),
                Size = new Size(330, 40),
                Font = new Font("Segoe UI", 12F),
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Gray,
                Text = placeholder
            };
            txt.Enter += (s, e) => { if (txt.Text == placeholder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = placeholder; txt.ForeColor = Color.Gray; } };
            return txt;
        }

        private Button CreateButton(string text, int y, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(35, y),
                Size = new Size(330, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void PnlMain_Paint(object sender, PaintEventArgs e)
        {
            using (Pen p = new Pen(Color.LightGray, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, pnlMain.Width - 1, pnlMain.Height - 1);
            }
        }

        private void ShowStatus(string msg, bool isError = true)
        {
            lblStatus.Text = msg;
            lblStatus.ForeColor = isError ? DangerColor : AccentColor;
        }

        private void BtnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email) || email == "Enter your registered email address")
            {
                ShowStatus("Please enter an email address.");
                return;
            }

            ShowStatus("Searching for account...", false);
            Application.DoEvents();

            verifiedUserId = UserService.GetUserIdByEmail(email);
            if (verifiedUserId > 0)
            {
                isUserAdmin = false;
            }
            else
            {
                verifiedUserId = AdminService.GetAdminIdByEmail(email);
                if (verifiedUserId > 0)
                {
                    isUserAdmin = true;
                }
            }

            if (verifiedUserId == 0)
            {
                ShowStatus("No account found with that email address.");
                return;
            }

            // Generate 6-digit code
            var rand = new Random();
            generatedCode = rand.Next(100000, 999999).ToString();

            ShowStatus("Sending email...", false);
            Application.DoEvents();

            if (EmailService.SendVerificationCode(email, generatedCode))
            {
                ShowStatus("Code sent! Please check your email.", false);
            }
            else
            {
                // Fallback for testing/simulation when no real email is configured
                MessageBox.Show($"[SYSTEM SIMULATION]\n\nSince no SMTP email is configured in App.config, here is your verification code:\n\n{generatedCode}", "Verification Code Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowStatus("Simulation Mode: Code displayed on screen.", false);
            }

            lblSubTitle.Text = "Enter the 6-digit code sent to your email.";
            
            txtEmail.Visible = false;
            btnSendCode.Visible = false;
            
            txtCode.Visible = true;
            btnVerifyCode.Visible = true;
        }

        private void BtnVerifyCode_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            if (code != generatedCode)
            {
                ShowStatus("Invalid or incorrect code. Try again.");
                return;
            }

            ShowStatus("Code verified! Please enter your new password.", false);
            lblSubTitle.Text = "Create a new strong password.";
            
            txtCode.Visible = false;
            btnVerifyCode.Visible = false;

            txtNewPassword.Visible = true;
            txtConfirmPassword.Visible = true;
            btnResetPassword.Visible = true;
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            string pass = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (pass != confirm)
            {
                ShowStatus("Passwords do not match.");
                return;
            }

            if (pass.Length < 8)
            {
                ShowStatus("Password must be at least 8 characters.");
                return;
            }

            try
            {
                bool success = false;
                if (isUserAdmin)
                {
                    success = AdminService.ResetAdminPasswordDirect(verifiedUserId, pass);
                }
                else
                {
                    success = UserService.ResetPasswordDirect(verifiedUserId, pass);
                }

                if (success)
                {
                    MessageBox.Show("✅ Password successfully reset!\nYou can now log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    ShowStatus("Failed to reset password. Security check failed.");
                }
            }
            catch (Exception ex)
            {
                ShowStatus("Error: " + ex.Message);
            }
        }
    }
}
