﻿using CryptocurrencyProject.Models;
using CryptocurrencyProject.ModelViews;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrencyProject.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        private readonly CurrencyConverterView _viewConverter;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            _viewConverter = new CurrencyConverterView();
            DataContext = _viewModel;
        }

        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            string currentTheme = (string)ToggleThemeButton.Content;

            if (currentTheme == "Switch to Light")
            {
                app.ChangeTheme("Light");
                ToggleThemeButton.Content = "Switch to Dark";
            }
            else
            {
                app.ChangeTheme("Dark");
                ToggleThemeButton.Content = "Switch to Light";
            }
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

        private void SwitchLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            Localizer.SetCulture("uk-UA");
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.UpdateLanguage();
            }

            if (_viewConverter.DataContext is CurrencyConverterViewModel converterViewModel)
            {
                converterViewModel.UpdateLanguage();
            }
        }
    }
}
