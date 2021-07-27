using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
namespace DB_project
{
    public partial class Form1 : Form
    {
        account myaccount;
        SQLiteConnection DB;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Enabled = true;
            timer1.Interval = 1000;
            DB = new SQLiteConnection("Data Source = DB.sqlite;Version=3;");
            DB.Open();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myaccount = new account(textBox1.Text, decimal.Parse(textBox2.Text), int.Parse(textBox3.Text));
                string sql = "INSERT INTO acc (name,balance,phone) VALUES ('" + myaccount.name + "'," + myaccount.balance + "," + myaccount.Phone + ")";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
               int x= command.ExecuteNonQuery();
                MessageBox.Show($"It seems that record(s){x} was(were) inserted successfully", "Database Operation Result");
                allclear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "DELETE FROM acc WHERE name = '" + textBox4.Text + "';";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
                int affectedRecords = command.ExecuteNonQuery();
                MessageBox.Show("It seems that " + affectedRecords + " record(s) was(were) deleted successfully", "Database Operation Result");
                allclear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM acc order by phone desc";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listBox1.Items.Add("Name: " + reader["name"] + "\tbalance: " + reader["balance"] + "\t\tphone number:" + reader["phone"]);

                }
                MessageBox.Show("It seems that SELECT statement was executed successfully", "Database Operation Result");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void allclear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            listBox1.Items.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox6.Text;
                decimal b = decimal.Parse(textBox5.Text);
                string sql = "SELECT balance FROM acc WHERE name ='" + name + "'";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
                SQLiteDataReader reader = command.ExecuteReader();
                decimal b2 = decimal.Parse(reader["balance"].ToString());

                if (comboBox1.SelectedIndex == 0)
                {
                    if (b2 < b)
                    {
                        MessageBox.Show("you can't withdraw ", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        b2 = b2 - b;

                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    b2 = b2 + b;
                }
                else
                    MessageBox.Show("sorry you should just select withdraw or deposit", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                string sql2 = "UPDATE acc SET balance =" + b2 + " WHERE name ='" + name + "'";
                SQLiteCommand command2 = new SQLiteCommand(sql2, DB);
                command2.ExecuteNonQuery();
                allclear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var write = new StreamWriter("data.txt");

                write.WriteLine("name \tbalance \tphone number ");
                string sql = "SELECT * FROM acc order by name desc";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    write.WriteLine(reader["name"] + "\t " + reader["balance"] + "\t" + reader["phone"]);

                }
                write.Close();
                MessageBox.Show("now you have a copy from your data ia the file");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var f2 = new Form2();
            f2.Show();
        }
    }
}
