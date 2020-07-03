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
    public partial class ShowStock : Form
    {

        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;
        public ShowStock()
        {
            InitializeComponent();
        }

        private void ShowStock_Load(object sender, EventArgs e)
        {
            con.Open();

            cmd = new SqlCommand($"SELECT tableProduk.idProduk, namaProduk, namaMerek, RAM, mInternal, hargaPrd, prdStock FROM tableProduk LEFT JOIN tableMerek ON(tableProduk.idMerek = tableMerek.idMerek) JOIN tableStock ON(tableProduk.idProduk = tableStock.idProduk)", con);
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
                item.SubItems.Add(dataReader.GetValue(6).ToString());
                listView1.Items.Add(item);
            }

            con.Close();
            cmd.Dispose();
            dataReader.Close();
        }
    }
}
