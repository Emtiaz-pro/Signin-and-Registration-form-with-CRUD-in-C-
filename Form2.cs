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
using System.Runtime.InteropServices;

namespace AKM
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30");
        private object dataGridView1;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (username.Text != "" && password.Text != "" && confirm_password.Text != "" && email.Text != "" && phone.Text != "")
                {
                    if (password.Text == confirm_password.Text)
                    {
                        int c = check(email.Text);
                        if (c !=1 ) 
                        {
                            connect.Open();
                            SqlCommand command = new SqlCommand("insert into signupTB values(@username,@password,@email,@phone)", connect);
                            command.Parameters.AddWithValue("@username", username.Text);
                            command.Parameters.AddWithValue("@password", password.Text);
                            command.Parameters.AddWithValue("@email", email.Text);
                            command.Parameters.AddWithValue("@phone", phone.Text);
                            command.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("SUCCESSFULLY REGISTERED !!");
                            username.Text = "";
                            password.Text = "";
                            confirm_password.Text = "";
                            email.Text = "";
                            phone.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("You are already registered");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password does not match");
                    }
                }
                else
                {
                    MessageBox.Show("Fill the informations correctly");
                }

            }
            catch(Exception ex)
            {  MessageBox.Show(ex.Message); 
            }

    
        }
        int check(string email)
        {
            connect.Open();
            string query = "select count(*) from signupTB where email= '" + email + "'";
            SqlCommand command = new SqlCommand(query, connect);
            int c= (int)command.ExecuteScalar();
            connect.Close();
            return c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 goform21 = new Form1();
            goform21.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {

                if (control is TextBox)
                {
                    control.Visible = false;
                }
                else if (control is NumericUpDown)
                {
                    control.Visible = false;
                }
                else if (control is Label)
                {
                    control.Visible = false;
                }
                else
                {
                    control.Visible = true;
                }
            }

            SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30");

            string readQuery = "SELECT * FROM signupTB";

            SqlDataAdapter sda = new SqlDataAdapter(readQuery, connect);

            SqlCommandBuilder cmd = new SqlCommandBuilder(sda);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    int idToDelete = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["ID"].Value);

                    using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        connect.Open();
                        string deleteQuery = "DELETE FROM signupTB WHERE ID = @ID";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@ID", idToDelete);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Record deleted successfully!");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            

            private void RefreshDataGridView()
            {
                string readQuery = "SELECT * FROM signupTB";
                using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(readQuery, connect);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    int idToUpdate = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["ID"].Value);

                    
                    string username = dataGridView1.Rows[selectedIndex].Cells["Username"].Value.ToString();
                    string password = dataGridView1.Rows[selectedIndex].Cells["Password"].Value.ToString();
                   

                   
                    using (SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\OneDrive\\Documents\\loginDB.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        connect.Open();
                        string updateQuery = "UPDATE signupTB SET Username = @Username, Password = @Password WHERE ID = @ID";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", password);
                            // Add more parameters for other fields

                            cmd.Parameters.AddWithValue("@ID", idToUpdate);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Record updated successfully!");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Please select a row to update.");
                }
            

        }
    }
}
