using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RickAndMortyAPI.Utils.Exceptions;

namespace RickAndMortyAPI.Utils
{
    public static class RestClient
    {
        private static HttpClient _client;

        static RestClient()
        {
            _client = new();
        }
        
        public static async Task<T> Get<T>(string url)
        {
            var response = await _client.GetAsync(url);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new FailedHttpRequestException(responseJson, response.StatusCode);
            
            return JsonConvert.DeserializeObject<T>(responseJson);
        }
    }
}