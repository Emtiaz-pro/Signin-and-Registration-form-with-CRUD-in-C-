using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AKM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void button2_Click(object sender, EventArgs e)
        {
            if(username.Text !=""&& password.Text !="")
            {
                string query = "select count(*) from signupTB ehere email='" + username + "' and" + "password= " + password.Text + "'";
                connect.Open();
                SqlCommand command = new SqlCommand(query,connect);
                int c = (int)command.ExecuteScalar();
                if (c != 1)
                {
                    MessageBox.Show("Error !! Check username & Password");
                }
                else
                {
                    MessageBox.Show("Welcome to your profile !!");
                    username.Text = "";
                    password.Text = "";
                }
            }
            else
            {
                MessageBox.Show("FIll the information");
            }
            //Form2 goform22 = new Form2();
            //goform22.Show();
            //this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 goform33 = new Form3();
            goform33.Show();
            this.Hide();
        }
    }
}
