using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbeitsZeitApp.Model
{
    public class ArbeitsZeitModel
    {
        public int ID { get; set; }
        public DateTime StartdatumUndZeit { get; set; }
        public DateTime EnddatumUndZeit { get; set; }
        public decimal Pausen { get; set; } = 0.0m;

        public decimal ArbeitsZeit
        {
            get
            {
                TimeSpan arbeitsZeitSpan = EnddatumUndZeit - StartdatumUndZeit;
                decimal arbeitsZeitStunden = (decimal)arbeitsZeitSpan.TotalHours;
                return Math.Round(arbeitsZeitStunden - Pausen, 2);
            }
        }
    }
}