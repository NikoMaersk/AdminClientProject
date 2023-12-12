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
        public List<NameMatch> matches;
        public List<Name> names;
        public List<Users> users;
        public static SeriesFactory instance => _instance;
        private SeriesFactory() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            matches = new List<NameMatch>();
            names = new List<Name>();
            users = new List<Users>();
            UpdateAllMatches();
            UpdateAllNames();
            UpdateAllUser();
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
       
        // Remove to new class
        public async void UpdateAllMatches()
        {
                var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<NameMatch>();
                List<NameMatch> TempList = new List<NameMatch>();
               TempList = await client.GetAll();
            matches = TempList;
        }
        public async void UpdateAllNames()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Name>();
            List<Name> TempList = new List<Name>();
            TempList = await client.GetAll();
            names = TempList;
        }
        public async void UpdateAllUser()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Users>();
            List<Users> TempList = new List<Users>();
            TempList = await client.GetAll();
            
            users = TempList;

        }



        public Name GetNameFromString(string s)
        {
            var name = from Name in names where Name.name == s select Name;
            return name.First();
        }
        public Users GetUsersFromString(string s)
        {
            var user = from Users in users where Users.Email == s select Users;
            return user.First();
        }

        public List<Name> GetMatchedNames(Users user, Users partner)
        {
            List<Name> matched = new List<Name>();
            foreach (var item in user.Names)
            {
                if (partner.Names.Contains(item))
                {
                    matched.Add(GetNameFromString(item));
                }
            }
            return matched;
        }
        private Func<ChartPoint, string> PointLabel { get; set; }

    }
 
}
