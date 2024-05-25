using CryptocurrencyProject.Models;
using CryptocurrencyProject.ModelViews;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrencyProject.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currency = button.DataContext as Currency;
            if (currency != null)
            {
                var detailsView = new DetailsView(currency);
                detailsView.Show();
            }
        }
    }
}
