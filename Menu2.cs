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
    public partial class Menu2 : Form
    {
        public Menu2()
        {
            InitializeComponent();
            this.FormClosing += Menu2_FormClosing;
        }

        private void editSpl_Click(object sender, EventArgs e)
        {
            EditSplForm form = new EditSplForm();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSplForm form = new addSplForm();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EditCusForm form = new EditCusForm();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            addCusForm form = new addCusForm();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditPrdForm form = new EditPrdForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addPrdForm form = new addPrdForm();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditMrkForm form = new EditMrkForm();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addMrkForm form = new addMrkForm();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowTransaction form = new ShowTransaction();
            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowStock form = new ShowStock();
            form.Show();
        }

        private void Menu2_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }
        private void Menu2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

    }
}
