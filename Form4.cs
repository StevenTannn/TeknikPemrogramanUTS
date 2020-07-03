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
    public partial class Form4 : Form
    {
        StockMasuk stockMasuk;
        StockKeluar stockKeluar;
        string formActive;
        public Form4(StockMasuk stockMasuk)
        {
            InitializeComponent();
            this.stockMasuk = stockMasuk;
            formActive = "stockMasuk";
        }

        public Form4(StockKeluar stockKeluar)
        {
            InitializeComponent();
            this.stockKeluar = stockKeluar;
            formActive = "stockKeluar";
        }

        private void Form4_Load(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
            con.Open();
            SqlCommand cmd;
            SqlDataReader dataReader;

            if(formActive == "stockKeluar") cmd = new SqlCommand("SELECT * FROM tableCustomer", con);
            else cmd = new SqlCommand("SELECT * FROM tableSupplier", con);

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

        private void btnLanjut_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int idProduk = Int32.Parse(listView1.SelectedItems[0].Text);
                if (formActive == "stockKeluar")
                {
                    StockKeluar.cus.getFromDatabase(idProduk);
                    stockKeluar.writeCusInfo();
                }
                else if (formActive == "stockMasuk")
                {
                    StockMasuk.spl.getFromDatabase(idProduk);
                    stockMasuk.writeSplInfo();
                }

                this.Close();
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
