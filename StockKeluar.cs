using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Stock
{
    public partial class StockKeluar : Form
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;


        public static Produk prd = new Produk();
        public static Customer cus = new Customer();
        ArrayList transactionList = new ArrayList();
        public StockKeluar()
        {
            InitializeComponent();
        }

        private void genPrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!prd.getFromDatabase(Int32.Parse(prdIdInput.Text))) MessageBox.Show("Data tidak ditemukan");
                else writePrdInfo();
            }
            catch
            {
                MessageBox.Show("Tolong isi ID Product terlebih dahulu");
            }

        }


        public void writePrdInfo()
        {
            prdIdInput.Text = prd.getIdProduk().ToString();
            prdID.Text = prd.getIdProduk().ToString();
            prdName.Text = prd.getNamaProduk();
            prdSpecs.Text = prd.getRAM().ToString() + "/" + prd.getMInternal();
            prdHargaEceran.Text = prd.getHargaPrd().ToString();
            try { prdPicture.Image = prd.getImgPrd(); } catch { }
            stok.Text = prd.getQty().ToString();
        }

        Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void searchPrd_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        private void genCus_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cus.getFromDatabase(Int32.Parse(cusInput.Text))) MessageBox.Show("Data tidak ditemukan");
                else writeCusInfo();
            }
            catch
            {
                MessageBox.Show("Tolong isi ID Customer terlebih dahulu");
            }

        }

        public void writeCusInfo()
        {
            cusInput.Text = cus.getIdCustomer().ToString();
            cusName.Text = cus.getNamaCustomer();
        }

        private void searchCUS_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(this);
            form4.Show();
        }

        private void genTrans_Click(object sender, EventArgs e)
        {
            if (prd.getIdProduk() != 0 && cus.getIdCustomer() != 0 && !string.IsNullOrWhiteSpace(prdHarga.Text) && !string.IsNullOrWhiteSpace(date.Text))
            {
                int qty = Int32.Parse(prdQty.Text);
                int qtySisa = checkQty(prd.getIdProduk(), qty);
                if (qtySisa >= 0)
                {
                    TransStockKeluar trans = new TransStockKeluar(prd.getIdProduk(), cus.getIdCustomer(), qty, date.Text, Int32.Parse(prdHarga.Text) * qty);
                    transactionList.Add(trans);
                    ListViewItem item = new ListViewItem();
                    item.Text = prd.getIdProduk().ToString();
                    item.SubItems.Add(prd.getNamaProduk());
                    item.SubItems.Add(prd.getNamaMerek());
                    item.SubItems.Add(cus.getIdCustomer().ToString());
                    item.SubItems.Add(cus.getNamaCustomer());
                    item.SubItems.Add(trans.getQty().ToString());
                    item.SubItems.Add(trans.getDate());
                    item.SubItems.Add(trans.getHargaTransaksi().ToString());
                    listView1.Items.Add(item);
                }
                else
                {
                    MessageBox.Show($"Jumlah stock yang tersedia tidak mencukupi. Mohon sesuaikan lagi jumlah produk dalam transaksi (Qty). \nQty = {qty+qtySisa}");
                }         
            }
            else
            {
                MessageBox.Show("Data tidak lengkap.");
            }

        }

        public int checkQty(int idProduk, int inputQty)
        {
            con.Open();
            cmd = new SqlCommand($"SELECT prdStock FROM tableStock WHERE idProduk = {idProduk}", con);
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                int stock = dataReader.GetInt32(0);
                con.Close();
                cmd.Dispose();
                dataReader.Close();
                return stock - inputQty;
            }
            con.Close();
            cmd.Dispose();
            dataReader.Close();
            return 0;
        }

        private void process_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (TransStockKeluar t in transactionList)
                {
                    t.addToDatabase();
                }
                transactionList.Clear();
                listView1.Items.Clear();
            }
        }

        private void StockKeluar_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            date.Text = today.ToString("dd/MM/yyyy");

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
