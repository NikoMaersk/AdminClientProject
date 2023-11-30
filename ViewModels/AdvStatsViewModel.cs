using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public string SeriesNameTextBox { get; set; }
        public int SelectedSeriesIndex { get; set; }
        private bool PieEnable { get; set; }
        public bool ColumnEnable { get; set; }
        public List<Name> Names { get; set; }
        public ObservableCollection<Name> SelectedNames { get; set; }
        private SeriesCollection ColumnSeries = new SeriesCollection(); // should not be needed
        public ObservableCollection<Button> SeriesButtons { get; set; }
        public SeriesCollection SeriesCollect { get; set; }
        
        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            ChartTypeEnum = ChartTypeEnum.ColumnChart;
            SeriesCollect = new SeriesCollection();
            ColumnEnable= true;
            ColumnSeries.Add(new ColumnSeries { Title = "Series 1", Values = new ChartValues<int> { } });

            Names = new List<Name>();
            SelectedNames = new ObservableCollection<Name>();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(() => OccerrencePie());
            togglePieChartCommand = new DelegateCommand(() => ToggleVisibility());

            Names.Add(new Name { Id = 1, name = "test", Popularity  = 3, Occerrence = 111, Gender = Gender.male });
            Names.Add(new Name { Id = 1, name = "test2", Popularity = 3, Occerrence = 111, Gender = Gender.female });
            Names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.male });
            Names.Add(new Name { Id = 1, name = "test4", Popularity = 3, Occerrence = 111, Gender = Gender.female });
            SeriesButtons = new ObservableCollection<Button>();

        }
        #region StateChangers
        // SeriesCollect & SelectedNames should always be cleared between major state changes
        #region Buttons
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
                Button button = new Button();
                button.Height = 100;
                button.Width = 100;
                SeriesButtons.Add(button);

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

        void buttonAddSeries(object sender, RoutedEventArgs e)
        {

            throw new NotImplementedException();
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
                    SeriesFactory seriesBuilder = new SeriesFactory();
                    SelectedNames.Add(value);
                    object holder = seriesBuilder.MakeSeries(value, StatTypeEnum, ChartTypeEnum, ref ColumnSeries);
                    if (holder != null) { SeriesCollect.Add((ISeriesView)holder); }
                    
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
        #endregion
        public SeriesCollection ColumnSeriesProp
        {
            get { return ColumnSeries; }
            set { ColumnSeries = value; }

        }
        public int selectedIndex
        {
            get; set;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
