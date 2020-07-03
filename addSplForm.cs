using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class addSplForm : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        ArrayList supplierList = new ArrayList();
        public static Supplier splEditTarget = new Supplier();
        public addSplForm()
        {
            InitializeComponent();
        }

        private void addSplForm_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (splName.Text == null || string.IsNullOrWhiteSpace(splName.Text) || splPhone.Text == null || string.IsNullOrWhiteSpace(splPhone.Text) || splAlamat.Text == null || string.IsNullOrWhiteSpace(splAlamat.Text))
            {
                MessageBox.Show("Mohon mengisi lengkap nama, alamat, dan no telepon perusahaan supplier.");
            }
            else
            {
                Boolean doubleSupplier = false;
                

                string name = splName.Text.ToUpper();
                foreach (ListViewItem i in listView1.Items)
                {
                    doubleSupplier = name == i.SubItems[1].Text.ToUpper();
                    if (doubleSupplier)
                    {
                        MessageBox.Show("Nama perusahaan sudah terdapat di listview");
                        break;
                    }
                }
                
                if (!doubleSupplier)
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT namaSupplier FROM tableSupplier", con);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        doubleSupplier = name == dataReader.GetString(0).ToUpper();
                        if (doubleSupplier)
                        {
                            MessageBox.Show("Nama perusahaan sudah terdapat di database");
                            break;
                        }
                    }
                    if (!doubleSupplier)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = "auto generate";
                        item.SubItems.Add(splName.Text);
                        item.SubItems.Add(splAlamat.Text);
                        item.SubItems.Add(splPhone.Text);
                        listView1.Items.Add(item);
                    }
                    con.Close();
                    cmd.Dispose();
                    dataReader.Close();


                }


            }
        }
        private void process_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (ListViewItem i in listView1.Items)
                {
                    Supplier s = new Supplier(i.Index, i.SubItems[1].Text, i.SubItems[2].Text, i.SubItems[3].Text);
                    s.addToDatabase();
                }

                listView1.Items.Clear();
            }

        }

        private void editData_Click(object sender, EventArgs e)
        {
            ListViewItem i = listView1.SelectedItems[0];
            if (listView1.SelectedItems.Count > 0)
            {
                splEditTarget  = new Supplier();
                splEditTarget.setNamaSupplier(i.SubItems[1].Text);
                splEditTarget.setAlamat(i.SubItems[2].Text);
                splEditTarget.setAlamat(i.SubItems[3].Text);
                EditSplListview form = new EditSplListview(this, splEditTarget);
                form.Show();
                
            }

        }

        public void editSplListview(string namaSupplier, string alamat, string noTelp)
        {
            listView1.SelectedItems[0].SubItems[1].Text = namaSupplier;
            listView1.SelectedItems[0].SubItems[2].Text = alamat;
            listView1.SelectedItems[0].SubItems[3].Text = noTelp;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
