using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using CryptocurrencyProject.Extensions;
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
        private string _searchQuery;
        private CurrencyConverterViewModel _converterViewModel;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set { _searchQuery = value; OnPropertyChanged(); }
        }

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
        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ChangeLanguageCommand { get; }

        public MainViewModel()
        {
            _apiService = new ApiService();
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            SearchCommand = new RelayCommand(SearchCurrency);
            RefreshCommand = new RelayCommand(RefreshCurrencies);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            _converterViewModel = new CurrencyConverterViewModel();
            LoadDataAsync();
        }

        /// <summary>
        /// Refresh UI after serach
        /// </summary>
        /// <param name="obj"></param>
        private async void RefreshCurrencies(object obj)
        {
            SearchQuery = string.Empty;
            await LoadDataAsync();
        }

        /// <summary>
        /// Getting data from api
        /// </summary>
        /// <param name="obj">Seraching for currency by id or name</param>
        private async void SearchCurrency(object obj)
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var data = await _apiService.SearchCurrencyByNameAsync(SearchQuery);
                if (data != null)
                {
                    Data = new ObservableCollection<Currency> { data };
                }
                else
                {
                    Data.Clear();
                }
            }
        }

        /// <summary>
        /// Loading top 10(by default) currencies
        /// </summary>
        /// <returns>Collections of data</returns>
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

        /// <summary>
        /// Method to change localization
        /// </summary>
        /// <param name="obj">String of language</param>
        private void ChangeLanguage(object obj)
        {
            string cultureName = obj as string;
            if (!string.IsNullOrEmpty(cultureName))
            {
                TranslationSource.Instance.CurrentCulture = new CultureInfo(cultureName);
            }
        }
    }
}
