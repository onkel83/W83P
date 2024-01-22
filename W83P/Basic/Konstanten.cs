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
            { "Jumps", "D://jumps.txt"},
            { "AppPath", AppDomain.CurrentDomain.BaseDirectory },
            { "Error", "Error.xml"},
            { "MGXml", "MG.xml"},
            { "RXml", "R.xml" },
            { "MPXml", "MP.xml" },
            { "JXml", "J.xml" },
            { "Sys", "Sys.bin" }
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
            WriteError(Logging.LogLevel.Info, "Folgender Schlüßel wurde nicht gefunden : " + key);
            return "";
        }

        public static void WriteError(LogLevel level, string message)
        {
            ErrorLogger.Initialize();
            ErrorModel logEntry = new ErrorModel
            {
                Level = level,
                Message = message
            };
            ErrorLogger.Log(logEntry);
        }

        public static int StringToInt(string value, int standart = 0)
        {
            try
            {
                return Convert.ToInt32(value);
            }catch(Exception ex)
            {
                WriteError(LogLevel.Error, ex.Message);
            }
            return standart;
        }
        public static double StringToDouble(string value, double standart = 0.0f)
        {
            try 
            { 
                return Convert.ToDouble(value);
            }catch(Exception ex)
            {
                WriteError(LogLevel.Error, ex.Message);
            }
            return standart;
        }

        public static DateTime StringToDateTime(string value) {
            try{
                return DateTime.ParseExact(value, "HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture);
            }catch(Exception ex){
                WriteError(LogLevel.Error, ex.Message);
            }
            return DateTime.Now;
        }

        public static string DateTimeToString(DateTime dateTime) {
            try {
                return dateTime.ToString("HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture);
            }catch(Exception ex){
                WriteError(LogLevel.Error, ex.Message);
            }
            return "00:00 01.01.2024";
        }

        public static void SaveObject<T>(T obj, string filePath){
            using (Stream stream = File.Open(filePath, FileMode.Create)){
                BinaryFormatter formatter = new BinaryFormatter();
                #pragma warning disable SYSLIB0011, CS8604 
                formatter.Serialize(stream, obj);
                #pragma warning restore SYSLIB0011, CS8604 // Type or member is obsolete
            }
        }

        public static T LoadObject<T>(string filePath){
            using (Stream stream = File.Open(filePath, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                #pragma warning disable SYSLIB0011 // Type or member is obsolete
                return (T)formatter.Deserialize(stream);
                #pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
        }
    }
}
