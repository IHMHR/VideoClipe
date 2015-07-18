using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;

namespace VideoClipe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button5.Text = "Save";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog video = new OpenFileDialog();

                if (video.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBox1.Text = video.FileName;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show (ee.Message.ToString());
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.URL = textBox1.Text;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] bt = null;
            FileStream fs = new FileStream(this.textBox1.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            bt = br.ReadBytes((int)fs.Length);

            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");

            try
            {
                con.Open();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }

            if (con.State == ConnectionState.Open)
            {
                try
                {
                    MySqlCommand com = new MySqlCommand("INSERT INTO tbl VALUES (Null,@VC)", con);
                    com.Parameters.AddWithValue("@VC", bt);
                    com.ExecuteNonQuery();

                    MessageBox.Show("Okay \ti'm guess");
                }
                catch(Exception eee)
                {
                    MessageBox.Show(eee.Message.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");
            MySqlDataReader ms;
            MySqlCommand com = new MySqlCommand("SELECT teste FROM tbl", con);

            try
            {
                con.Open();
                ms = com.ExecuteReader();

                while (ms.Read())
                {
                    byte[] img = ((byte[])(ms["teste"]));
                    if (img == null)
                    {
                        //pictureBox1.Image = null;
                        axWindowsMediaPlayer1.URL = null;
                    }
                    else
                    {
                        /*//MemoryStream mst = new MemoryStream(img);
                        //pictureBox1.Image = System.Drawing.Image.FromStream(mst);

                        FileStream fs = null;
                        BinaryReader bs = null;

                        fs = new FileStream(@"C:\Users\Martinelli\Desktop\Teste1234.mp4", FileMode.Open, FileAccess.Read);
                        bs = new BinaryReader(fs);
                        
                        int cc = int.Parse((fs.Length.ToString()));

                        byte[] buff = bs.ReadBytes(cc);

                        //tenho que converte estes bytes em clipe
                        Stream stream = new MemoryStream(buff);
                        string caminho = @"C:\Users\Martinelli\Desktop\Teste1234.mp4";
                        fs = new FileStream(caminho, FileMode.CreateNew);

                        axWindowsMediaPlayer1.URL = caminho;*/
                        
                        string caminho = @"C:\Users\Martinelli\Desktop\Teste1234.mp4";
                        FileStream fs = new FileStream(caminho, FileMode.CreateNew);
                        fs.Write(img, 0, img.Length);
                        fs.Close();
                        axWindowsMediaPlayer1.URL = caminho;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }
    }
}
