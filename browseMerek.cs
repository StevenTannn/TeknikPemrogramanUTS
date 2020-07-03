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
    public partial class browseMerek : Form
    {
        Form form;
        string formActive;
        public browseMerek(addPrdForm addForm)
        {
            InitializeComponent();
            form = addForm;
            formActive = "addForm";
        }

        public browseMerek(EditProduk editForm)
        {
            InitializeComponent();
            form = editForm;
            formActive = "editForm";
        }

        private void browseMerek_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
            con.Open();
            SqlCommand cmd;
            SqlDataReader dataReader;

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

        private void btnLanjut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int idMerek = Int32.Parse(listView1.SelectedItems[0].Text);
                string namaMerek = listView1.SelectedItems[0].SubItems[1].Text;
                if(formActive == "addForm") ((addPrdForm)form).writeMerek(idMerek, namaMerek);
                else if (formActive == "editForm") ((EditProduk)form).writeMerek(idMerek, namaMerek);
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
