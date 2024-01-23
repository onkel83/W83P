using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W83P.Modelle;
using W83P.Logging;
using System.Runtime.Serialization.Formatters.Binary;

namespace W83P.Basic
{
    public static class Konstanten
    {
        public static Dictionary<string, string> Urls = new Dictionary<string, string> {
            { "MarketGroup", "https://evetycoon.com/api/v1/market/groups"},
            { "Regionen", "https://evetycoon.com/api/v1/market/regions"},
            { "MarktPreise", "https://esi.evetech.net/latest/markets/prices/?datasource=tranquility"},
            { "Systems", "https://esi.evetech.net/latest/universe/systems/?datasource=tranquility" },
            { "Status", "https://esi.evetech.net/latest/status/?datasource=tranquility"}
        };

        public static Dictionary<string, string> FilePaths = new Dictionary<string, string> {
            { "AppPath", AppDomain.CurrentDomain.BaseDirectory },
            { "Error", "Error.xml"},
            { "MGXml", "MG.xml"},
            { "RXml", "R.xml" },
            { "MPXml", "MP.xml" },
            { "JXml", "J.xml" },
            { "SysBin", "Sys.bin" },
            { "Jumps", "D://jumps.txt"}
        };

        public static string GetValueByKey(string key)
        {
            if (Urls.ContainsKey(key))
            {
                return Urls[key];
            }else if (FilePaths.ContainsKey(key))
            {
                return FilePaths[key];
            }
            ErrorHelper.WriteError(Logging.LogLevel.Info, "Folgender Schlüßel wurde nicht gefunden : " + key);
            return "";
        }
    }
}
