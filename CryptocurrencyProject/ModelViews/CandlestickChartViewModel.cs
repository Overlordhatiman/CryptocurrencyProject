using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CryptocurrencyProject.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace CryptocurrencyProject.ModelViews
{
    public class CandlestickChartViewModel : BaseViewModel
    {
        public ObservableCollection<OhlcPoint> OhlcData { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public CandlestickChartViewModel(List<OhlcPoint> data)
        {
            OhlcData = new ObservableCollection<OhlcPoint>(data);

            SeriesCollection = new SeriesCollection
            {
                new OhlcSeries
                {
                    Values = new ChartValues<OhlcPoint>(OhlcData.Select(c => new OhlcPoint(
                        c.open,
                        c.high,
                        c.low,
                        c.close
                    )))
                }
            };
        }
    }
}
