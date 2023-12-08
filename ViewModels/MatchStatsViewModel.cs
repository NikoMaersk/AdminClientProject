using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Calendar = System.Windows.Controls.Calendar;

namespace AdminClient.ViewModels
{
    internal class MatchStatsViewModel : ViewModelBase
    {

        private string StartDate {  get; set; }
        private string EndDate { get; set; }
        
        private ObservableCollection<NameMatch> FoundMatches { get; set; }
        private List<NameMatch> Matches { get; set; }



        public MatchStatsViewModel() 
        {
            FoundMatches = new ObservableCollection<NameMatch>();
            SeriesFactory.instance.UpdateAllMatches();
            foreach (var item in SeriesFactory.instance.matches)
            {
                Matches.Add(item);
            }
        }

        #region Properties
        public ObservableCollection<NameMatch> MatchesFound { 
            get => FoundMatches; 
            set => FoundMatches = value; 
        
        }

        #endregion
        #region EventHandlers
        public void DatePicked(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            Calendar calendar = sender as Calendar;
            SelectedDatesCollection dateTimes = calendar.SelectedDates;
            DateTime dateTimeStart = dateTimes[0];
            DateTime dateTimeEnd = dateTimes[1];
            int CompareResultStart = 0;
            int CompareResultEnd = 0;
            foreach (var item in Matches)
            {
                CompareResultStart = item.Date.CompareTo(dateTimeStart);
                CompareResultEnd = item.Date.CompareTo(dateTimeEnd);

                if (CompareResultStart >= 0 && CompareResultEnd <= 0)
                {
                    FoundMatches.Add(item);
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
