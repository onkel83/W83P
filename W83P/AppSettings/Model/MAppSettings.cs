using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace W83P.AppSettings.Model
{
    public class MAppSettings{
        #region Private Members
        private static Dictionary<string, string> _settings;
        #endregion
        #region Constructor 
        static MAppSettings(){
            _settings = new Dictionary<string, string>();
        }
        #endregion
        #region Public Methods
        public static void SaveSettingsBin(string filePath){
            using (FileStream stream = new FileStream(filePath, FileMode.Create)){
                BinaryFormatter serializer = new BinaryFormatter();
                #pragma warning disable SYSLIB0011 // Type or member is obsolete
                serializer.Serialize(stream, _settings);
            }
        }
        public static void LoadSettingsBin(string filePath){
            using (FileStream stream = new FileStream(filePath, FileMode.Open)){
                BinaryFormatter serializer = new BinaryFormatter();
                _settings = (Dictionary<string, string>)serializer.Deserialize(stream);
                #pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
        }
        public static void SaveSettingsXml(string filePath){
            // Serialize the settings to a XML file
            XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, string>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create)){
                serializer.Serialize(stream, _settings);
            }
        }
        public static void LoadSettingsXml(string filePath){
            // Deserialize the settings from the XML file
            XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, string>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open)){
                #pragma warning disable CS8600, CS8601 // Converting null literal or possible null value to non-nullable type.
                _settings = (Dictionary<string, string>)serializer.Deserialize(stream);
                #pragma warning restore CS8600, CS8601 // Converting null literal or possible null value to non-nullable type.
            }
        }
        public static string GetSetting(string key){
            return _settings[key];
        }
        public static void SetSetting(string key, string value){
            _settings[key] = value;
        }
        #endregion
    }
}