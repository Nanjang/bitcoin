using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlbot
{
    public class Datum
    {
        public string transaction_date { get; set; }
        public string type { get; set; }
        public string units_traded { get; set; }
        public string price { get; set; }
        public string total { get; set; }
    }

    public class TransactionsRootObject
    {
        public string status { get; set; }
        public List<Datum> data { get; set; }
    }
}
