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

        private async void RefreshCurrencies(object obj)
        {
            SearchQuery = string.Empty;
            await LoadDataAsync();
        }

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

        private void ChangeLanguage(object obj)
        {
            string cultureName = obj as string;
            if (!string.IsNullOrEmpty(cultureName))
            {
                Localizer.SetCulture(cultureName);
                UpdateLanguage();
                _converterViewModel.UpdateLanguage();
            }
        }

        public string CurrencyList => Localizer.GetString("CurrencyList");
        public string CurrencyConverter => Localizer.GetString("CurrencyConverter");
        public string Search => Localizer.GetString("Search");
        public string Refresh => Localizer.GetString("Refresh");
        public string IdLabel => Localizer.GetString("Id");
        public string RankLabel => Localizer.GetString("Rank");
        public string NameLabel => Localizer.GetString("Name");
        public string ViewDetailsLabel => Localizer.GetString("ViewDetails");

        public void UpdateLanguage()
        {
            OnPropertyChanged(nameof(CurrencyList));
            OnPropertyChanged(nameof(CurrencyConverter));
            OnPropertyChanged(nameof(Search));
            OnPropertyChanged(nameof(Refresh));
            OnPropertyChanged(nameof(IdLabel));
            OnPropertyChanged(nameof(RankLabel));
            OnPropertyChanged(nameof(NameLabel));
            OnPropertyChanged(nameof(ViewDetailsLabel));
        }
    }
}
