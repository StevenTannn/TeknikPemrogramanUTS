using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public class Produk
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;


        private int idProduk = 0, idMerek, ram, hargaPrd;
        private string namaProduk, namaMerek, mInternal;
        private byte[] img;

        public Produk()
        {

        }
        public Produk(int idProduk, int idMerek, string namaProduk, int ram, string mInternal, byte[] img, int hargaPrd)
        {
            this.idProduk = idProduk;
            this.idMerek = idMerek;
            this.namaProduk = namaProduk;
            this.ram = ram;
            this.mInternal = mInternal;
            this.img = img;
            this.hargaPrd = hargaPrd;
        }

        internal TransStockMasuk TransStockMasuk
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal TransStockKeluar TransStockKeluar
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void setIdProduk(int idProduk) { this.idProduk = idProduk; }
        public int getIdProduk() { return this.idProduk; }

        public void setNamaProduk(string namaProduk) { this.namaProduk = namaProduk; }
        public string getNamaProduk() { return this.namaProduk; }

        public void setIdMerek(int idMerek) { this.idMerek = idMerek; }
        public int getIdMerek() { return this.idMerek; }

        public void setNamaMerek(string namaMerek) { this.namaMerek = namaMerek; }
        public string getNamaMerek() { return this.namaMerek; }

        public void setRAM(int ram) { this.ram = ram; }
        public int getRAM() { return this.ram; }

        public void setMInternal(string mInternal) { this.mInternal = mInternal; }
        public string getMInternal() { return this.mInternal; }

        public void setImg(byte[] img) { this.img = img; }
        public byte[] getImg() { return this.img; }

        public void setHargaPrd(int hargaPrd) { this.hargaPrd = hargaPrd; }
        public int getHargaPrd() { return this.hargaPrd; }

        public int getQty()
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
                return stock;
            }
            return -1;
        }

        public void setImgPrd(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            this.img = ms.ToArray();
        }
        public void setImgPrd(byte[] img) { this.img = img; }

        public Image getImgPrd()
        {
            MemoryStream ms = new MemoryStream(img);
            return Image.FromStream(ms);
        }

        public Boolean getFromDatabase(int idInput)
        {
            con.Open();
            cmd = new SqlCommand($"SELECT idProduk, tableProduk.idMerek, namaProduk, namaMerek, RAM, mInternal, imgPrd, hargaPrd FROM tableProduk LEFT JOIN tableMerek ON (tableProduk.idMerek = tableMerek.idMerek) WHERE idProduk = {idInput}", con);
            dataReader = cmd.ExecuteReader();

            if (dataReader.Read() == false)
            {
                cmd.Dispose();
                dataReader.Close();
                con.Close();
                return false;
            }
            else
            {
                this.setIdProduk(dataReader.GetInt32(0));
                this.setIdMerek(dataReader.GetInt32(1));
                this.setNamaProduk(dataReader.GetString(2));
                this.setNamaMerek(dataReader.GetString(3));
                this.setRAM(dataReader.GetInt32(4));
                this.setMInternal(dataReader.GetString(5));
                try { this.setImgPrd((byte[])dataReader.GetValue(6)); } catch { }
                this.setHargaPrd(dataReader.GetInt32(7));

            }
            cmd.Dispose();
            dataReader.Close();
            con.Close();


            return true;
        }

        public void addToDatabase()
        {
            con.Open();
            cmd = new SqlCommand($"INSERT INTO tableProduk VALUES({this.idMerek}, '{this.namaProduk}', {this.ram}, '{this.mInternal}', @imgProduk, {hargaPrd})", con);
            cmd.Parameters.Add(new SqlParameter("@imgProduk", img));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("SELECT TOP 1 idProduk FROM tableProduk ORDER BY idProduk DESC;", con);
            dataReader = cmd.ExecuteReader();
            if (dataReader.Read()) this.idProduk = dataReader.GetInt32(0);
            dataReader.Close();
            cmd.Dispose();
            cmd = new SqlCommand($"INSERT INTO tableStock VALUES({this.idProduk}, 0)", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void updToDatabase(int idMerek, string namaProduk, int RAM, string mInternal, byte[] img, int hargaPrd)
        {
            con.Open();
            cmd = new SqlCommand($"UPDATE tableProduk SET idMerek = {idMerek}, namaProduk = '{namaProduk}', RAM = {RAM}, mInternal = '{mInternal}', imgPrd = @imgProduk, hargaPrd = {hargaPrd} WHERE idProduk = {this.idProduk}", con);
            cmd.Parameters.Add(new SqlParameter("@imgProduk", img));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Boolean delFromDatabase()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand($"DELETE FROM tableProduk WHERE idProduk = {this.idProduk}", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = new SqlCommand($"DELETE FROM tableStock WHERE idProduk = {this.idProduk}", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return false;
            }
            return true;
        }


    }
}
