using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace AdminClient.ViewModels
{
    internal class AdvStatsViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public ICommand PopularityCommand { get; set; }
        public ICommand OccerrenceCommand { get; set; }
        public ICommand SelectNameCommand { get; set; }
        public ICommand togglePieChartCommand { get; set; }
        public ICommand AddSeries { get; set; }
        
        private StatTypeEnum StatTypeEnum { get; set; }
        private ChartTypeEnum chartTypeEnum { get; set; }
        private Visibility _VisibiltyPie;
        private Visibility _VisibiltyColumn;
        private Visibility _VisibiltySelected;
        private Visibility _VisibiltySelectedCol;
        public string SeriesNameTextBox { get; set; }
        public int SelectedSeriesIndex { get; set; }
        public bool ColumnEnable { get; set; }
        private List<string> seriesNames = new List<string>();
        public List<Name> Names { get; set; }
        public ObservableCollection<Name> SelectedNames { get; set; }
        private SeriesCollection ColumnSeries = new SeriesCollection(); // should not be needed
        public ObservableCollection<Button> SeriesButtons { get ; set; }
        public SeriesCollection SeriesCollect { get; set; }
        private ObservableCollection<nestedSeries> nestedSeries = new ObservableCollection<nestedSeries>();
        
        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            chartTypeEnum = ChartTypeEnum.PieChart;
            SeriesCollect = new SeriesCollection();
            VisibilityColumn = Visibility.Hidden;
            VisibilityPie = Visibility.Visible;

            Names = new List<Name>();
            SelectedNames = new ObservableCollection<Name>();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(() => OccerrencePie());
            togglePieChartCommand = new DelegateCommand(() => ToggleVisibility());
            AddSeries = new DelegateCommand(() => addSeries());

            Names.Add(new Name { Id = 1, name = "test", Popularity  = 1, Occerrence = 111, Gender = Gender.Male });
            Names.Add(new Name { Id = 1, name = "test2", Popularity = 2, Occerrence = 111, Gender = Gender.Female });
            Names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.Unisex });
            Names.Add(new Name { Id = 1, name = "test4", Popularity = 4, Occerrence = 111, Gender = Gender.Female });
            SeriesButtons = new ObservableCollection<Button>();
            

        }
        private void buttest(object sender, EventArgs e)
        {
            foreach (Button button in SeriesButtons) 
            { 
                button.Background = Brushes.LightGray;
            }
            Button? btn = sender as Button;
            if (btn != null) 
            {
                selectedIndex = seriesNames.IndexOf(btn.Content.ToString());
                Trace.WriteLine(selectedIndex);
                btn.Background = Brushes.Green;
            }
        }
        public void addSeries()
        {
            bool check = true;
            foreach (var item in ColumnSeries)
            {
                if (item.Title == SeriesNameTextBox)
                {
                    check=false;
                }
            }
            if (check)
            {
                
                SeriesFactory.instance.addNewSeries(ref ColumnSeries, SeriesNameTextBox);
                seriesNames.Add(SeriesNameTextBox);
                Button button = new Button();
                button.Click += buttest;
                button.Content = SeriesNameTextBox;
                SeriesButtons.Add(button);
                nestedSeries.Add(SeriesFactory.instance.makeNestedSeries(SeriesNameTextBox));
            }

            

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
        public Visibility VisibilityPie
        {
            get => _VisibiltyPie;
            set
            {
                _VisibiltyPie = value;
                OnPropertyChanged();

            }

        }
        public Visibility VisibilityColumn
        {
            get => _VisibiltyColumn;
            set
            {
                _VisibiltyColumn = value;
                OnPropertyChanged();

            }

        }
        public Visibility VisibiltySelected
        {
            get => _VisibiltySelected;
            set
            {
                _VisibiltySelected = value;
                OnPropertyChanged();

            }

        }
        public Visibility VisibiltySelectedCol
        {
            get => _VisibiltySelected;
            set
            {
                _VisibiltySelectedCol = value;
                OnPropertyChanged();

            }

        }
        public void ToggleVisibility()
        {
            if (_VisibiltyPie == Visibility.Visible)
            {
                _VisibiltyPie = Visibility.Hidden;
                VisibiltySelected = Visibility.Hidden;
                _VisibiltyColumn = Visibility.Visible;
                chartTypeEnum = ChartTypeEnum.ColumnChart;
                SeriesCollect.Clear();
                SelectedNames.Clear();
                Trace.WriteLine(_VisibiltyPie.ToString());
                OnPropertyChanged("VisibilityColumn");
                OnPropertyChanged("VisibilityPie");
            }
            else
            {
                _VisibiltyPie = Visibility.Visible;
                VisibiltySelected = Visibility.Visible;
                _VisibiltyColumn = Visibility.Hidden;
                chartTypeEnum = ChartTypeEnum.PieChart;
                SeriesCollect.Clear();
                SelectedNames.Clear();
                Trace.WriteLine(_VisibiltyPie.ToString());
                OnPropertyChanged("VisibilityColumn");
                OnPropertyChanged("VisibilityPie");
            }

            
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
                
                if (chartTypeEnum == ChartTypeEnum.ColumnChart)
                {
                    SeriesFactory.instance.MakeSeries(value, StatTypeEnum, ref ColumnSeries, selectedIndex);
                    nestedSeries[selectedIndex].SelectedSeries.Add(new SelectedSeries { Name = value.name, index = selectedIndex});
                    
                }

                if (SelectedNames.Contains(value)) // Allows users to removed items by reselecting a selected item
                {
                    int index = SelectedNames.IndexOf(value);
                    SelectedNames.RemoveAt(index);
                    SeriesCollect.RemoveAt(index);
                }
                else
                { // Converts the Name object into a usable series, the significant value is StatTypeEnum, ChartTypeEnum determins the underlying series object type, them should not be mixed.
                    SelectedNames.Add(value);
                    SeriesCollect.Add(SeriesFactory.instance.MakeSeries(value, StatTypeEnum));

                    
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
            get { return ColumnSeries ; }
            set { ColumnSeries = value; }

        }
        public int selectedIndex
        {
            get; set;
        }
        public ObservableCollection<nestedSeries> nestedSeriesview 
        { 
        get { return this.nestedSeries; }
            set {  this.nestedSeries = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
