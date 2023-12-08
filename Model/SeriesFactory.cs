using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AdminClient.Model
{
    internal class SeriesFactory
    {
        private static SeriesFactory _instance = new SeriesFactory();
        public List<NameMatch> matches = new List<NameMatch>();
        public static SeriesFactory instance => _instance;
        private SeriesFactory() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        public ISeriesView MakeSeries(Name name, StatTypeEnum statType) // Consolidate all methods into here
        {
          return PieSeries(name, statType);

        }
        public ISeriesView MakeSeries(Name name, StatTypeEnum statType, ref SeriesCollection series, int index) 
        {
            AddtoSeriesCollection(ref series, name.name, name.Popularity, index);


            return null;

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
        public void addNewSeries(ref SeriesCollection series, string s)
        {
            series.Add(new ColumnSeries { Title = s, Values = { } });
        }
       
        private SeriesCollection AddtoSeriesCollection (ref SeriesCollection series, string s, int value, int index )
        {
            if (series[index].Values ==null)
            {
                series[index].Values = new ChartValues<ObservableValue> { new ObservableValue(value) };

            }
            else 
            {
                series[index].Values.Add(new ObservableValue(value));
            }
            return series;
        }
        public async void UpdateAllMatches()
        {
                var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<NameMatch>();
                List<NameMatch> TempList = await client.GetAll();
            matches = TempList;
        }

        


        public nestedSeries makeNestedSeries(string seriesName)
        {



            return new nestedSeries { SeriresName = seriesName };
        }
        public nestedSeries addNestedSeries(string name, int index)
        {



            return new nestedSeries();
        }

        private Func<ChartPoint, string> PointLabel { get; set; }

    }
 
}
