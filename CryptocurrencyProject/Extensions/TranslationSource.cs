using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows.Data;

namespace CryptocurrencyProject.Extensions
{
    public class TranslationSource : INotifyPropertyChanged
    {
        private readonly ResourceManager _resourceManager = CryptocurrencyProject.Resources.Strings.ResourceManager;
        private CultureInfo _currentCulture;

        private static readonly TranslationSource _instance = new TranslationSource();
        public static TranslationSource Instance => _instance;

        public string? this[string key] => _resourceManager.GetString(key, _currentCulture);

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                _currentCulture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocalizationExtension : Binding
    {
        public LocalizationExtension(string name) : base($"[{name}]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}