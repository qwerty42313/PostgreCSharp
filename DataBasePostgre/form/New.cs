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
    public partial class New : Form
    {
        public NpgsqlConnection connection;
        private TextBox[] textBox = new TextBox[Data.Colums];
        private Label[] label = new Label[Data.Colums];
        int point = 0, numb = 0;
        public New()
        {
            connection = new NpgsqlConnection($"Server={Data.server};Port={Data.post};User Id={Data.user_id};Password={Data.password};Database={Data.database}");
            InitializeComponent();
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
            while(i < Data.Colums);
            GetControls(225, 25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"insert into {Data.nameDatabase} (";
            int i = 0;
            do
            {
                if (i == 0)
                    sql += $"{Data.NameColum[i]} ";
                else if(i > 0)
                    sql += $",{ Data.NameColum[i]} ";
                i++;
            } while(i < Data.Colums);
            i = 0;
            do
            {
                if (i == 0)
                    sql += $") values ('{textBox[i].Text}' ";
                else if (i > 0 && i != Data.Colums - 1)
                    sql += $",'{textBox[i].Text}' ";
                else if (i == (Data.Colums - 1))
                    sql += $",'{textBox[i].Text}')";
                i++;
            } while (i < Data.Colums);
            connection.Open();
            var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            MessageBox.Show("Done");
            connection.Close();
        }

        private void New_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.NameColum.Clear();
        }

        private void GetControls(int x, int y)
        {
            if(point < Data.Colums && numb == 0)
            {
                textBox[point].Location = new Point(x + 50, y);
                label[point].Location = new Point(x, y);
                point ++; numb = 1; 
                GetControls(x, y);
            }
            else if(point < Data.Colums && numb == 1) 
            {
                textBox[point].Location = new Point(x + 50, y + 30);
                label[point].Location = new Point(x, y + 30);
                point++; numb = 0; x += 225;
                GetControls(x, y);
            }
        }
    }
}
