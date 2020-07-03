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
    public partial class Form3 : Form
    {
        StockMasuk stockMasuk;
        StockKeluar stockKeluar;
        string formActive;
        public Form3(StockMasuk stockMasuk)
        {
            InitializeComponent();
            this.stockMasuk = stockMasuk;
            formActive = "stockMasuk";
        }

        public Form3(StockKeluar stockKeluar)
        {
            InitializeComponent();
            this.stockKeluar = stockKeluar;
            formActive = "stockKeluar";
        }

        private void Form3_Load(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
            con.Open();
            SqlCommand cmd;
            SqlDataReader dataReader;


            cmd = new SqlCommand("SELECT idProduk, namaProduk, tableMerek.namaMerek, RAM, mInternal, hargaPrd FROM tableProduk LEFT JOIN tableMerek ON(tableProduk.idMerek = tableMerek.idMerek)", con);
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

        private void btnLanjut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int idProduk = Int32.Parse(listView1.SelectedItems[0].Text);
                if (formActive == "stockKeluar")
                {
                    StockKeluar.prd.getFromDatabase(idProduk);
                    stockKeluar.writePrdInfo();
                }
                else if (formActive == "stockMasuk")
                {
                    StockMasuk.prd.getFromDatabase(idProduk);
                    stockMasuk.writePrdInfo();
                }
                    
                this.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
