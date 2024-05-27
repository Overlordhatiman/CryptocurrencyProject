using System.Text.Json.Serialization;

namespace CryptocurrencyProject.Models
{
    public class RateData
    {
        public string id { get; set; }

        public string symbol { get; set; }

        public string currencySymbol { get; set; }

        public string type { get; set; }

        public string rateUsd { get; set; }
    }

    public class RateResponse
    {
        public RateData data { get; set; }

        public long timestamp { get; set; }
    }
}
