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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    internal class AdvStatsViewModel : ViewModelBase
    {
        public ICommand PopularityCommand { get; set; }
        public ICommand OccerrenceCommand { get; set; }
        public SeriesCollection PieSeries1 { get; set; }
        private List<Name> SelectedNames = new List<Name> { new Name { Id =1, name="test", Popularity = 3, Occerrence =111} };

        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel()
        {
            PieSeries1 = new SeriesCollection();
            PopularityCommand = new DelegateCommand(() => PopularityPie());
            OccerrenceCommand = new DelegateCommand(()=> OccerrencePie());
        }
        public void PopularityPie()
        {
            SeriesBuilder buider = new SeriesBuilder();
            foreach (var item in buider.PieSeriesPopularity(SelectedNames))
            {
                PieSeries1.Add(item);
            }
        }
        public void OccerrencePie()
        {
            SeriesBuilder buider = new SeriesBuilder();
            foreach (var item in buider.PieSeriesOccerrence(SelectedNames))
            {
                PieSeries1.Add(item);
            }
        }

    }
    
}
