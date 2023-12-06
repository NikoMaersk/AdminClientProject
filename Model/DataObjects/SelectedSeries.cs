using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Model.DataObjects
{

    internal class SelectedSeries
    {
        public string Name { get; set; }
        public int index { get; set; }
    }
    internal class nestedSeries
    { public string SeriresName { get; set; }
        public List<SelectedSeries> SelectedSeries = new List<SelectedSeries>();
    }
}
