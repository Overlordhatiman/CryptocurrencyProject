using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;
using CryptocurrencyProject.Views;

namespace CryptocurrencyProject.ModelViews
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Currency> _data;
        private Currency _selectedCurrency;

        public ObservableCollection<Currency> Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(); }
        }

        public Currency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set { _selectedCurrency = value; OnPropertyChanged(); }
        }

        public ICommand ShowDetailsCommand { get; }

        public MainViewModel()
        {
            _apiService = new ApiService();
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var data = await _apiService.GetTopNCurrenciesAsync();
            Data = new ObservableCollection<Currency>(data);
        }

        private void ShowDetails(object obj)
        {
            if (SelectedCurrency != null)
            {
                var detailsView = new DetailsView(SelectedCurrency);
                detailsView.Show();
            }
        }
    }
}
