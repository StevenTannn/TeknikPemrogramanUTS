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
    public partial class Login : Form
    {
        Boolean success = false;
        public Login()
        {
            InitializeComponent();
            this.FormClosing += Login_FormClosing;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu1 form = new Menu1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu2 form = new Menu2();
            form.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(AddConnection.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select idKaryawan, pass, idJabatan from tableKaryawan", con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.GetInt32(0).ToString() == id.Text && dataReader.GetString(1) == pass.Text)
                {
                    success = true;
                    if(dataReader.GetInt32(2) == 1)
                    {
                        Menu2 form = new Menu2();
                        form.Show();
                        this.Hide();
                    }
                    else if(dataReader.GetInt32(2) == 2)
                    {
                        Menu1 form = new Menu1();
                        form.Show();
                        this.Hide();
                    }
                    break;
                }
            }
            if (!success)
            {
                MessageBox.Show("Login gagal. Id atau Password salah.");
            }

        }
        private static bool _exiting;
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            if (!_exiting && MessageBox.Show("Are you sure want to exit?","Exit",MessageBoxButtons.OKCancel ,MessageBoxIcon.Information) == DialogResult.OK)
            {
                _exiting = true;
                // this.Close(); // you don't need that, it's already closing
                Environment.Exit(1);
            }
        }

   
    }
}
