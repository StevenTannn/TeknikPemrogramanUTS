using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock
{
    public class Merek
    {

        SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
        SqlCommand cmd;
        SqlDataReader dataReader;


        private int idMerek;
        private string namaMerek;

        public Merek()
        {

        }
        public Merek(int idMerek, string namaMerek)
        {
            this.idMerek = idMerek;
            this.namaMerek = namaMerek;
        }

        public Produk Produk
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void setIdMerek(int idMerek) { this.idMerek = idMerek; }
        public int getIdMerek() { return this.idMerek; }

        public void setNamaMerek(string namaMerek) { this.namaMerek = namaMerek; }
        public string getNamaMerek() { return this.namaMerek; }

        

        public Boolean getFromDatabase(int idInput)
        {
            con.Open();
            cmd = new SqlCommand($"SELECT * FROM tableMerek WHERE idMerek = {idInput}", con);
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
                this.setIdMerek(dataReader.GetInt32(0));
                this.setNamaMerek(dataReader.GetString(1));
            }
            cmd.Dispose();
            dataReader.Close();
            con.Close();

            return true;
        }

        public void addToDatabase()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO tableMerek VALUES('{this.namaMerek.ToUpper()}')", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void updToDatabase(string namaMerek)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"UPDATE tableMerek SET namaMerek = '{namaMerek.ToUpper()}' WHERE idMerek = {this.idMerek}", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Boolean delFromDatabase()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM tableMerek WHERE idMerek = {this.idMerek}", con);
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
