using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Menu1 : Form
    {
        public Menu1()
        {
            InitializeComponent();
            this.FormClosing += Menu1_FormClosing;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            StockMasuk stockMasuk = new StockMasuk();
            stockMasuk.Show();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            StockKeluar stockKeluar = new StockKeluar();
            stockKeluar.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowStock form = new ShowStock();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void Menu1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

    }
}
