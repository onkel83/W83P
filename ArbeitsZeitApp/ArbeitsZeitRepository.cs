using ArbeitsZeitApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using W83P.Logging;

namespace ArbeitsZeitApp
{
    public class ArbeitsZeitRepository
    {
        private List<ArbeitsZeitModel> _daten;
        private const string Dateiname = "AZ.xml";
        ErrorModel _error = new ErrorModel();

        public ArbeitsZeitRepository()
        {
            if (File.Exists(Dateiname))
            {
                var serializer = new XmlSerializer(typeof(List<ArbeitsZeitModel>));
                using var stream = File.OpenRead(Dateiname);
                _daten = (List<ArbeitsZeitModel>)serializer.Deserialize(stream);
            }
            else
            {
                _daten = new List<ArbeitsZeitModel>();
            }
        }

        public void Speichern(ArbeitsZeitModel model)
        {
            _daten.Add(model);
            Persistieren();
        }

        public void Aendern(ArbeitsZeitModel model)
        {
            var vorhandenesModel = _daten.FirstOrDefault(m => m.ID == model.ID);
            if (vorhandenesModel != null)
            {
                _daten.Remove(vorhandenesModel);
                _daten.Add(model);
                Persistieren();
            }
            else
            {
                CreateErrorLog("Aendern :: Model nicht gefunden");
                throw new Exception("Model nicht gefunden");
            }
        }

        public void Loeschen(int id)
        {
            var model = _daten.FirstOrDefault(m => m.ID == id);
            if (model != null)
            {
                _daten.Remove(model);
                Persistieren();
            }
            else
            {
                CreateErrorLog("Loeschen :: Model nicht gefunden");
                throw new Exception("Model nicht gefunden");
            }
        }

        public ArbeitsZeitModel Abfragen(int id)
        {
            return _daten.FirstOrDefault(m => m.ID == id);
        }

        public List<ArbeitsZeitModel> AlleAbfragen()
        {
            return _daten;
        }

        private void Persistieren()
        {
            var serializer = new XmlSerializer(typeof(List<ArbeitsZeitModel>));
            using var stream = File.Create(Dateiname);
            serializer.Serialize(stream, _daten);
        }

        private void CreateErrorLog(string _errMsg)
        {
            _error.Level = LogLevel.Error;
            _error.Message = "Repository :: " +_errMsg;
            ErrorLogger.Initialize("error.xml");
            ErrorLogger.Log(_error);
        }
    }
}
