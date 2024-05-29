﻿using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyProject.ModelViews
{
    public class CurrencyConverterViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Currency> Currencies { get; }
        public Currency SelectedFromCurrency { get; set; }
        public Currency SelectedToCurrency { get; set; }
        public string Amount { get; set; }
        public string ConversionResult { get; set; }

        public ICommand ConvertCommand { get; }

        public CurrencyConverterViewModel()
        {
            _apiService = new ApiService();
            Currencies = new ObservableCollection<Currency>();
            LoadCurrencies();

            ConvertCommand = new RelayCommand(async (obj) => await ConvertCurrency());
        }

        private async void LoadCurrencies()
        {
            var currencies = await _apiService.GetCurrenciesAsync();
            foreach (var currency in currencies)
            {
                Currencies.Add(currency);
            }
        }

        private async Task ConvertCurrency()
        {
            if (SelectedFromCurrency != null && SelectedToCurrency != null && decimal.TryParse(Amount, out decimal amount))
            {
                var conversionRate = await _apiService.GetConversionRateAsync(SelectedFromCurrency.id, SelectedToCurrency.id);
                var convertedAmount = amount * conversionRate;

                ConversionResult = $"{Amount} {SelectedFromCurrency.symbol} = {convertedAmount:F2} {SelectedToCurrency.symbol}";
                OnPropertyChanged(nameof(ConversionResult));
            }
        }

        public string CurrencyConverterLabel => Localizer.GetString("CurrencyConverter");
        public string FromLabel => Localizer.GetString("FromLabel");
        public string ToLabel => Localizer.GetString("ToLabel");
        public string AmountLabel => Localizer.GetString("AmountLabel");
        public string ConvertButtonLabel => Localizer.GetString("ConvertButtonLabel");
        public string ResultLabel => Localizer.GetString("ResultLabel");

        public void UpdateLanguage()
        {
            OnPropertyChanged(nameof(CurrencyConverterLabel));
            OnPropertyChanged(nameof(FromLabel));
            OnPropertyChanged(nameof(ToLabel));
            OnPropertyChanged(nameof(AmountLabel));
            OnPropertyChanged(nameof(ConvertButtonLabel));
            OnPropertyChanged(nameof(ResultLabel));
        }
    }
}
