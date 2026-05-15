using System;
using System.Drawing;
using System.Windows.Forms;

namespace E_commerance_System.Forms
{
    public partial class FormAddStock : Form
    {
        public int StockToAdd { get; private set; }

        public FormAddStock(string productName, int currentStock)
        {
            InitializeComponent();
            lblInfo.Text = $"Product: {productName}\nCurrent Stock: {currentStock}";
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.StockToAdd = (int)numStock.Value;
        }
    }
}
