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
    public partial class EditSupplier : Form
    {
        EditSplForm form;
        Supplier splEditTarget;
        public EditSupplier(EditSplForm form)
        {
            InitializeComponent();
            this.form = form; 
        }

        private void EditSupplier_FormClosing(object sender, FormClosingEventArgs e)
        {
        }


        private void EditSupplier_Load(object sender, EventArgs e)
        {
            splEditTarget = EditSplForm.splEditTarget;
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

                DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    splEditTarget.updToDatabase(splName.Text, splAlamat.Text, splPhone.Text);
                    form.Close();
                    EditSplForm editForm = new EditSplForm();
                    editForm.Show();
                    this.Close();
                }
            }
            
        }


    }
}
