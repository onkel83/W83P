using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W83P.EveOnline.Model
{
    [Serializable]
    public class MMarktPrice{
        public decimal AdjustedPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public int TypeId { get; set; }
    }
}