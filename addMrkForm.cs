using System;
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
    public partial class addMrkForm : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;
        public addMrkForm()
        {
            InitializeComponent();
        }

        private void addMrkForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mrkName.Text == null || string.IsNullOrWhiteSpace(mrkName.Text))
            {
                MessageBox.Show("Mohon mengisi nama merek");
            }
            else
            {
                Boolean doubleCustomer = false;
                string name = mrkName.Text.ToUpper();

                foreach (ListViewItem i in listView1.Items)
                {
                    doubleCustomer = name == i.SubItems[1].Text.ToUpper();
                    if (doubleCustomer)
                    {
                        MessageBox.Show("Nama merek sudah terdapat di listview");
                        break;
                    }
                }

                if (!doubleCustomer)
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT namaMerek FROM tableMerek", con);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        doubleCustomer = name == dataReader.GetString(0).ToUpper();
                        if (doubleCustomer)
                        {
                            MessageBox.Show("Nama merek sudah terdapat di database");
                            break;
                        }
                    }
                    if (!doubleCustomer)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = "auto generate";
                        item.SubItems.Add(mrkName.Text);
                        listView1.Items.Add(item);
                        mrkName.Text = "";
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
                    Merek m = new Merek(i.Index, i.SubItems[1].Text);
                    m.addToDatabase();
                }
                listView1.Items.Clear();
            }
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
    }
}
