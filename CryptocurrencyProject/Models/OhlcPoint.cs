using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyProject.Models
{
    public class OhlcPoint
    {
        public OhlcPoint(double open, double high, double low, double close)
        {
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
        }

        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public long period { get; set; }
    }

}
