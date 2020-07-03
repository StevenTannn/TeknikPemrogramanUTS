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
    public partial class EditPrdForm : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        public static Produk prdEditTarget = new Produk();
        public EditPrdForm()
        {
            InitializeComponent();
        }

        private void EditPrdForm_Load(object sender, EventArgs e)
        {
            con.Open();

            cmd = new SqlCommand($"SELECT idProduk, namaProduk, namaMerek, RAM, mInternal, hargaPrd FROM tableProduk LEFT JOIN tableMerek ON(tableProduk.idMerek = tableMerek.idMerek)", con);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ListViewItem item = new ListViewItem();
                item.Text = dataReader.GetInt32(0).ToString();
                item.SubItems.Add(dataReader.GetValue(1).ToString());
                item.SubItems.Add(dataReader.GetValue(2).ToString());
                item.SubItems.Add(dataReader.GetValue(3).ToString());
                item.SubItems.Add(dataReader.GetValue(4).ToString());
                item.SubItems.Add(dataReader.GetValue(5).ToString());
                listView1.Items.Add(item);
            }

            con.Close();
            cmd.Dispose();
            dataReader.Close();
        }

        private void deleteData_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string idProduk = listView1.SelectedItems[0].Text;
                DialogResult result = MessageBox.Show("Data ini sudah tersimpan dari database. Menghapus dari database ?", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Produk prdDelTarget = new Produk();
                    prdDelTarget.getFromDatabase(Int32.Parse(idProduk));
                    if (prdDelTarget.delFromDatabase() == false)
                    {
                        MessageBox.Show("Data ini tidak dapat dihapus karena terhubung ke Tabel Transaksi");
                    }
                    else
                    {
                        EditPrdForm form = new EditPrdForm();
                        form.Show();
                        this.Close();
                    }
                }
            }
        }

        private void editData_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int idProduk = Int32.Parse(listView1.SelectedItems[0].Text);
                prdEditTarget.getFromDatabase(idProduk);
                EditProduk form = new EditProduk(this, prdEditTarget);
                form.Show();
            }
        }
    }
}
