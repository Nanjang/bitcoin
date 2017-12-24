using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlbot
{
    class SqlAdd
    {
        public static void InsertTransaction(string time, string price, string units, string type)
        {
            //"Server=서버주소;Port=포트3306;Database=데이터베이스;Uid=유저;Pwd=패스워드;";
            // MariaDB10이면 포트 3307
            string strConn = File.ReadAllText(Program.sqlconn); 

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = string.Format("INSERT INTO `coin`.`transactions_btc` (`time`, `price`, `units`, `type`) VALUES ('{0}', '{1}', '{2}', '{3}');",time,price,units,type);
                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }


}
