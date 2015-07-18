using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;

namespace VideoClipe
{
    public partial class Form1 : Form
    {
        public Form1(int? a)
        {
            InitializeComponent();
            button5.Text = "Save";
            button6.Text = "Load";
            label2.Visible = false;
            txtFormato.Visible = false;
            label3.Visible = false;
            txtNome.Visible = false;
            if (a.HasValue)
                Play(a.Value);
        }
        private void Play(int cod)
        {
            string name = string.Empty;
            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");
            MySqlDataReader ms;
            MySqlCommand com = new MySqlCommand("SELECT nome FROM tbl WHERE id=" + cod, con);
            
            try
            {
                con.Open();
                name = com.ExecuteScalar().ToString();
            
                com = new MySqlCommand("SELECT teste FROM tbl WHERE id=" + cod, con);
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
                        string caminho = @"C:\Users\Martinelli\Desktop\" + name + ".mp4";
                        textBox1.Text = caminho;
                        FileStream fs = new FileStream(caminho, FileMode.CreateNew);
                        fs.Write(img, 0, img.Length);
                        fs.Close();
                        axWindowsMediaPlayer1.URL = caminho;
                    }
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNome.Enabled = true;
            txtFormato.Enabled = true;
            try
            {
                OpenFileDialog video = new OpenFileDialog();

                if (video.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.textBox1.Text = video.FileName;
                }

                MessageBox.Show("Press play anytime !", "Just play it!");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
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
            txtNome.Enabled = true;
            label3.Visible = true;
            txtNome.Visible = true;
            txtNome.Focus();
            label2.Text = "*Enter para salvar!";
            //label2.Height = label2.Height - 275;
            this.label2.Location = new System.Drawing.Point(200, 291);
            label2.ForeColor = Color.Red;
            label2.Font = new Font("Verdana", 14);
            label2.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            txtFormato.Visible = true;
            label3.Visible = true;
            txtNome.Visible = true;
            txtNome.Focus();
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void txtFormato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtFormato.Text.Length > 2 && txtNome.Text.Length > 1)
                {
                    MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");
                    MySqlDataReader ms;
                    MySqlCommand com = new MySqlCommand("SELECT teste FROM tbl ORDER BY id DESC", con);

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

                                string caminho = @"C:\Users\Martinelli\Desktop\" + txtNome.Text + "." + txtFormato.Text;
                                textBox1.Text = caminho;
                                FileStream fs = new FileStream(caminho, FileMode.CreateNew);
                                fs.Write(img, 0, img.Length);
                                fs.Close();
                                axWindowsMediaPlayer1.URL = caminho;
                                txtFormato.Enabled = false;
                                txtNome.Enabled = false;
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

        private void txtFormato_Leave(object sender, EventArgs e)
        {
            if (txtFormato.Text.Length > 2 && txtNome.Text.Length > 1)
            {
                MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");
                MySqlDataReader ms;
                MySqlCommand com = new MySqlCommand("SELECT teste FROM tbl ORDER BY id DESC", con);

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

                            //string caminho = @"C:\Users\Martinelli\Desktop\testando1234.mp4";
                            string caminho = @"C:\Users\Martinelli\Desktop\" + txtNome.Text + "." + txtFormato.Text;
                            textBox1.Text = caminho;
                            FileStream fs = new FileStream(caminho, FileMode.CreateNew);
                            fs.Write(img, 0, img.Length);
                            fs.Close();
                            axWindowsMediaPlayer1.URL = caminho;
                            txtFormato.Enabled = false;
                            txtNome.Enabled = false;
                        }
                    }
                }

                catch (Exception ee)
                {
                    //MessageBox.Show(ee.Message.ToString());
                }
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                try
                {
                    txtNome.Enabled = !txtNome.Enabled;
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
                            MySqlCommand com = new MySqlCommand("INSERT INTO tbl VALUES (Null,@nome,@video)", con);
                            com.Parameters.AddWithValue("@nome", txtNome.Text);
                            com.Parameters.AddWithValue("@video", bt);
                            com.ExecuteNonQuery();

                            /*StreamWriter sw = new StreamWriter(@"C:\Users\Martinelli\Desktop\teste.txt");
                            foreach (var item in bt)
                                sw.Write(item);
                            sw.Close();*/

                            //MessageBox.Show("Okay \ti'm guess");
                            MessageBox.Show("Salvo !");
                            txtNome.Clear();
                        }
                        catch (Exception eee)
                        {
                            MessageBox.Show(eee.Message.ToString());
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString());
                }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}