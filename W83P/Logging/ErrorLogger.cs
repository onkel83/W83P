
/*  Aufruf Der XML Logger Klasse !
 *  // Initialisieren Sie den ErrorLogger mit dem Pfad zur XML-Datei
 *  ErrorLogger.Initialize("PfadZurXmlDatei.xml");
 *  
 *  // Erstellen Sie einen neuen LogEntry
 *  LogEntry logEntry = new LogEntry
 *  {
 *      Level = LogLevel.Error,
 *      Message = "Dies ist eine Fehlermeldung"
 *  };
 *
 *  // Fügen Sie den LogEntry hinzu
 *  ErrorLogger.Log(logEntry);
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using W83P.Basic;

namespace W83P.Logging
{
    public class ErrorLogger
    {
        private static ErrorLogger instance = new ErrorLogger();
        private readonly string filePath;

        private ErrorLogger()
        {
            filePath = Konstanten.GetValueByKey("AppPath") + Konstanten.GetValueByKey("Error");
        }

        public static void Initialize()
        {
            instance = new ErrorLogger();
        }

        public static void Log(ErrorModel logEntry)
        {
            if (instance == null)
            {
                throw new InvalidOperationException("ErrorLogger not initialized. Call Initialize() first.");
            }

            instance.Log(logEntry.Level, logEntry.Message);
        }

        private void Log(LogLevel level, string message)
        {
            var logEntry = new ErrorModel
            {
                Level = level,
                Message = message,
                TimeStamp = DateTime.Now
            };

            var doc = new XDocument();
            if (File.Exists(filePath))
            {
                doc = XDocument.Load(filePath);
            }
            else
            {
                doc.Add(new XElement("LogEntries"));
            }
            if(doc.Root != null){
                doc.Root.Add(
                    new XElement("LogEntry",
                        new XElement("Level", logEntry.Level.ToString()),
                        new XElement("Message", logEntry.Message),
                        new XElement("TimeStamp", logEntry.TimeStamp.ToString("o"))
                    )
                );
            }

            doc.Save(filePath);
        }
    }
}
