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
        public List<Name> names = new List<Name>();
        public static SeriesFactory instance => _instance;
        private SeriesFactory() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            names.Add(new Name { Id = 1, name = "test", Popularity = 1, Occerrence = 111, Gender = Gender.Male });
            names.Add(new Name { Id = 1, name = "test2", Popularity = 2, Occerrence = 111, Gender = Gender.Female });
            names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.Unisex });
            names.Add(new Name { Id = 1, name = "test4", Popularity = 4, Occerrence = 111, Gender = Gender.Female });
        }

        public ISeriesView MakeSeries(Name name, StatTypeEnum statType) // Consolidate all methods into here¨, determine chartype before call
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
        public void addNewSeries(ref SeriesCollection series, Name name)
        {
            //, LabelPoint = PointLabel => PointLabel.Y + name.name 
            ColumnSeries columnSeries= new ColumnSeries 
            {
                Title = name.name,
                Values = new ChartValues<int> { name.Popularity, name.Occerrence },

            };
            series.Add(columnSeries);
            //series.Add(new ColumnSeries { Title = name.name, Values = {name.Popularity,name.Occerrence }});
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
        public Name GetNameFromString(string s)
        {
            var name = from Name in names where Name.name == s select Name;
            return name.First();
        }
        private Func<ChartPoint, string> PointLabel { get; set; }

    }
 
}
