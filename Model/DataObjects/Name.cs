using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Model.DataObjects
{
    internal class Name
    {
        public int Id { get; set; }
        public string name { get; set; }
        public Gender Gender { get; set; }
        public bool IsInternational { get; set; }
        public int Popularity { get; set; }
        public int Occerrence { get; set; }
    }
    enum Gender { male,female,unisex}
}
