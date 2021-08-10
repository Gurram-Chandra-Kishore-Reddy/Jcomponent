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
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Blue;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox1.Text == "Username")
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }
        
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox2.Text == "Password")
            textBox2.Text = null;
            textBox2.ForeColor = Color.Black;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Gray;
                textBox2.Text = "Password";
                textBox2.ForeColor = Color.Gray;
            }
            
        }
        
        private void label6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
        
            this.Hide();
            f2.Show();
          
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.Show();
        }
        public static string Nextuser,user;
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            
                SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");

                String qry = "select Password,FullName from LoginTable where username = '" + textBox1.Text + "'";
                SqlCommand SqlCom = new SqlCommand(qry, CN);

                CN.Open();
                SqlDataReader sqldr = SqlCom.ExecuteReader();
                sqldr.Read();
                string check_password = sqldr["Password"].ToString();
                Nextuser = sqldr["FullName"].ToString();
                user = textBox1.Text;
                CN.Close();
                if (textBox2.Text == check_password)
                {

                    Form4 f4 = new Form4();
                    this.Hide();
                    f4.Show();

                }
                else
                {

                    MessageBox.Show("Invalid Username or password");

                }
            
           
        }
        
    }
}
