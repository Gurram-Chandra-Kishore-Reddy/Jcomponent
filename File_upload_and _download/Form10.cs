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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        string keyencrypt, IVencrypt, downloadpath, filename;
        private void button1_Click(object sender, EventArgs e)
        {
            filename = Form6.namefile;
            SqlConnection CN = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Chandu\source\repos\File_upload_and _download\File_upload_and _download\fileuploaddownload.mdf; Integrated Security = True");

            String qry = "select encryptkey,IV,filepath from UploadTable where filename = '" + filename + "'";
            SqlCommand SqlCom = new SqlCommand(qry, CN);

            CN.Open();
            using (SqlDataReader oReader = SqlCom.ExecuteReader())
            {
                while (oReader.Read())
                {
                    keyencrypt = oReader["encryptkey"].ToString();
                    downloadpath = oReader["filepath"].ToString();
                    IVencrypt = oReader["IV"].ToString();

                }


            }


            CN.Close();

            string encrypted = File.ReadAllText(downloadpath);
            string key = textBox1.Text;
            string iv = textBox2.Text;
            try
            {
                //start
                byte[] encbytes = Convert.FromBase64String(encrypted);
                AesCryptoServiceProvider endec = new AesCryptoServiceProvider();
                endec.BlockSize = 128;
                endec.KeySize = 256;
                endec.Key = ASCIIEncoding.ASCII.GetBytes(key);
                endec.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                endec.Padding = PaddingMode.PKCS7;
                endec.Mode = CipherMode.CBC;
                ICryptoTransform icrypt = endec.CreateDecryptor(endec.Key, endec.IV);
                byte[] dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
                icrypt.Dispose();
                string decryptedtext = ASCIIEncoding.ASCII.GetString(dec);
                string finalpath = @"C:\Users\Chandu\Downloads\" + filename;
                FileStream fs = File.Create(finalpath);

                byte[] bytes = Encoding.UTF8.GetBytes(decryptedtext);


                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                MessageBox.Show("decrypted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The specified Key and IV pair is incorrect or invalid, the file will be downloaded without decryption");
                string finalpath = @"C:\Users\Chandu\Downloads\" + filename;
                FileStream fs = File.Create(finalpath);
                byte[] bytes = Encoding.UTF8.GetBytes(encrypted);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                //File.Copy(downloadpath, finalpath);
                MessageBox.Show("downloaded without decryption");
            }

            

        }
    }
}
