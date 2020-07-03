using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock
{
    class TransStockMasuk : Transaction
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;

        private int idProduk, idSupplier, qty, hargaTransaksi;
        private string date;

        public TransStockMasuk(int idProduk, int idSupplier, int qty, string date, int hargaTransaksi)
        {
            this.idProduk = idProduk;
            this.idSupplier = idSupplier;
            this.qty = qty;
            this.date = date.ToUpper();
            this.hargaTransaksi = hargaTransaksi;
        }

        public void setIdProduk(int idProduk) { this.idProduk = idProduk; }
        public int getIdProduk() { return this.idProduk; }

        public void setIdSupplier(int idSupplier) { this.idSupplier = idSupplier; }
        public int getIdSupplier() { return this.idSupplier; }

        public void setQty(int qty) { this.qty = qty; }
        public int getQty() { return this.qty; }

        public void setDate(string date) { this.date = date; }
        public string getDate() { return this.date; }

        public void setHargaTransaksi(int hargaTransaksi) { this.hargaTransaksi = hargaTransaksi; }
        public int getHargaTransaksi() { return this.hargaTransaksi; }

        public void addToDatabase()
        {
            con.Open();
            int totalQty;
            
            cmd = new SqlCommand($"SELECT prdStock FROM tableStock WHERE idProduk = {this.idProduk}", con);
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                totalQty = dataReader.GetInt32(0) + this.qty;
                cmd.Dispose();
                dataReader.Close();
                cmd = new SqlCommand($"UPDATE tableStock SET prdStock = {totalQty} WHERE idProduk = {this.idProduk}", con);
                cmd.ExecuteNonQuery();
            }
            cmd.Dispose();
            dataReader.Close();
           
            cmd = new SqlCommand($"INSERT INTO tableStockMasuk VALUES({this.idProduk}, {this.idSupplier}, {this.qty}, '{this.date}', {this.hargaTransaksi})", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
