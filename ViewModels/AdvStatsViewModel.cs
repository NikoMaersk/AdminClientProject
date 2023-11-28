using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using AdminClient.Views;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
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
        private ChartTypeEnum ChartTypeEnum { get; set; }
        private Visibility _VisibilityChart;

        private bool PieEnable { get; set; }
        public bool ColumnEnable { get; set; }
        public List<Name> Names { get; set; }
        public ObservableCollection<Name> SelectedNames { get; set; }
        //public SeriesCollection ColumnSeries { get; set; } // should not be needed
        public SeriesCollection SeriesCollect { get; set; }
        
        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            ColumnEnable = false;
            ChartTypeEnum = ChartTypeEnum.PieChart;
            SeriesCollect = new SeriesCollection();
            Names = new List<Name>();
            SelectedNames = new ObservableCollection<Name>();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(() => OccerrencePie());
            togglePieChartCommand = new DelegateCommand(() => ToggleVisibility());

            Names.Add(new Name { Id = 1, name = "test", Popularity = 3, Occerrence = 111, Gender = Gender.male });
            Names.Add(new Name { Id = 1, name = "test2", Popularity = 3, Occerrence = 111, Gender = Gender.female });
            Names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.male });
            Names.Add(new Name { Id = 1, name = "test4", Popularity = 3, Occerrence = 111, Gender = Gender.female });

        }
        #region StateChangers
        // SeriesCollect & SelectedNames should always be cleared between major state changes
        public void PopularityPie()
        {
            SeriesCollect.Clear();
            SelectedNames.Clear();
            StatTypeEnum = StatTypeEnum.Popularity;
        }
        public void OccerrencePie()
        {
            SeriesCollect.Clear();
            SelectedNames.Clear();
            StatTypeEnum = StatTypeEnum.Occerrence;
        }
        #region VisibilityController
        public Visibility VisibilityChart
        {
            get => _VisibilityChart;
            set
            {
                _VisibilityChart = value;
                OnPropertyChanged("VisibilityChart");

            }

        }

        public void ToggleVisibility()
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

            OnPropertyChanged("VisibilityChart");
        }
        #endregion

        #endregion
        public Name SelectedName
        {
            get
            {
                return new Name();
            }
            set
            {
                if (SelectedNames.Contains(value)) // Allows users to removed items by reselecting a selected item
                {
                    int index = SelectedNames.IndexOf(value);
                    SelectedNames.RemoveAt(0);
                    SeriesCollect.RemoveAt(index);
                }
                else
                { // Converts the Name object into a usable series, the significant value is StatTypeEnum, ChartTypeEnum determins the underlying series object type, them should not be mixed.
                    SeriesBuilder seriesBuilder = new SeriesBuilder();
                    SelectedNames.Add(value);
                    SeriesCollect.Add(seriesBuilder.MakeSeries(value, StatTypeEnum, ChartTypeEnum));
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
                    SeriesCollect.RemoveAt(index);
                    SelectedNames.Remove(value);
                }


                OnPropertyChanged();
            }



        }
        
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
