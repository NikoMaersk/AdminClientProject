using AdminClient.Model.DataObjects;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Model
{
    internal class SeriesBuilder
    {
        public SeriesBuilder() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }
        public List<PieSeries> PieSeriesPopularity(List<Name> names)
        {
            List<PieSeries> series = new List<PieSeries>();
            foreach (var item in names)
            {
                series.Add(new PieSeries {
                Title = item.name, 
                Values = new ChartValues<ObservableValue> { new ObservableValue(item.Popularity)},
                    DataLabels = true,
                    LabelPoint = PointLabel

                });
            }


            return series;
        }
        public List<PieSeries> PieSeriesOccerrence(List<Name> names)
        {
            List<PieSeries> series = new List<PieSeries>();
            foreach (var item in names)
            {
                series.Add(new PieSeries
                {
                    Title = item.name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(item.Occerrence) },
                    DataLabels = true,
                    LabelPoint = PointLabel

                });
            }


            return series;
        }
        private Func<ChartPoint, string> PointLabel { get; set; }

    }
}
