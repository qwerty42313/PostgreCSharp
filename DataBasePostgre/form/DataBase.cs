using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataBasePostgre
{
    public partial class DataBase : Form
    {
        public NpgsqlConnection connection;
        public DataBase()
        {
            InitializeComponent();
            connection = new NpgsqlConnection($"Server={Data.server};Port={Data.post};User Id={Data.user_id};Password={Data.password};Database={Data.database}");
            SelectData();
        }

        private void SelectData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            connection.Close(); 
            connection.Open();
            NpgsqlDataAdapter select = new NpgsqlDataAdapter($"select * from {Data.nameDatabase}", connection);
            select.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            connection.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM {Data.nameDatabase} WHERE id =" + textBox1.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("Данные удалены");
            SelectData();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"{textBox2.Text};";
            command.ExecuteNonQuery();
            SelectData();
            MessageBox.Show("Запрос выполнен");
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Data.NameColum.Add(dataGridView1.Columns[i].HeaderText);
                Data.Colums = dataGridView1.Columns.Count;
            }
            Data.deleteColum();
            New newF = new New();
            newF.ShowDialog();
            SelectData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Data.NameColum.Add(dataGridView1.Columns[i].HeaderText);
                Data.Colums = dataGridView1.Columns.Count;
            }
            Update update = new Update();
            update.ShowDialog();
            SelectData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if (textBox3.Text != " " || textBox3.Text != String.Empty)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(textBox3.Text.ToLower()))
                            {
                                if (!dataGridView1.Rows[i].Visible)
                                {
                                    dataGridView1.Rows[i].Visible = true;
                                }
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Selected != true)
                {
                    dataGridView1.CurrentCell = null;
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {

                        dataGridView1.Rows[i].Visible = false;
                    }

                }


            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SelectData();
        }
    }
}
