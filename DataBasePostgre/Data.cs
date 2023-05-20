using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasePostgre
{
    internal class Data
    {
        public static string server {get; set;}
        public static string post {get; set;}
        public static string user_id {get; set;}
        public static string password {get; set;}
        public static string database { get; set;}
        public static string nameDatabase { get; set; }


        public static int Colums { get; set; }
        public static List<string> NameColum = new List<string>();
        public static void deleteColum()
        {
            for(int i = 0; i < Colums; i++)
            {
                if (NameColum[i] == "id")
                {
                    NameColum.RemoveAt(i);
                    Colums = Colums - 1;
                }
            }
        }
    }
}
