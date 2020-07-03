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
    public partial class ShowTransaction : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;
        public ShowTransaction()
        {
            InitializeComponent();
        }

        private void ShowTransaction_Load(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT sm.idTransaksi, p.namaProduk, m.namaMerek, s.namaSupplier, sm.qty, sm.tanggal, sm.hargaTransaksi FROM tableStockMasuk AS sm LEFT JOIN tableProduk AS p ON(sm.idProduk = p.idProduk) LEFT JOIN tableSupplier AS s ON(sm.idSupplier = s.idSupplier) JOIN tableMerek AS m ON(p.idMerek = m.idMerek);", con);
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
                listViewSM.Items.Add(item);
            }
            cmd.Dispose();
            dataReader.Close();

            cmd = new SqlCommand("SELECT sk.idTransaksi, p.namaProduk, m.namaMerek, c.namaCustomer, sk.qty, sk.tanggal, sk.hargaTransaksi FROM tableStockKeluar AS sk LEFT JOIN tableProduk AS p ON(sk.idProduk = p.idProduk) LEFT JOIN tableCustomer AS c ON(sk.idCustomer = c.idCustomer) JOIN tableMerek AS m ON(p.idMerek = m.idMerek); ", con);
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
                listViewSK.Items.Add(item);
            }
            cmd.Dispose();
            dataReader.Close();

            con.Close();
        }
    }
}
