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
        
        public PieSeries PieSeriesPopularity(Name names, StatTypeEnum statTypeEnum)
        {
            List<PieSeries> series = new List<PieSeries>();

            return new PieSeries
            {
                Title = names.name,
                Values = new ChartValues<ObservableValue> { new ObservableValue(names.Popularity) },
                DataLabels = true,
                LabelPoint = PointLabel


            };
        }
        public PieSeries PieSeriesOccerrence(Name names, StatTypeEnum statTypeEnum)
        {
            List<PieSeries> series = new List<PieSeries>();

            return new PieSeries
            {
                Title = names.name,
                Values = new ChartValues<ObservableValue> { new ObservableValue(names.Occerrence) },
                DataLabels = true,
                LabelPoint = PointLabel


            };
        }
        public ChartValues<int> LineSeriesPopularity()
        {
            return new ChartValues<int>
            {   
                
            };

        }
        
        private Func<ChartPoint, string> PointLabel { get; set; }

    }
}
