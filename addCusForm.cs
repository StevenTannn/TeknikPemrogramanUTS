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
    public partial class addCusForm : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        ArrayList customerList = new ArrayList();
        public static Customer cusEditTarget = new Customer();
        public addCusForm()
        {
            InitializeComponent();
        }

        private void addCusForm_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cusName.Text == null || string.IsNullOrWhiteSpace(cusName.Text) || cusPhone.Text == null || string.IsNullOrWhiteSpace(cusPhone.Text) || cusAlamat.Text == null || string.IsNullOrWhiteSpace(cusAlamat.Text))
            {
                MessageBox.Show("Mohon mengisi lengkap nama, alamat, dan no telepon perusahaan Customer.");
            }
            else
            {
                Boolean doubleCustomer = false;
               

                string name = cusName.Text.ToUpper();

                foreach (ListViewItem i in listView1.Items)
                {
                    doubleCustomer = name == i.SubItems[1].Text.ToUpper();
                    if (doubleCustomer)
                    {
                        MessageBox.Show("Nama perusahaan sudah terdapat di listview");
                        break;
                    }
                }

                

                if (!doubleCustomer)
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT namaCustomer FROM tableCustomer", con);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        doubleCustomer = name == dataReader.GetString(0).ToUpper();
                        if (doubleCustomer)
                        {
                            MessageBox.Show("Nama perusahaan sudah terdapat di database");
                            break;
                        }
                    }
                    if (!doubleCustomer)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = "auto generate";
                        item.SubItems.Add(cusName.Text);
                        item.SubItems.Add(cusAlamat.Text);
                        item.SubItems.Add(cusPhone.Text);
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
                    Customer c = new Customer(i.Index , i.SubItems[1].Text, i.SubItems[2].Text, i.SubItems[3].Text);
                    c.addToDatabase();
                }

                listView1.Items.Clear();
            }

        }

        private void editData_Click(object sender, EventArgs e)
        {
            ListViewItem i = listView1.SelectedItems[0];
            if (listView1.SelectedItems.Count > 0)
            {
                cusEditTarget = new Customer();
                cusEditTarget.setNamaCustomer(i.SubItems[1].Text);
                cusEditTarget.setAlamat(i.SubItems[2].Text);
                cusEditTarget.setAlamat(i.SubItems[3].Text);
                EditCusListview form = new EditCusListview(this, cusEditTarget);
                form.Show();

            }

        }

        public void editCusListview(string namaCustomer, string alamat, string noTelp)
        {
            listView1.SelectedItems[0].SubItems[1].Text = namaCustomer;
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


        private void addCusForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
