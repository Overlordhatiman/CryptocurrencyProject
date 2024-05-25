using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;

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

        public DetailsViewModel(Currency selectedCurrency)
        {
            _apiService = new ApiService();
            SelectedCurrency = selectedCurrency;
            LoadCurrencyDetails(selectedCurrency.id);
        }

        private async void LoadCurrencyDetails(string currencyId)
        {
            SelectedCurrency = await _apiService.GetCurrencyDetailsAsync(currencyId);
        }
    }
}
