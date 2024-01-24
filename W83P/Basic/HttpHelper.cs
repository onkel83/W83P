using System.Text;
using Newtonsoft.Json;

namespace W83P.Basic
{
    public class HttpHelper{
        private readonly HttpClient _httpClient;
        public HttpHelper(){
            _httpClient = new HttpClient();
        }
        public async Task<string> SendGetRequestAsync(string url){
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            #pragma warning disable CS8603 // Possible null reference return.
            return responseContent;
        }
        public async Task<string> SendPostRequestAsync(string url, object data){
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(responseContent);
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }
}