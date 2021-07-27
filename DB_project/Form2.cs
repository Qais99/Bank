using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
namespace DB_project
{
    public partial class Form2 : Form
    {
        SQLiteConnection DB;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                DB = new SQLiteConnection("Data Source = DB.sqlite;Version=3;");
                DB.Open();
                var read = new StreamReader("data.txt");
                string[] q;
                while (!read.EndOfStream)
                {
                    q = read.ReadLine().Split('\t');

                    label1.Text = label1.Text + q[0];
                    for (int i = 0; i < 20 - q[0].Length; i++)
                        label1.Text += "  ";
                    label1.Text += q[1];
                    for (int i = 0; i < 20 - q[1].Length; i++)
                        label1.Text += "  ";
                    label1.Text += q[2]+"\n";
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
