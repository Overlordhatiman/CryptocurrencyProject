using CryptocurrencyProject.ModelViews;
using System.Windows.Controls;

namespace CryptocurrencyProject.Views
{
    public partial class CurrencyConverterView : UserControl
    {
        public CurrencyConverterView()
        {
            InitializeComponent();
            DataContext = new CurrencyConverterViewModel();
        }
    }
}
