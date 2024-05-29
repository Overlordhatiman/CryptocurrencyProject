using CryptocurrencyProject.ModelViews;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrencyProject.Views
{
    public partial class CurrencyConverterView : UserControl
    {
        public CurrencyConverterView()
        {
            InitializeComponent();
            DataContext = new CurrencyConverterViewModel();
            UpdateLanguage();
        }

        public void UpdateLanguage()
        {
            if (DataContext is CurrencyConverterViewModel viewModel)
            {
                viewModel.UpdateLanguage();
            }
        }
    }
}
