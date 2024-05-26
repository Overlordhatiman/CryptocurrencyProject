using CryptocurrencyProject.Models;
using System;
using System.Collections.Generic;
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