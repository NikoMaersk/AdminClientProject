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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    internal class AdvStatsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand PopularityCommand { get; set; }
        public ICommand OccerrenceCommand { get; set; }
        public ICommand SelectNameCommand {  get; set; }
        public List<Name> Names { get; set; }
        public SeriesCollection PieSeries1 { get; set; }
        private List<Name> SelectedNames;

        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            PieSeries1 = new SeriesCollection();
            Names = new List<Name>();
            SelectedNames = new List<Name>();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(()=> OccerrencePie());
            Names.Add(new Name { Id = 1, name = "test", Popularity = 3, Occerrence = 111 } );
            Names.Add(new Name { Id = 1, name = "test2", Popularity = 3, Occerrence = 111 });
            Names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111 });
            Names.Add(new Name { Id = 1, name = "test4", Popularity = 3, Occerrence = 111 });

        }
        public void PopularityPie()
        {
            SeriesBuilder buider = new SeriesBuilder();
            foreach (var item in buider.PieSeriesPopularity(SelectedNames))
            {
                PieSeries1.Add(item);
            }
            OnPropertyChanged();
        }
        public void OccerrencePie()
        {
            SeriesBuilder buider = new SeriesBuilder();
            foreach (var item in buider.PieSeriesOccerrence(SelectedNames))
            {
                PieSeries1.Add(item);
            }
            OnPropertyChanged();
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
                    return;

                SelectedNames.Add(value);
                Trace.WriteLine(SelectedNames.ToString());
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
