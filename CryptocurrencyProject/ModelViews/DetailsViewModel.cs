using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;
using CryptocurrencyProject.Views;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyProject.ModelViews
{
    public class DetailsViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Currency _selectedCurrency;

        public Currency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set { _selectedCurrency = value; OnPropertyChanged(); }
        }

        public ICommand ShowCandlestickChartCommand { get; }

        public DetailsViewModel(Currency selectedCurrency)
        {
            _apiService = new ApiService();
            SelectedCurrency = selectedCurrency;
            ShowCandlestickChartCommand = new RelayCommand(ShowCandlestickChart);
            LoadCurrencyDetails(selectedCurrency.id);
        }

        /// <summary>
        /// Method to output data as japanese chart
        /// </summary>
        /// <param name="obj">exchangeId or id of currency</param>
        private async void ShowCandlestickChart(object obj)
        {
            var candlestickData = await _apiService.GetCandlestickDataAsync("binance", "d1", SelectedCurrency.id, "usd");

            if (candlestickData != null && candlestickData.Count != 0)
            {
                var candlestickView = new CandlestickChartView
                {
                    DataContext = new CandlestickChartViewModel(candlestickData)
                };
                candlestickView.Show();
            }
            else
            {
                MessageBox.Show("The CoinCap api returns 0 data");
            }
        }

        /// <summary>
        /// Getting data from api by id
        /// </summary>
        /// <param name="currencyId">Name of currency</param>
        private async void LoadCurrencyDetails(string currencyId)
        {
            SelectedCurrency = await _apiService.GetCurrencyDetailsAsync(currencyId);
        }
    }
}
