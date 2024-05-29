using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptocurrencyProject.Converters
{
    /// <summary>
    /// Needed for LiveChartWPF for correct data
    /// </summary>
    public class DateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long timestamp)
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                return dateTimeOffset.DateTime.ToString("MM/dd/yyyy");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
