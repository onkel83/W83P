using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W83P.Basic;


namespace W83P.Basic
{
    public abstract class SerialisationHelper<T>
    {
        private static readonly HttpClient client = new HttpClient();
        private T? _Value;
        private List<T>? _Values = null;

        public T Value {
        #pragma warning disable CS8603 // Possible null reference return.
            get => (_Value != null)?_Value: default;
        #pragma warning restore CS8603 // Possible null reference return.
            set => _Value = value;
        }

        public List<T> Values {
            get => (_Values != null)?_Values: new List<T>();
        }

        public async Task<JToken> GetJsonFromUrlAsync(string url)
        {
            Uri uriResult;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (!isValidUrl) {
                Konstanten.WriteError(Logging.LogLevel.Error, $"Die angegebene URL : {url} ist nicht gültig!");
                throw new ArgumentException("Invalid URL");
            }

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JToken json;
            try {
                json = JToken.Parse(responseBody);
            } catch (JsonReaderException) {
            Konstanten.WriteError(Logging.LogLevel.Error, $"Die angegebene URL : {url} enthält keinen gültigen Json Code!");
            throw new ArgumentException("Invalid JSON");
            }

            return json;
        }

        public bool GetJsonObject(JToken json){
            if (json is JObject jsonObject){
                IterateThroughJToken(json);
                return true;
            }
            return false;
        }

        public bool GetJsonArray(JToken json){
            if (json is JArray jsonArray) {
                foreach (JObject jsonObject in jsonArray) {
                    IterateThroughJToken(json);
                }
                return true;
            }
            return false;
        }

        public void IterateThroughJToken(JToken jToken) {
            if (jToken is JObject jObject) {
                foreach (var property in jObject.Properties()) {
                    string propertyName = property.Name;
                    JToken propertyValue = property.Value;
                    // Wenn der Wert ein weiteres JObject oder JArray ist, rufen Sie die Funktion rekursiv auf
                    IterateThroughJToken(propertyValue);
                    InsertIntoClass(propertyName, propertyValue.ToString());
                }
            } else if (jToken is JArray jArray) {
                foreach (JToken nestedToken in jArray) {
                    IterateThroughJToken(nestedToken);
                }
            }
        }

        public abstract void InsertIntoClass(string propertyName,string  propertyValue);
    }
}