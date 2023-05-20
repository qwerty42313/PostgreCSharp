using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasePostgre
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Data.server = textBox1.Text;
                Data.post = textBox2.Text;
                Data.user_id = textBox3.Text;
                Data.password = textBox4.Text;
                Data.database = textBox5.Text;
                Data.nameDatabase = textBox6.Text;
                connection = new NpgsqlConnection($"Server={Data.server};Port={Data.post};User Id={Data.user_id};Password={Data.password};Database={Data.database}");
                connection.Open();
                connection.Close();
                DataBase database = new DataBase();
                database.ShowDialog();
            }
           catch 
            {
                MessageBox.Show("Введите корректные данные");
            }
            
        }
    }
}
