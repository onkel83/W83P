using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W83P.Modelle;
using W83P.Basic;

namespace W83P.EveOnline.Model
{
    [Serializable]
    public class MServerStatus : Modelle.Basic{
        private string _ServerVersion = "";
        private int _SpielerZahl;
        private string _StartZeit = "";

        public string ServerVersion{
            get => _ServerVersion;
            set => _ServerVersion = value;
        }
        public string SpielerZahl{
            get => _SpielerZahl + "";
            set => _SpielerZahl = Helper.StringToInt(value);
        }
        public string StartZeit{
            get => _StartZeit;
            set => _StartZeit = value;
        }
    }
}