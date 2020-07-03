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
    public partial class StockMasuk : Form
    {

        public static Produk prd = new Produk();
        public static Supplier spl = new Supplier();
        ArrayList transactionList = new ArrayList();
        public StockMasuk()
        {
            InitializeComponent();  
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            date.Text = today.ToString("dd/MM/yyyy");
        }

        private void genPrd_Click(object sender, EventArgs e)
        {
            try { 
                if (!prd.getFromDatabase(Int32.Parse(prdIdInput.Text))) MessageBox.Show("Data tidak ditemukan");
                else writePrdInfo();
            }
            catch {
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

        private void genSpl_Click(object sender, EventArgs e)
        {
            try {
                if (!spl.getFromDatabase(Int32.Parse(splInput.Text))) MessageBox.Show("Data tidak ditemukan");
                else writeSplInfo();
            }
            catch{
                MessageBox.Show("Tolong isi ID Supplier terlebih dahulu");
            }

        }

        public void writeSplInfo()
        {
            splInput.Text = spl.getIdSupplier().ToString();
            splName.Text = spl.getNamaSupplier();
        }

        private void searchSPL_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(this);
            form4.Show();
        }

        private void genTrans_Click(object sender, EventArgs e)
        {
            if (prd.getIdProduk() != 0 && spl.getIdSupplier() != 0 && !string.IsNullOrWhiteSpace(prdHarga.Text) && !string.IsNullOrWhiteSpace(date.Text))
            {
                try
                {
                    int qty = Int32.Parse(prdQty.Text);
                    TransStockMasuk trans = new TransStockMasuk(prd.getIdProduk(), spl.getIdSupplier(), qty, date.Text, Int32.Parse(prdHarga.Text) * qty);
                    transactionList.Add(trans);
                    ListViewItem item = new ListViewItem();
                    item.Text = prd.getIdProduk().ToString();
                    item.SubItems.Add(prd.getNamaProduk());
                    item.SubItems.Add(prd.getNamaMerek());
                    item.SubItems.Add(spl.getIdSupplier().ToString());
                    item.SubItems.Add(spl.getNamaSupplier());
                    item.SubItems.Add(trans.getQty().ToString());
                    item.SubItems.Add(trans.getDate());
                    item.SubItems.Add(trans.getHargaTransaksi().ToString());
                    listView1.Items.Add(item);
                }
                catch
                {
                    MessageBox.Show("Quantity tidak valid");
                }
            }
            else
            {
                MessageBox.Show("Data tidak lengkap");
            }

        }

        private void process_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Menyimpan ke database ? ", "SAVE TO DATABASE", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (TransStockMasuk t in transactionList)
                {
                    t.addToDatabase();
                }
                transactionList.Clear();
                listView1.Items.Clear();
            }
        }
    }
}
