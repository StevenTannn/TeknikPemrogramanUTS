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
    public partial class EditMerek : Form
    {
        Merek mrkEditTarget;
        EditMrkForm form;
        public EditMerek(EditMrkForm form, Merek mrkEditTarget)
        {
            InitializeComponent();
            this.mrkEditTarget = mrkEditTarget;
            this.form = form;
        }

        private void EditMerek_Load(object sender, EventArgs e)
        {
            mrkName.Text = mrkEditTarget.getNamaMerek();
        }

        private void process_Click(object sender, EventArgs e)
        {
            if (mrkName.Text == null || string.IsNullOrWhiteSpace(mrkName.Text))
            {
                MessageBox.Show("Mohon isi nama Merek");
            }
            else
            {

                DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    mrkEditTarget.updToDatabase(mrkName.Text);
                    form.Close();
                    EditMrkForm editForm = new EditMrkForm();
                    editForm.Show();
                    this.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
