using System;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Data;

namespace VideoClipe
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1(null);
            f1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=vivi;user id=root;pwd=");
                DataTable dt = new DataTable();
                MySqlCommand com = new MySqlCommand("SELECT id AS 'Codigo do video',nome AS 'Nome do arquivo' FROM tbl ORDER BY id ASC", con);
                try
                {
                    con.Open();
                    com.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString());
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // é um cabeçalho ?
            if (e.RowIndex > -1)
            {
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                Form1 f1 = new Form1((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                f1.Show();
                Hide();
            }
        }
    }
}