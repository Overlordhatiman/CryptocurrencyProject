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

        public string CurrencyDetailsLabel => Localizer.GetString("CurrencyDetailsLabel");
        public string IdLabel => Localizer.GetString("Id");
        public string RankLabel => Localizer.GetString("Rank");
        public string NameLabel => Localizer.GetString("Name");
        public string ExplorerLabel => Localizer.GetString("ExplorerLabel");
        public string ShowCandlestickChartLabel => Localizer.GetString("ShowCandlestickChartLabel");
        public string MarketsLabel => Localizer.GetString("MarketsLabel");
        public string ExchangeIdLabel => Localizer.GetString("ExchangeIdLabel");
        public string BaseSymbolLabel => Localizer.GetString("BaseSymbolLabel");
        public string QuoteSymbolLabel => Localizer.GetString("QuoteSymbolLabel");
        public string VolumeUsdLabel => Localizer.GetString("VolumeUsdLabel");
        public string PriceUsdLabel => Localizer.GetString("PriceUsdLabel");
        public string SymbolLabel => Localizer.GetString("SymbolLabel");
        public string ChangeLabel => Localizer.GetString("ChangeLabel");

        public void UpdateLanguage()
        {
            OnPropertyChanged(nameof(IdLabel));
            OnPropertyChanged(nameof(RankLabel));
            OnPropertyChanged(nameof(NameLabel));
            OnPropertyChanged(nameof(ExplorerLabel));
            OnPropertyChanged(nameof(ShowCandlestickChartLabel));
            OnPropertyChanged(nameof(MarketsLabel));
            OnPropertyChanged(nameof(ExchangeIdLabel));
            OnPropertyChanged(nameof(BaseSymbolLabel));
            OnPropertyChanged(nameof(QuoteSymbolLabel));
            OnPropertyChanged(nameof(VolumeUsdLabel));
            OnPropertyChanged(nameof(PriceUsdLabel));
        }
    }
}
