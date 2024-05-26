using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;
using CryptocurrencyProject.Views;
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

        private async void ShowCandlestickChart(object obj)
        {
            var candlestickData = await _apiService.GetCandlestickDataAsync("binance", "d1", SelectedCurrency.id, "usd");

            if (candlestickData != null)
            {
                var candlestickView = new CandlestickChartView
                {
                    DataContext = new CandlestickChartViewModel(candlestickData)
                };
                candlestickView.Show();
            }
        }

        private async void LoadCurrencyDetails(string currencyId)
        {
            SelectedCurrency = await _apiService.GetCurrencyDetailsAsync(currencyId);
        }
    }
}
