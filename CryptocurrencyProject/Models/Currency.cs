using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyProject.Models
{
    public class Currency
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string currencySymbol { get; set; }
        public string type { get; set; }
        public string rateUsd { get; set; }
    }

    public class RootObject
    {
        public List<Currency> data { get; set; }
        public long timeStamp { get; set; }
    }
}
