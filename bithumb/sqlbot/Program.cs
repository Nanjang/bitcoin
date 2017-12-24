using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Open the console. "View" > "Other Windows" > "Package Manager Console"
 * Then type the following:
 * PM> Install-Package Newtonsoft.Json
 */

namespace sqlbot
{
    class Program
    {
        public static string apikey = "apikey.txt";
        public static string secret = "secret.txt";
        public static string sqlconn = "sqlconn.txt";

        static HashSet<string> cache = new HashSet<string>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 120; i++)
            {
                GetTR();
                Thread.Sleep(1000);
            }
        }

        private static void GetTR()
        {
            XCoinAPI hAPI_Svr;
            string sAPI_Key = File.ReadAllText(apikey);
            string sAPI_Secret = File.ReadAllText(secret);
            hAPI_Svr = new XCoinAPI(sAPI_Key, sAPI_Secret);

            string sParams = "order_currency=BTC&payment_currency=KRW";
            string sRespBodyData = String.Empty;

            string response = hAPI_Svr.xcoinApiCall("/public/recent_transactions", sParams, ref sRespBodyData);
            //Console.WriteLine(response);

            //File.WriteAllText("tr.json", response);

            TransactionsRootObject root = JsonConvert.DeserializeObject<TransactionsRootObject>(response);
            Console.WriteLine(root.status);

            if (root.status == "0000")
            {
                Console.WriteLine(root.data.Count);
                foreach (Datum data in root.data.Reverse<Datum>())
                {

                    Console.WriteLine(data.transaction_date);

                    if (!cache.Contains(data.transaction_date + data.price + data.units_traded + data.type))
                    {
                        SqlAdd.InsertTransaction(data.transaction_date, data.price, data.units_traded, data.type);
                        cache.Add(data.transaction_date + data.price + data.units_traded + data.type);
                    }

                }
            }
            //Console.WriteLine("======");
            //string sParamsUT = "searchGb=0&order_currency=BTC";
            //string responseUT = hAPI_Svr.xcoinApiCall("/public/user_transactions", sParamsUT, ref sRespBodyData);
            //Console.WriteLine(responseUT);
        }
    }
}
