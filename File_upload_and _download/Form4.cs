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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
            label4.Text = Form1.Nextuser;
            
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }


        string emailuser = Form1.user;
        private void button_WOC1_MouseClick(object sender, MouseEventArgs e)
        {
            if (emailuser != "guest")
            {

                Form5 f5 = new Form5();
                this.Hide();
                f5.Show();
            }
            else 
            {
                MessageBox.Show("Uploading is not possible , please register yourself to upload and download all the encrypted files");
            }
        }

        private void button_WOC2_MouseClick(object sender, MouseEventArgs e)
        {
            Form6 f6 = new Form6();
            this.Hide();
            f6.Show();
        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
            Form7 f7 = new Form7();
            this.Hide();
            f7.Show();
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            Form7 f7 = new Form7();
            this.Hide();
            f7.Show();
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
            Form8 f8 = new Form8();
            this.Hide();
            f8.Show();
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            Form8 f8 = new Form8();
            this.Hide();
            f8.Show();
        }

        private void label8_MouseClick(object sender, MouseEventArgs e)
        {
            Form9 f9 = new Form9();
            this.Hide();
            f9.Show();
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            Form9 f9 = new Form9();
            this.Hide();
            f9.Show();
        }

        
    }
         
}
