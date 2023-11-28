using AdminClient.Model.DataObjects;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AdminClient.Model
{
    internal class SeriesBuilder
    {
        public SeriesBuilder() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        public ISeriesView MakeSeries(Name name, StatTypeEnum statType,ChartTypeEnum chartType ) // Consolidate all methods into here
        {
            switch(chartType)
            {
                case ChartTypeEnum.PieChart:
                    {
                        return PieSeries(name, statType);
                    }

            }


            return new PieSeries();
        }




        private PieSeries PieSeries(Name names, StatTypeEnum statTypeEnum)
        {
            switch (statTypeEnum)
            {
                case StatTypeEnum.Popularity:
                    {
                        return new PieSeries
                        {
                            Title = names.name,
                            Values = new ChartValues<ObservableValue> { new ObservableValue(names.Popularity) },
                            DataLabels = true,
                            LabelPoint = PointLabel
                        };

                    }
                case StatTypeEnum.Occerrence:
                    {
                        return new PieSeries
                        {
                            Title = names.name,
                            Values = new ChartValues<ObservableValue> { new ObservableValue(names.Occerrence) },
                            DataLabels = true,
                            LabelPoint = PointLabel
                        };
                    }
                default: break;
            }
            return null;
        }
       
        public SeriesCollection AddtoSeriesCollection (ref SeriesCollection series, string s, int value )
        {
            
            int i = series.IndexOf(ColumnSeries.NameProperty.Name == s);
            series[i].Values.Add(value);
            return series;
        }
        
        private Func<ChartPoint, string> PointLabel { get; set; }

    }
}
