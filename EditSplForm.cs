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
    public partial class EditSplForm : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        ArrayList supplierList = new ArrayList();
        public static Supplier splEditTarget = new Supplier();
        public static int idxSupplier;

        public EditSplForm()
        {
            InitializeComponent();
        }

        private void EditSplForm_Load(object sender, EventArgs e)
        {
            
            con.Open();

            cmd = new SqlCommand("SELECT * FROM tableSupplier", con);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ListViewItem item = new ListViewItem();
                item.Text = dataReader.GetInt32(0).ToString();
                item.SubItems.Add(dataReader.GetValue(1).ToString());
                item.SubItems.Add(dataReader.GetValue(2).ToString());
                item.SubItems.Add(dataReader.GetValue(3).ToString());
                listView1.Items.Add(item);
            }

            con.Close();
            cmd.Dispose();
            dataReader.Close();
        }

       

        private void editData_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int idSupplier = Int32.Parse(listView1.SelectedItems[0].Text);
                splEditTarget.getFromDatabase(idSupplier);
                EditSupplier form = new EditSupplier(this    );
                form.Show();               
            }
            
        }

        private void deleteData_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string idSupplier = listView1.SelectedItems[0].Text;
                DialogResult result = MessageBox.Show("Data ini sudah tersimpan dari database. Menghapus dari database ?", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Supplier splDelTarget = new Supplier();
                    splDelTarget.getFromDatabase(Int32.Parse(idSupplier));
                    if (splDelTarget.delFromDatabase() == false)
                    {
                        MessageBox.Show("Data ini tidak dapat dihapus karena terhubung ke Tabel Transaksi");
                    }
                    else
                    {
                        EditSplForm form = new EditSplForm();
                        form.Show();
                        this.Close();
                    }
                }
            }
            
        }
    }
}
