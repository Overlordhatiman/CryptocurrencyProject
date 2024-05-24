using CryptocurrencyProject.Models;
using CryptocurrencyProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyProject.ModelViews
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Currency> _data;

        public ObservableCollection<Currency> Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            _apiService = new ApiService();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var data = await _apiService.GetTop10CurrenciesAsync();
            Data = new ObservableCollection<Currency>(data);
        }
    }
}
