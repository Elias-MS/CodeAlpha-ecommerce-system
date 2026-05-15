namespace E_commerance_System.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPortfolio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMission = new System.Windows.Forms.Panel();
            this.lblAboutTitle = new System.Windows.Forms.Label();
            this.lblMission = new System.Windows.Forms.Label();
            this.pnlHero = new System.Windows.Forms.Panel();
            this.btnGuest = new System.Windows.Forms.Button();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.btnShopNow = new System.Windows.Forms.Button();
            this.pnlFeatures = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.pnlMission.SuspendLayout();
            this.pnlHero.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(0, 121, 107);
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLogin,
            this.menuRegister,
            this.menuPortfolio,
            this.menuExit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(10);
            this.menuStrip.Size = new System.Drawing.Size(1182, 59);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuLogin
            // 
            this.menuLogin.Name = "menuLogin";
            this.menuLogin.Size = new System.Drawing.Size(95, 39);
            this.menuLogin.Text = "🔑 Login";
            this.menuLogin.Click += new System.EventHandler(this.MenuLogin_Click);
            // 
            // menuRegister
            // 
            this.menuRegister.Name = "menuRegister";
            this.menuRegister.Size = new System.Drawing.Size(117, 39);
            this.menuRegister.Text = "📝 Register";
            this.menuRegister.Click += new System.EventHandler(this.MenuRegister_Click);
            // 
            // menuPortfolio
            // 
            this.menuPortfolio.Name = "menuPortfolio";
            this.menuPortfolio.Size = new System.Drawing.Size(119, 39);
            this.menuPortfolio.Text = "🖼️ Portfolio";
            this.menuPortfolio.Click += new System.EventHandler(this.MenuPortfolio_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(81, 39);
            this.menuExit.Text = "❌ Exit";
            this.menuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // pnlMission
            // 
            this.pnlMission.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.pnlMission.Controls.Add(this.lblMission);
            this.pnlMission.Controls.Add(this.lblAboutTitle);
            this.pnlMission.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMission.Location = new System.Drawing.Point(0, 59);
            this.pnlMission.Name = "pnlMission";
            this.pnlMission.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlMission.Size = new System.Drawing.Size(1182, 100);
            this.pnlMission.TabIndex = 1;
            // 
            // lblAboutTitle
            // 
            this.lblAboutTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAboutTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAboutTitle.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblAboutTitle.Location = new System.Drawing.Point(20, 10);
            this.lblAboutTitle.Name = "lblAboutTitle";
            this.lblAboutTitle.Size = new System.Drawing.Size(1142, 30);
            this.lblAboutTitle.TabIndex = 0;
            this.lblAboutTitle.Text = "📖 About Our System";
            this.lblAboutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMission
            // 
            this.lblMission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMission.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblMission.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.lblMission.Location = new System.Drawing.Point(20, 40);
            this.lblMission.Name = "lblMission";
            this.lblMission.Size = new System.Drawing.Size(1142, 50);
            this.lblMission.TabIndex = 1;
            this.lblMission.Text = "The main goal of the E-Commerce System is to design and develop an online platfor" +
    "m that enables customers to browse...";
            this.lblMission.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHero
            // 
            this.pnlHero.BackColor = System.Drawing.Color.Transparent;
            this.pnlHero.Controls.Add(this.btnGuest);
            this.pnlHero.Controls.Add(this.lblSlogan);
            this.pnlHero.Controls.Add(this.lblSub);
            this.pnlHero.Controls.Add(this.btnShopNow);
            this.pnlHero.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHero.Location = new System.Drawing.Point(0, 159);
            this.pnlHero.Name = "pnlHero";
            this.pnlHero.Size = new System.Drawing.Size(1182, 320);
            this.pnlHero.TabIndex = 2;
            // 
            // btnGuest
            // 
            this.btnGuest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.btnGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuest.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuest.ForeColor = System.Drawing.Color.White;
            this.btnGuest.Location = new System.Drawing.Point(500, 260);
            this.btnGuest.Name = "btnGuest";
            this.btnGuest.Size = new System.Drawing.Size(200, 40);
            this.btnGuest.TabIndex = 3;
            this.btnGuest.Text = "Browse as Guest";
            this.btnGuest.UseVisualStyleBackColor = true;
            this.btnGuest.Click += new System.EventHandler(this.BtnGuest_Click);
            // 
            // lblSlogan
            // 
            this.lblSlogan.BackColor = System.Drawing.Color.Transparent;
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblSlogan.Location = new System.Drawing.Point(0, 50);
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.Size = new System.Drawing.Size(1182, 70);
            this.lblSlogan.TabIndex = 0;
            this.lblSlogan.Text = "Your Ultimate Shopping Destination";
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSub
            // 
            this.lblSub.BackColor = System.Drawing.Color.Transparent;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblSub.ForeColor = System.Drawing.Color.LightGray;
            this.lblSub.Location = new System.Drawing.Point(0, 130);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(1182, 30);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Explore thousands of products with secure payments and fast delivery.";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShopNow
            // 
            this.btnShopNow.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnShopNow.FlatAppearance.BorderSize = 0;
            this.btnShopNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShopNow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnShopNow.ForeColor = System.Drawing.Color.Black;
            this.btnShopNow.Location = new System.Drawing.Point(500, 200);
            this.btnShopNow.Name = "btnShopNow";
            this.btnShopNow.Size = new System.Drawing.Size(200, 50);
            this.btnShopNow.TabIndex = 2;
            this.btnShopNow.Text = "Start Shopping Now";
            this.btnShopNow.UseVisualStyleBackColor = false;
            // 
            // pnlFeatures
            // 
            this.pnlFeatures.AutoScroll = true;
            this.pnlFeatures.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.pnlFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeatures.Location = new System.Drawing.Point(0, 479);
            this.pnlFeatures.Name = "pnlFeatures";
            this.pnlFeatures.Padding = new System.Windows.Forms.Padding(50, 20, 50, 20);
            this.pnlFeatures.Size = new System.Drawing.Size(1182, 154);
            this.pnlFeatures.TabIndex = 3;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.pnlFooter.Controls.Add(this.lblCopyright);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 633);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1182, 120);
            this.pnlFooter.TabIndex = 4;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCopyright.ForeColor = System.Drawing.Color.DimGray;
            this.lblCopyright.Location = new System.Drawing.Point(0, 90);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(1182, 30);
            this.lblCopyright.TabIndex = 0;
            this.lblCopyright.Text = "© 2024 E-Commerce System. All Rights Reserved.";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.pnlFeatures);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHero);
            this.Controls.Add(this.pnlMission);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "E-Commerce Management System — Welcome";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlMission.ResumeLayout(false);
            this.pnlHero.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuLogin;
        private System.Windows.Forms.ToolStripMenuItem menuRegister;
        private System.Windows.Forms.ToolStripMenuItem menuPortfolio;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.Panel pnlMission;
        private System.Windows.Forms.Label lblAboutTitle;
        private System.Windows.Forms.Label lblMission;
        private System.Windows.Forms.Panel pnlHero;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Button btnShopNow;
        private System.Windows.Forms.Button btnGuest;
        private System.Windows.Forms.FlowLayoutPanel pnlFeatures;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCopyright;
    }
}
