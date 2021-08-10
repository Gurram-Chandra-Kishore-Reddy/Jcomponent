using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_upload_and__download
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
           
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            label5.Visible = true;
            //textBox2.Visible = true;
            if (checkBox1.Checked == false)
            {
                label5.Visible = false;
                //textBox2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            if (od.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = od.FileName;
            }
        }

        public static int check = 1;
        private void button2_Click(object sender, EventArgs e)
        {

            string user = Form1.user;
            int ran;
            Random randm = new Random();
            ran = randm.Next(1, 9999);

            if (checkBox1.Checked == true)
            {
                string name = Path.GetFileName(textBox1.Text);
                string decrypted = File.ReadAllText(textBox1.Text);
                int keylength = 32;
                StringBuilder keybuilder = new StringBuilder();
                Random random = new Random();
                char letter;
                for (int i = 0; i < keylength; i++)
                {
                    double flt = random.NextDouble();
                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letter = Convert.ToChar(shift + 65);
                    keybuilder.Append(letter);
                }
                string keyforencryption = keybuilder.ToString();
                //generate iv
                int ivlength = 16;
                StringBuilder ivbuilder = new StringBuilder();
                Random randomone = new Random();
                char letterone;
                for (int i = 0; i < ivlength; i++)
                {
                    double flt = randomone.NextDouble();
                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letterone = Convert.ToChar(shift + 65);
                    ivbuilder.Append(letterone);
                }
                string ivforencryption = ivbuilder.ToString();

                //start
                byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(decrypted);
                AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
                endec.BlockSize = 128;
                endec.KeySize = 256;
                endec.Key = ASCIIEncoding.ASCII.GetBytes(keyforencryption);
                endec.IV = ASCIIEncoding.ASCII.GetBytes(ivforencryption);
                endec.Padding = PaddingMode.PKCS7;
                endec.Mode = CipherMode.CBC;
                ICryptoTransform icrypt = endec.CreateEncryptor(endec.Key, endec.IV);
                byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
                icrypt.Dispose();
                string encryptedtext = Convert.ToBase64String(enc);
                string encryptedfilepath = "E:\\files\\encrypted files\\" + name;
                FileStream fs = File.Create(encryptedfilepath);
                //FileStream fs = File.Create("C:\\Users\\Chandu\\Desktop\\today.txt");
                byte[] bytes = Encoding.UTF8.GetBytes(encryptedtext);


                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                //MessageBox.Show("key = " + key + "  \nIV = " + iv);



                
                
                
                
                
                //byte[] FileData = ReadFile(encryptedfilepath);
                SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
                string query = "insert into UploadTable (id,filename,encryptkey,IV,username,filepath) values (@id,@filename,@encryptkey,@IV,@username,@filepath)";
                SqlCommand SqlCom = new SqlCommand(query, CN);
                SqlCom.Parameters.Add(new SqlParameter("@username", user));
                //SqlCom.Parameters.Add(new SqlParameter("@filedata", (object)FileData));
                SqlCom.Parameters.Add(new SqlParameter("@filepath", encryptedfilepath));
                SqlCom.Parameters.Add(new SqlParameter("@id", (int)ran));
                SqlCom.Parameters.Add(new SqlParameter("@filename", name));
                SqlCom.Parameters.Add(new SqlParameter("@encryptkey",keyforencryption));

                SqlCom.Parameters.Add(new SqlParameter("@IV", ivforencryption));
                
                CN.Open();
                SqlCom.ExecuteNonQuery();
                CN.Close();
                MessageBox.Show("Uploaded successfully");
            }
            else 
            {
                MessageBox.Show("warning, encryption mode not chosen");

                // string encryptedfilepath = "E:\\files\\encrypted files\\" + name;

                //byte[] FileData = ReadFile(textBox1.Text);
                string filename = Path.GetFileName(textBox1.Text);
                string decrypted = File.ReadAllText(textBox1.Text);
                string encryptedfilepath = "E:\\files\\encrypted files\\" + filename;
                FileStream fs = File.Create(encryptedfilepath);
                byte[] bytes = Encoding.UTF8.GetBytes(decrypted);


                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                //string filename = Path.GetFileName(textBox1.Text);

                string keyencrypt = "null";
                string stringIV = "null";
                
                SqlConnection CNw = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");
                String qry = "insert into UploadTable (id,filename,encryptkey,IV,username,filepath) values(@id,@filename,@encryptkey,@IV,@username,@filepath)";
                SqlCommand SqlCom = new SqlCommand(qry, CNw);
                SqlCom.Parameters.Add(new SqlParameter("@username", user));
                //SqlCom.Parameters.Add(new SqlParameter("@filedata", (object)FileData));
                SqlCom.Parameters.Add(new SqlParameter("@encryptkey", keyencrypt));
                SqlCom.Parameters.Add(new SqlParameter("@id", (int)ran));
                SqlCom.Parameters.Add(new SqlParameter("@filename", filename));
                SqlCom.Parameters.Add(new SqlParameter("@IV", stringIV));
                SqlCom.Parameters.Add(new SqlParameter("@filepath", encryptedfilepath));
                CNw.Open();
                SqlCom.ExecuteNonQuery();
                CNw.Close();
                check = 0;
                MessageBox.Show("Uploaded successfully");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }
    }
}
