using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Calendar = System.Windows.Controls.Calendar;

namespace AdminClient.ViewModels
{
    internal class MatchStatsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public DateTime _startDate { get; set; }
        public DateTime _endDate { get; set; }

        public ICommand SelectedMatches { get; set; }
        public ICommand SelectedStartDate { get; set; }

        private ObservableCollection<NameMatch> FoundMatches { get; set; }
        private List<DateTime> dates = new List<DateTime>();
        private List<NameMatch> matchesSeries = new List<NameMatch>();
        private ObservableCollection<Name> MatchedNames = new ObservableCollection<Name>();
        private SeriesCollection seriesViews = new SeriesCollection();
        private string[] labels = { "Popularity", "Occerrence" };
        private List<NameMatch> Matches { get; set; }



        public MatchStatsViewModel()
        {

            SelectedStartDate = new DelegateCommand((Object e) => DatePicked(e));
            _startDate = new DateTime();

            FoundMatches = new ObservableCollection<NameMatch>();
            Matches = new List<NameMatch>();

            Matches.Add(new NameMatch("1", DateTime.Now, "test"));
            Matches.Add(new NameMatch("2", DateTime.Now, "test2"));
            Matches.Add(new NameMatch("3", DateTime.Now, "test3"));
            Matches.Add(new NameMatch("4", DateTime.Now, "test4"));

            /*
            SeriesFactory.instance.UpdateAllMatches();
            foreach (var item in SeriesFactory.instance.matches)
            {
                Matches.Add(item);
            }
            */
        }


        #region Properties
        public ObservableCollection<NameMatch> MatchesFound {
            get => FoundMatches;
            set => FoundMatches = value;

        }
        public ObservableCollection<Name> nameList { get => MatchedNames; set { } }
        public SeriesCollection series { get => seriesViews; set { } }
        public string StartDate
        {
            get
            {
                if (dates.Count > 0)
                {
                    return dates[0].ToShortDateString();
                }
                else
                {
                    return "";
                }
                ;

            }
            set { }
        }
        public string EndDate

        {
            get
            {
                if (dates.Count > 1)
                {
                    return dates[1].ToShortDateString();
                }
                else
                {
                    return "";
                }
                ;

            }
            set { }
        }
        public string[] getLabels { get => labels; set { } }

        #endregion
        #region EventHandlers

        private void DatePicked(Object sender)
        {
            Calendar calendar = sender as Calendar;
            _startDate = (DateTime)calendar.SelectedDate;

            switch (dates.Count)
            {
                case 0:
                    dates.Add((DateTime)calendar.SelectedDate);
                    break; 
                case 1:
                    dates.Add((DateTime)calendar.SelectedDate);
                    break;
                case 2:
                    dates.Clear();
                    dates.Add((DateTime)calendar.SelectedDate);
                    FoundMatches.Clear();
                    seriesViews.Clear();
                    MatchedNames.Clear();
                    break;

            }
            OnPropertyChanged("StartDate");
            OnPropertyChanged("EndDate");
            if (dates.Count ==2)
            {
              
                int CompareResultStart = 0;
                int CompareResultEnd = 0;
                foreach (var item in Matches)
                {
                    CompareResultStart = item.Date.CompareTo(dates[0]);
                    CompareResultEnd = item.Date.CompareTo(dates[1]);

                    if (CompareResultStart >= 0 && CompareResultEnd <= 0)
                    {
                        FoundMatches.Add(item);
                    }
                }
                foreach (var match in FoundMatches)
                {
                    Name tempName = SeriesFactory.instance.GetNameFromString(match.Name);
                   if (MatchedNames.Contains(tempName)) { break; }
                   else { MatchedNames.Add(tempName); SeriesFactory.instance.addNewSeries(ref seriesViews, tempName); }


                }

            }
            
            

        }
        private NameMatch _SelectedMatches
        {
            get
            {
                return null;
            }
            set 
            { 
                if (matchesSeries.Contains(value)) 
                {
                    matchesSeries.Remove(value);
                }
                else
                {
                    matchesSeries.Add(value);
                }
            
            }

        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
