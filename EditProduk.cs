using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class EditProduk : Form
    {
        Produk prdEditTarget;
        EditPrdForm form;
        int idMerek;
        public EditProduk(EditPrdForm form, Produk prdEditTarget)
        {
            InitializeComponent();
            this.form = form;
            this.prdEditTarget = prdEditTarget;
        }

        private void EditProduk_Load(object sender, EventArgs e)
        {
            prdName.Text = prdEditTarget.getNamaProduk();
            prdMerek.Text = prdEditTarget.getNamaMerek();
            prdRam.Text = prdEditTarget.getRAM().ToString();
            prdInternal.Text = prdEditTarget.getMInternal();
            prdPrice.Text = prdEditTarget.getHargaPrd().ToString();
            try { prdImg.Image = prdEditTarget.getImgPrd(); } catch { }
            idMerek = prdEditTarget.getIdMerek();
        }

        private void browseMerek_Click(object sender, EventArgs e)
        {
            browseMerek form = new browseMerek(this);
            form.Show();
        }

        public void writeMerek(int idMerek, string namaMerek)
        {
            prdMerek.Text = namaMerek;
            this.idMerek = idMerek;
        }

        private void browseImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*.JPG|*.jpg";

            if (f.ShowDialog() == DialogResult.OK)
            {
                Image File = Image.FromFile(f.FileName);
                prdImg.Image = File;
            }
        }

        private void process_Click(object sender, EventArgs e)
        {
            if (prdName.Text == null || string.IsNullOrWhiteSpace(prdName.Text))
            {
                MessageBox.Show("Mohon isi nama perusahaan");
            }
            else
            {

                DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    prdEditTarget.updToDatabase(idMerek, prdName.Text, Int32.Parse(prdRam.Text), prdInternal.Text, convertToByte(prdImg.Image), Int32.Parse(prdPrice.Text));
                    form.Close();
                    EditPrdForm editForm = new EditPrdForm();
                    editForm.Show();
                    this.Close();
                }
            }
        }

        public byte[] convertToByte(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
