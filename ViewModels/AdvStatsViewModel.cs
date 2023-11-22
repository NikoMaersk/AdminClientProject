using AdminClient.Views;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    internal class AdvStatsViewModel : ViewModelBase
    {
        public string name1 { get; set; }

        private void ExecuteAuthorization()
        {
            throw new NotImplementedException();
        }
        public AdvStatsViewModel() 
        {
            PointLabel = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            name1 = "meme";
            PieSeries pieSeries = new PieSeries();
            PieChart pieChart = new PieChart();
            pieChart.Series.Add (pieSeries);
            


        }
        public Func<ChartPoint, string> PointLabel { get; set; }
    }
}
