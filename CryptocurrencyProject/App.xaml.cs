using System;
using System.Linq;
using System.Windows;

namespace CryptocurrencyProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeTheme(string theme)
        {
            ResourceDictionary newTheme = new ResourceDictionary();
            switch (theme)
            {
                case "Light":
                    newTheme.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);
                    break;
                case "Dark":
                default:
                    newTheme.Source = new Uri("Resources/DarkTheme.xaml", UriKind.Relative);
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
