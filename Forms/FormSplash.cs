using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace E_commerance_System.Forms
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            InitializeComponent();
            this.Paint += FormSplash_Paint;
            this.Load += (s, e) => timerSplash.Start();
        }

        private void FormSplash_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle, Color.FromArgb(26, 35, 126), Color.FromArgb(63, 81, 181), System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void TimerSplash_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < 100)
            {
                progressBar.Value += 2;
                if (progressBar.Value == 40) lblStatus.Text = "Loading system modules...";
                if (progressBar.Value == 70) lblStatus.Text = "Connecting to SQL Server...";
                if (progressBar.Value == 90) lblStatus.Text = "Ready!";
            }
            else
            {
                timerSplash.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
