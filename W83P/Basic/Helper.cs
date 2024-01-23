using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using W83P.Logging;

namespace W83P.Basic
{
    public static class Helper{
        public static int StringToInt(string value, int standart = 0)
        {
            try
            {
                return Convert.ToInt32(value);
            }catch(Exception ex)
            {
                ErrorHelper.WriteError(LogLevel.Error, ex.Message);
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
                ErrorHelper.WriteError(LogLevel.Error, ex.Message);
            }
            return standart;
        }

        public static DateTime StringToDateTime(string value) {
            try{
                return DateTime.ParseExact(value, "HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture);
            }catch(Exception ex){
                ErrorHelper.WriteError(LogLevel.Error, ex.Message);
            }
            return DateTime.Now;
        }

        public static string DateTimeToString(DateTime dateTime) {
            try {
                return dateTime.ToString("HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture);
            }catch(Exception ex){
                ErrorHelper.WriteError(LogLevel.Error, ex.Message);
            }
            return "00:00 01.01.2024";
        }    
    }
    public static class ErrorHelper{
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
    }

    public static class FileHelper{
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

    public static class JsonHelper{
        public static bool IfJsonObject(JToken json){
            if (json is JObject jsonObject){
                return true;
            }
            return false;
        }

        public static bool IfJsonArray(JToken json){
            if (json is JArray jsonArray) {
                return true;
            }
            return false;
        }
    }
}