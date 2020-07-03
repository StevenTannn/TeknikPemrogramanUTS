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
    public partial class EditCusListview : Form
    {
        addCusForm form = new addCusForm();
        Customer cusEditTarget = addCusForm.cusEditTarget;
        public EditCusListview(addCusForm form, Customer cusEditTarget)
        {
            InitializeComponent();
            this.cusEditTarget = cusEditTarget;
            this.form = form;
        }

        private void EditSupplier_Load(object sender, EventArgs e)
        {
            cusName.Text = cusEditTarget.getNamaCustomer();
            cusAlamat.Text = cusEditTarget.getAlamat();
            cusPhone.Text = cusEditTarget.getNoTelp();
        }

        private void process_Click(object sender, EventArgs e)
        {
            if (cusName.Text == null || string.IsNullOrWhiteSpace(cusName.Text))
            {
                MessageBox.Show("Mohon isi nama perusahaan");
            }
            else
            {
                form.editCusListview(cusName.Text, cusAlamat.Text, cusPhone.Text);
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
