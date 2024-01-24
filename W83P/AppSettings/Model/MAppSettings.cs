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

    /*
     * Using of Class :
     * using W83P.AppSettings.Model;
     *
     * MAppSettings.LoadSettingsXml(string Path to Xml File); // To Load from a Xml File
     * MAppSettings.LoadSettingsBin(string Path to bin File); // To Load from a Bin File
     * string setting = MAppSettings.GetSetting(string key);  // To Get a Value from Key
     * MAppSettings.SetSettings(string key, string value);    // To Store a Key:Value Pait
     * MAppSettings.SaveSettingsXml(string Path to Xml File); // Save in to a Xml File
     * MAppSettings.SaveSettingsBin(string Path to bin File); // Save in to a Bin File
     *
     */
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