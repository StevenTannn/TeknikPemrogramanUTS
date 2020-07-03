using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock
{
    public class Customer
    {
        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;


        private int idCustomer = 0;
        private string namaCustomer, alamat, noTelp;

        public Customer() { }
        public Customer(int idCustomer, string namaCustomer, string alamat, string noTelp)
        {
            this.idCustomer = idCustomer;
            this.namaCustomer = namaCustomer.ToUpper();
            this.alamat = alamat.ToUpper();
            this.noTelp = noTelp.ToUpper();
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

        public void setIdCustomer(int idCustomer) { this.idCustomer = idCustomer; }
        public int getIdCustomer() { return this.idCustomer; }

        public void setNamaCustomer(string namaCustomer) { this.namaCustomer = namaCustomer; }
        public string getNamaCustomer() { return this.namaCustomer; }

        public void setAlamat(string alamat) { this.alamat = alamat; }
        public string getAlamat() { return this.alamat; }

        public void setNoTelp(string noTelp) { this.noTelp = noTelp; }
        public string getNoTelp() { return this.noTelp; }




        public Boolean getFromDatabase(int idInput)
        {
            con.Open();
            cmd = new SqlCommand($"SELECT * FROM tableCustomer WHERE idCustomer = {idInput}", con);
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
                this.setIdCustomer(dataReader.GetInt32(0));
                this.setNamaCustomer(dataReader.GetString(1));
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
            SqlCommand cmd = new SqlCommand($"INSERT INTO tableCustomer VALUES('{this.namaCustomer}', '{this.alamat}', '{this.noTelp}')", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void updToDatabase(string namaCustomer, string alamat, string noTelp)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE tableCustomer SET namaCustomer = '{namaCustomer}', alamat = '{alamat}', noTelp = '{noTelp}' WHERE idCustomer = {this.idCustomer}", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Boolean delFromDatabase()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM tableCustomer WHERE idCustomer = {this.idCustomer}", con);
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
