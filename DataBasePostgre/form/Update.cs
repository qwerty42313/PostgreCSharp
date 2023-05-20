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
using System.Xml;

namespace DataBasePostgre
{
    public partial class Update : Form
    {
        public NpgsqlConnection connection;
        Label[] label = new Label[Data.Colums];
        TextBox[] textBox = new TextBox[Data.Colums];
        int numb = 0; int point = 0; int index;
        public Update()
        {
            connection = new NpgsqlConnection($"Server={Data.server};Port={Data.post};User Id={Data.user_id};Password={Data.password};Database={Data.database}");
            InitializeComponent();
            GetIndex();
            int i = 0;
            do
            {
                label[i] = new Label();
                label[i].Text = Data.NameColum[i];
                textBox[i] = new TextBox();
                this.Controls.Add(textBox[i]);
                this.Controls.Add(label[i]);
                i++;
            }
            while (i < Data.Colums);
            GetControls(225, 25);
        }
        private void GetControls(int x, int y)
        {
            if (point < Data.Colums && numb == 0)
            {
                textBox[point].Location = new Point(x + 50, y);
                label[point].Location = new Point(x, y);
                point++; numb = 1;
                GetControls(x, y);
            }
            else if (point < Data.Colums && numb == 1)
            {
                textBox[point].Location = new Point(x + 50, y + 30);
                label[point].Location = new Point(x, y + 30);
                point++; numb = 0; x += 225;
                GetControls(x, y);
            }
        }
        private void GetIndex()
        {
            for(int i = 0; i < Data.Colums; i++)
            {
                if (Data.NameColum[i] == "id")
                {
                    index = i; break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            for(int i = 0; i < Data.Colums; i++)
            {
                string sql = $"UPDATE {Data.nameDatabase} SET";
                if (i == index) continue;
                sql  = sql +  $" {Data.NameColum[i]}='{textBox[i].Text}' WHERE {Data.NameColum[index]}='{textBox[index].Text}'";
                connection.Open();
                var command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = sql;
                command.ExecuteNonQuery();
                connection.Close();
            }
            MessageBox.Show("Done");

        }

        private void Update_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.NameColum.Clear();
        }
    }
}
