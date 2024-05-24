using CryptocurrencyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<List<Currency>> GetTop10CurrenciesAsync()
        {
            var url = "https://api.coincap.io/v2/assets?limit=10";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    RootObject data = JsonSerializer.Deserialize<RootObject>(json);

                    return data.data;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return null;
        }
    }
}
