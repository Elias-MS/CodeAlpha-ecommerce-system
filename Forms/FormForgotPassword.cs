using System;
using System.Drawing;
using System.Windows.Forms;
using E_commerance_System.Services;

namespace E_commerance_System.Forms
{
    public partial class FormForgotPassword : Form
    {
        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, EventArgs e) => this.Close();

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            // In a real app, you'd check DB and send an email. 
            // For this project, we'll simulate a reset.
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = "Reset link sent! Please check your email.";
            btnSubmit.Enabled = false;
        }
    }
}
