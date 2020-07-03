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
    public partial class EditSplListview : Form
    {
        addSplForm form = new addSplForm();
        Supplier splEditTarget = addSplForm.splEditTarget;
        public EditSplListview(addSplForm form, Supplier splEditTarget)
        {
            InitializeComponent();
            this.splEditTarget = splEditTarget;
            this.form = form;
        }

        private void EditSupplier_Load(object sender, EventArgs e)
        {
            splName.Text = splEditTarget.getNamaSupplier();
            splAlamat.Text = splEditTarget.getAlamat();
            splPhone.Text = splEditTarget.getNoTelp();
        }

        private void process_Click(object sender, EventArgs e)
        {
            if (splName.Text == null || string.IsNullOrWhiteSpace(splName.Text))
            {
                MessageBox.Show("Mohon isi nama perusahaan");
            }
            else
            {
                form.editSplListview(splName.Text, splAlamat.Text, splPhone.Text);
                this.Close();
            }  
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void splAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void splName_TextChanged(object sender, EventArgs e)
        {

        }

        private void splPhone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
