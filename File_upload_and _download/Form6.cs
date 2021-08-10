using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_upload_and__download
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }
        
        public static string emailuser,Email,keyencrypt,IVencrypt,namefile;
        public string filename;
        public string downloadpathwithoutencr;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                filename = row.Cells["filename"].Value.ToString();
                
            }
            namefile = filename;
            SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");

            String qry = "select encryptkey,IV,filepath from UploadTable where filename = '" + namefile + "'";
            SqlCommand SqlCom = new SqlCommand(qry, CN);

            CN.Open();
            using (SqlDataReader oReader = SqlCom.ExecuteReader())
            {
                while (oReader.Read())
                {
                    keyencrypt = oReader["encryptkey"].ToString();
                    IVencrypt = oReader["IV"].ToString();
                    downloadpathwithoutencr = oReader["filepath"].ToString();


                }


            }
            CN.Close();
            emailuser = Form1.user;
            String qrynew = "select Email from LoginTable where username = '" + emailuser + "'";
            SqlCommand SqlComnew = new SqlCommand(qrynew, CN);

            CN.Open();
            using (SqlDataReader oReader = SqlComnew.ExecuteReader())
            {
                while (oReader.Read())
                {
                    Email = oReader["Email"].ToString();
                }
            }
            CN.Close();
            if (String.Equals(keyencrypt,"null") == false)
            {
                if (emailuser != "guest")
                {
                    string emailkeyiv = "Key = " + keyencrypt + "; IV = " + IVencrypt;
                    MailMessage mail = new MailMessage("gurramchandrakishore@gmail.com", Email, "Key for Downloading the file", emailkeyiv);
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential("gurramchandrakishore@gmail.com", "17MIS0066");
                    client.EnableSsl = true;
                    client.Send(mail);
                    MessageBox.Show("Key has been sent to your registered mail address");
                    Form10 f10 = new Form10();

                    f10.Show();
                }
                else 
                {
                    MessageBox.Show("Decryption is not possible,Click OK to download the encrypted file");
                    string path = @"C:\users\Chandu\Downloads\" + namefile;
                    FileStream fs = File.Create(path);
                    string withoutencrypted = File.ReadAllText(downloadpathwithoutencr);
                    byte[] bytes = Encoding.UTF8.GetBytes(withoutencrypted);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    MessageBox.Show("Successfully downloaded the file");
                }
            }
            else
            {
                MessageBox.Show("The file you have selected is not encrypted, Downloading directly");
                string path = @"C:\users\Chandu\Downloads\" + namefile;
                FileStream fs = File.Create(path);
                string withoutencrypted = File.ReadAllText(downloadpathwithoutencr);
                byte[] bytes = Encoding.UTF8.GetBytes(withoutencrypted);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                MessageBox.Show("Successfully downloaded the file");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
            string query = "Select id,username,filename from UploadTable";
            CN.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query, CN);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            
            
                dataGridView1.DataSource = dtbl;
            
            CN.Close();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Download here";
            btn.Text = "Download";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            namefile = textBox1.Text;
            SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
            
            String qry = "select encryptkey,IV from UploadTable where filename = '" + textBox1.Text + "'";
            SqlCommand SqlCom = new SqlCommand(qry, CN);

            CN.Open();
            using (SqlDataReader oReader = SqlCom.ExecuteReader())
            {
                while (oReader.Read())
                {
                    keyencrypt = oReader["encryptkey"].ToString();
                    IVencrypt = oReader["IV"].ToString();
                    downloadpathwithoutencr = oReader["filepath"].ToString();
                
                }


            }
            CN.Close();
            //String keycheck = keyencrypt;
            if (true)
            {
                emailuser = Form1.user;
                String qrynew = "select Email from LoginTable where username = '" + emailuser + "'";
                SqlCommand SqlComnew = new SqlCommand(qrynew, CN);

                CN.Open();
                using (SqlDataReader oReader = SqlComnew.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        Email = oReader["Email"].ToString();
                    }
                }
                CN.Close();
                string emailkeyiv = "Key = " + keyencrypt + "; IV = " + IVencrypt;
                MailMessage mail = new MailMessage("gurramchandrakishore@gmail.com", Email, "Key for Downloading the file", emailkeyiv);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("gurramchandrakishore@gmail.com", "17MIS0066");
                client.EnableSsl = true;
                client.Send(mail);
                MessageBox.Show("Key has been sent to your registered mail address");
                Form10 f10 = new Form10();

                f10.Show();
            }
            else
            {
                MessageBox.Show("The file you have selected is not encrypted, Downloading directly");
                string path = @"C:\users\Chandu\Downloads\" + namefile;
                FileStream fs = File.Create(path);
                string withoutencrypted = File.ReadAllText(downloadpathwithoutencr);
                byte[] bytes = Encoding.UTF8.GetBytes(withoutencrypted);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                MessageBox.Show("Successfullt downloaded the file");
            }
        }
    }
}
