using System.Collections.Generic;

namespace CryptocurrencyProject.Models
{
    public class Market
    {
        public string exchangeId { get; set; }
        public string baseId { get; set; }
        public string quoteId { get; set; }
        public string baseSymbol { get; set; }
        public string quoteSymbol { get; set; }
        public string priceUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string volumePercent { get; set; }
        public long updated { get; set; }
    }

    public class Currency
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string supply { get; set; }
        public string maxSupply { get; set; }
        public string marketCapUsd { get; set; }
        public string priceUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string changePercent24Hr { get; set; }
        public string vwap24Hr { get; set; }
        public string explorer { get; set; }
        public List<Market> markets { get; set; }
    }

    public class RootObjectList<T>
    {
        public List<T> data { get; set; }
        public long timestamp { get; set; }
    }

    public class RootObject<T>
    {
        public T data { get; set; }
        public long timestamp { get; set; }
    }
}