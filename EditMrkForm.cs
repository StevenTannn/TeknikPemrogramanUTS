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
    public partial class EditMrkForm : Form
    {
        Merek mrkEditTarget = new Merek();

        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;
        public EditMrkForm()
        {
            InitializeComponent();
        }

        private void EditMrkForm_Load(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM tableMerek", con);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ListViewItem item = new ListViewItem();
                item.Text = dataReader.GetInt32(0).ToString();
                item.SubItems.Add(dataReader.GetValue(1).ToString());
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
                string idMerek = listView1.SelectedItems[0].Text;
                DialogResult result = MessageBox.Show("Data ini sudah tersimpan dari database. Menghapus dari database ?", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Merek mrkDelTarget = new Merek();
                    mrkDelTarget.getFromDatabase(Int32.Parse(idMerek));
                    if (mrkDelTarget.delFromDatabase() == false)
                    {
                        MessageBox.Show("Data ini tidak dapat dihapus karena terhubung ke Tabel Database lain");
                    }
                    else
                    {
                        EditMrkForm form = new EditMrkForm();
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
                int idCustomer = Int32.Parse(listView1.SelectedItems[0].Text);
                mrkEditTarget.getFromDatabase(idCustomer);
                EditMerek form = new EditMerek(this, mrkEditTarget);
                form.Show();
            }
        }
    }
}
