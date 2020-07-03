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
    public partial class EditCustomer : Form
    {
        EditCusForm form;
        Customer cusEditTarget;
        public EditCustomer(EditCusForm form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void EditCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private void EditCustomer_Load(object sender, EventArgs e)
        {
            cusEditTarget = EditCusForm.cusEditTarget;
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

                DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    cusEditTarget.updToDatabase(cusName.Text, cusAlamat.Text, cusPhone.Text);
                    form.Close();
                    EditCusForm editForm = new EditCusForm();
                    editForm.Show();
                    this.Close();
                }
            }

        }


    }
}
