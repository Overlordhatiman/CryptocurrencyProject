using CryptocurrencyProject.Models;
using CryptocurrencyProject.ModelViews;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace CryptocurrencyProject.Views
{
    public partial class DetailsView : Window
    {
        public DetailsView(Currency selectedCurrency)
        {
            InitializeComponent();
            DataContext = new DetailsViewModel(selectedCurrency);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.ToString(),
                UseShellExecute = true
            });
        }
    }
}
