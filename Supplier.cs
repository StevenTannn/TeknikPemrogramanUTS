using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock
{
    public class Supplier
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;


        private int idSupplier = 0;
        private string namaSupplier, alamat, noTelp;

        public Supplier() { }
        public Supplier(int idSupplier, string namaSupplier, string alamat, string noTelp)
        {
            this.idSupplier = idSupplier;
            this.namaSupplier = namaSupplier.ToUpper();
            this.alamat = alamat.ToUpper();
            this.noTelp = noTelp.ToUpper();
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

        public void setIdSupplier(int idSupplier) { this.idSupplier = idSupplier; }
        public int getIdSupplier() { return this.idSupplier; }

        public void setNamaSupplier(string namaSupplier) { this.namaSupplier = namaSupplier; }
        public string getNamaSupplier() { return this.namaSupplier; }

        public void setAlamat(string alamat) { this.alamat = alamat; }
        public string getAlamat() { return this.alamat; }

        public void setNoTelp(string noTelp) { this.noTelp = noTelp; }
        public string getNoTelp() { return this.noTelp; }




        public Boolean getFromDatabase(int idInput)
        {
            con.Open();
            cmd = new SqlCommand($"SELECT * FROM tableSupplier WHERE idSupplier = {idInput}", con);
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
                this.setIdSupplier(dataReader.GetInt32(0));
                this.setNamaSupplier(dataReader.GetString(1));
                this.setAlamat(dataReader.GetString(2));
                this.setNoTelp(dataReader.GetString(3));
            }
            cmd.Dispose();
            dataReader.Close();
            con.Close();
            return true;
        }

        public void addToDatabase()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO tableSupplier VALUES('{this.namaSupplier}', '{this.alamat}', '{this.noTelp}')", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void updToDatabase(string namaSupplier, string alamat, string noTelp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE tableSupplier SET namaSupplier = '{namaSupplier}', alamat = '{alamat}', noTelp = '{noTelp}' WHERE idSupplier = {this.idSupplier}", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Boolean delFromDatabase()
        {
            try {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM tableSupplier WHERE idSupplier = {this.idSupplier}", con);
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
