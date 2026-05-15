using System;
using System.Windows.Forms;
using E_commerance_System.Data;
using E_commerance_System.Forms;

namespace E_commerance_System
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Initialize Database
            try 
            { 
                DatabaseHelper.InitializeDatabase(); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Database Initialization Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return; 
            }

            // 2. Show Splash Screen
            using (var splash = new FormSplash())
            {
                if (splash.ShowDialog() == DialogResult.OK)
                {
                    // 3. Start Main Landing Page
                    Application.Run(new FormMain());
                }
            }
        }
    }
}
