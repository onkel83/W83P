using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W83P.EveOnline.Model
{
    [Serializable]
    public class MServerStatus
    {
        public int Players { get; set; }
        public string Server_Version { get; set; }
        public DateTime Start_Time { get; set; }

        public MServerStatus(){
            Server_Version = "";
        }
    }
}