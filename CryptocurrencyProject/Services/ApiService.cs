using CryptocurrencyProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptocurrencyProject.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Getting list of currencies from coincap
        /// </summary>
        /// <returns>List of currencies</returns>
        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            var url = "https://api.coincap.io/v2/assets";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<RootObjectList<Currency>>(json);

                return root?.data ?? new List<Currency>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return new List<Currency>();
        }

        /// <summary>
        /// Getting data with rates in USD
        /// </summary>
        /// <param name="currencyId">Name or id of a currency</param>
        /// <returns>Rate in USD as decimal if data is null returns 0.0</returns>
        public async Task<decimal> GetRateUsdAsync(string currencyId)
        {
            var url = $"https://api.coincap.io/v2/rates/{currencyId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var rateData = JsonSerializer.Deserialize<RateResponse>(json);

                if (rateData?.data?.rateUsd != null)
                {
                    if (decimal.TryParse(rateData.data.rateUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rateUsd))
                    {
                        return rateUsd;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON Error: " + ex.Message);
            }

            return 0.0m;
        }

        /// <summary>
        /// Method to calculate from one currency to another
        /// </summary>
        /// <param name="fromCurrencyId">Id or name first currency which we will convert</param>
        /// <param name="toCurrencyId">Id or name in which currency we will convert</param>
        /// <returns>Decimal how much we cna get currencies</returns>
        public async Task<decimal> GetConversionRateAsync(string fromCurrencyId, string toCurrencyId)
        {
            decimal fromRateUsd = await GetRateUsdAsync(fromCurrencyId);
            decimal toRateUsd = await GetRateUsdAsync(toCurrencyId);

            if (fromRateUsd > 0 && toRateUsd > 0)
            {
                return fromRateUsd / toRateUsd;
            }

            return 0.0m;
        }

        /// <summary>
        /// Getting top currencies from coincap api
        /// </summary>
        /// <param name="limit">Number of currencies(default 10)</param>
        /// <returns>List of currencies</returns>
        public async Task<List<Currency>> GetTopNCurrenciesAsync(int limit = 10)
        {
            var url = $"https://api.coincap.io/v2/assets?limit={limit}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<RootObjectList<Currency>>(json);

                return root.data;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Getting data from api about currency
        /// </summary>
        /// <param name="currencyId">Id or name of currency</param>
        /// <returns>Single currency</returns>
        public async Task<Currency> GetCurrencyDetailsAsync(string currencyId)
        {
            var url = $"https://api.coincap.io/v2/assets/{currencyId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<RootObject<Currency>>(json);

                var currency = root?.data;

                if (currency != null)
                {
                    var marketsResponse = await _httpClient.GetAsync($"https://api.coincap.io/v2/assets/{currencyId}/markets");
                    marketsResponse.EnsureSuccessStatusCode();

                    var marketsJson = await marketsResponse.Content.ReadAsStringAsync();
                    var marketsRoot = JsonSerializer.Deserialize<RootObjectList<Market>>(marketsJson);

                    currency.markets = marketsRoot?.data;
                }

                return currency;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Seraching currency in coincap
        /// </summary>
        /// <param name="partialName">Searching closest id or name of currency in coincap api</param>
        /// <returns>Single currency</returns>
        public async Task<Currency> SearchCurrencyByNameAsync(string partialName)
        {
            var url = "https://api.coincap.io/v2/assets";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<RootObjectList<Currency>>(json);

                var allCurrencies = root?.data;
                if (allCurrencies == null)
                    return null;

                var closestMatch = allCurrencies
                    .Where(c => c.id.Contains(partialName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(c => c.id.Length)
                    .FirstOrDefault();

                return closestMatch;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Getting data(as object of OHLC)
        /// </summary>
        /// <param name="exchange">Exchange of currency</param>
        /// <param name="interval">Interval(like 1d,1h maximum 1m etc)</param>
        /// <param name="baseId">Based id of currency</param>
        /// <param name="quoteId">Quote id of currency</param>
        /// <returns>List of OHLC objects</returns>
        public async Task<List<OhlcPoint>> GetCandlestickDataAsync(string exchange, string interval, string baseId, string quoteId)
        {
            var url = $"https://api.coincap.io/v2/candles?exchange={exchange}&interval={interval}&baseId={baseId}&quoteId={quoteId}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonSerializer.Deserialize<RootObjectList<OhlcPoint>>(json);

                return root?.data ?? new List<OhlcPoint>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
                return new List<OhlcPoint>();
        }
    }
}