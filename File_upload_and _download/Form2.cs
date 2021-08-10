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

namespace File_upload_and__download
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox1.Text == "Full Name")
                textBox1.Text = null;
            textBox1.ForeColor = Color.Black;

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox2.Text == "Username")
                textBox2.Text = null;
            textBox2.ForeColor = Color.Black;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox4.Text == "Phone Number")
                textBox4.Text = null;
            textBox4.ForeColor = Color.Black;
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox3.Text == "E mail")
                textBox3.Text = null;
            textBox3.ForeColor = Color.Black;
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox6.Text == "Password")
                textBox6.Text = null;
            textBox6.ForeColor = Color.Black;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox5.Text == "Confirm Password")
                textBox5.Text = null;
            textBox5.ForeColor = Color.Black;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Full Name";
                textBox1.ForeColor = Color.Gray;
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "Username";
                textBox2.ForeColor = Color.Gray;
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "E mail";
                textBox3.ForeColor = Color.Gray;
            }
            if (textBox6.Text == "")
            {
                textBox6.Text = "Password";
                textBox6.ForeColor = Color.Gray;
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "Confirm Password";
                textBox5.ForeColor = Color.Gray;
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "Phone Number";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == textBox6.Text)
            {
                SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
                String qry = "insert into LoginTable (username,FullName,Email,Phone,Password) values(@username,@FullName, @Email,@Phone,@Password)";
                SqlCommand SqlCom = new SqlCommand(qry, CN);
                SqlCom.Parameters.Add(new SqlParameter("@username", textBox2.Text));
                SqlCom.Parameters.Add(new SqlParameter("@FullName", textBox1.Text));
                SqlCom.Parameters.Add(new SqlParameter("@Email", textBox3.Text));
                SqlCom.Parameters.Add(new SqlParameter("@Phone", textBox4.Text));
                SqlCom.Parameters.Add(new SqlParameter("@Password", textBox6.Text));
                CN.Open();
                SqlCom.ExecuteNonQuery();
                CN.Close();
                MessageBox.Show("Successfully Registered");
            }
            else
            {
                MessageBox.Show("Passwords Did'nt match");
            }
        }
    }
}
