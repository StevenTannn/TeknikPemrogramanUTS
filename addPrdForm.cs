using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class addPrdForm : Form
    {

        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        int idMerek;
        public addPrdForm()
        {
            InitializeComponent();
        }

        private void addPrdForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (prdName.Text == null || string.IsNullOrWhiteSpace(prdName.Text) || prdRam.Text == null || string.IsNullOrWhiteSpace(prdRam.Text) || prdMerek.Text == null || string.IsNullOrWhiteSpace(prdMerek.Text))
            {
                MessageBox.Show("Mohon mengisi info produk secara lengkap");
            }
            else
            {


                ListViewItem item = new ListViewItem();
                item.Text = "auto generate";
                item.SubItems.Add(prdName.Text);
                item.SubItems.Add(idMerek.ToString());
                item.SubItems.Add(prdRam.Text);
                item.SubItems.Add(prdInternal.Text);
                item.SubItems.Add(prdPrice.Text);
                listView1.Items.Add(item);


            }
        
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
            DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (ListViewItem i in listView1.Items)
                {
                    Produk p = new Produk(i.Index, Int32.Parse(i.SubItems[2].Text), i.SubItems[1].Text, Int32.Parse(i.SubItems[3].Text), i.SubItems[4].Text, convertToByte(prdImg.Image), Int32.Parse(i.SubItems[5].Text));
                    p.addToDatabase();
                }

                listView1.Items.Clear();
            }
        }

        public byte[] convertToByte(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void deleteData_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {

                DialogResult result = MessageBox.Show("Data ini belum tersimpan ke database. Menghapus dari list ?", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    listView1.SelectedItems[0].Remove();
                }

            }
        }

        private void prdImg_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
