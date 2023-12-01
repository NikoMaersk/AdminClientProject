using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using AdminClient.Views;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    internal class AdvStatsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand PopularityCommand { get; set; }
        public ICommand OccerrenceCommand { get; set; }
        public ICommand SelectNameCommand { get; set; }
        public ICommand togglePieChartCommand { get; set; }
        private StatTypeEnum StatTypeEnum { get; set; }
        private Visibility _VisibilityChart;
        public List<Name> Names { get; set; }
        public ObservableCollection<Name> SelectedNames { get; set; }
        public SeriesCollection PieSeries1 { get; set; }
        
        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            PieSeries1 = new SeriesCollection();
            Names = new List<Name>();
            SelectedNames = new ObservableCollection<Name>();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(() => OccerrencePie());
            togglePieChartCommand = new DelegateCommand(() => ToggleVisibility());

            Names.Add(new Name { Id = 1, name = "test", Popularity = 3, Occerrence = 111, Gender = Gender.boy });
            Names.Add(new Name { Id = 1, name = "test2", Popularity = 3, Occerrence = 111, Gender = Gender.girl });
            Names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.boy });
            Names.Add(new Name { Id = 1, name = "test4", Popularity = 3, Occerrence = 111, Gender = Gender.girl });

            //SelectedNames.Add(new Name { Id = 1, name = "test5", Popularity = 3, Occerrence = 111 });

        }
        public void PopularityPie()
        {
            PieSeries1.Clear();
            SelectedNames.Clear();
            StatTypeEnum = StatTypeEnum.Popularity;
        }
        public void OccerrencePie()
        {
            PieSeries1.Clear();
            SelectedNames.Clear();
            StatTypeEnum = StatTypeEnum.Occerrence;
        }

        public Name SelectedName
        {
            get
            {
                return new Name(); // very bad
            }
            set
            {
                if (SelectedNames.Contains(value))
                {
                    int index = SelectedNames.IndexOf(value);
                    SelectedNames.RemoveAt(0);
                    PieSeries1.RemoveAt(index);
                }
                else
                {
                    SeriesBuilder seriesBuilder = new SeriesBuilder();
                    SelectedNames.Add(value);
                    switch (StatTypeEnum)
                    {
                        case StatTypeEnum.Popularity:
                            PieSeries1.Add(seriesBuilder.PieSeriesPopularity(value, StatTypeEnum));
                            break;
                        case StatTypeEnum.Occerrence:
                            PieSeries1.Add(seriesBuilder.PieSeriesOccerrence(value, StatTypeEnum));
                            break;
                        default:
                            break;
                    }

                }
                OnPropertyChanged();
            }

        }

        public Name UnSelectName
        {
            get { return new Name(); }
            set
            {
                if (SelectedNames.Contains(value))
                {
                    int index = SelectedNames.IndexOf(value);
                    PieSeries1.RemoveAt(index);
                    SelectedNames.Remove(value);
                }


                OnPropertyChanged();
            }



        }

        public Visibility VisibilityChart
        {
            get { return _VisibilityChart; }
            set 
            {
                if (_VisibilityChart == Visibility.Visible)
                {
                    _VisibilityChart = Visibility.Hidden;
                    Trace.WriteLine(_VisibilityChart.ToString());
                }
                else
                {
                    _VisibilityChart = Visibility.Visible;
                    Trace.WriteLine(_VisibilityChart.ToString());
                }
                OnPropertyChanged();

            }
        
        }
        public void ToggleVisibility()
        {
            if ( _VisibilityChart == Visibility.Visible)
            {
                _VisibilityChart = Visibility.Hidden;
                Trace.WriteLine(_VisibilityChart.ToString());
            }
            else
            {
                _VisibilityChart = Visibility.Visible;
                Trace.WriteLine(_VisibilityChart.ToString());
            }

            OnPropertyChanged("VisibilityChart");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
