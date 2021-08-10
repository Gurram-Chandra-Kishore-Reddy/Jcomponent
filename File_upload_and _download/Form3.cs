using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Data.SqlClient;

namespace File_upload_and__download
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Username")
                textBox1.Text = null;
            textBox1.ForeColor = Color.Black;

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "New Password")
                textBox2.Text = null;
            textBox2.ForeColor = Color.Black;

        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == "Confirm Password")
                textBox3.Text = null;
            textBox3.ForeColor = Color.Black;

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Gray;
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "New Password";
                textBox2.ForeColor = Color.Gray;
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "Confirm Password";
                textBox3.ForeColor = Color.Gray;
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "Enter OTP";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox4.Text == "Enter OTP")
                textBox4.Text = null;
            textBox4.ForeColor = Color.Black;
        }
        string otp;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == otp)
            {
                if (textBox2.Text == textBox3.Text)
                {
                    SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
                    String qry = "update LoginTable set Password = '" + textBox2.Text + "' where username = '" + textBox1.Text + "'";
                    SqlCommand SqlCom = new SqlCommand(qry, CN);
                    CN.Open();
                    SqlCom.ExecuteNonQuery();
                    CN.Close();
                    MessageBox.Show("Reset Password Successfull");
                }
                else
                {
                    MessageBox.Show("Passwords Did'nt Match");
                }
                
            }
            else 
            {
                MessageBox.Show("Incorrect OTP");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
            String qry = "select Email from LoginTable where username = '" + textBox1.Text + "'";
            SqlCommand SqlCom = new SqlCommand(qry, CN);

            CN.Open();
            SqlDataReader sqldr = SqlCom.ExecuteReader();
            sqldr.Read();
            string resetmail = sqldr["Email"].ToString();
            CN.Close();
            int id;
            Random randm = new Random();
            id = randm.Next(1, 9999);
            otp = id.ToString();
            MailMessage mail = new MailMessage("gurramchandrakishore@gmail.com", resetmail, "OTP from Secure File Handling to reset your password", otp);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("gurramchandrakishore@gmail.com", "gurramgckr1998");
            client.EnableSsl = true;
            client.Send(mail);
            MessageBox.Show("OTP has been sent to the registered Email address");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }
    }
}
