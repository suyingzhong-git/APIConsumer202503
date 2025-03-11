using System.Net.Http;
using APIConsumer.BL.Interfaces;
using APIConsumer.Models;

namespace APIConsumer.BL.services
{
    public class HttpClientExternalAPIRainfallService: IHttpClientExternalAPIRainfallService
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HttpClientExternalAPIRainfallService(IConfiguration config, HttpClient httpClient)
        {
            _configuration = config;
            _httpClient = httpClient;
        }
        //create and manage HttpClient connection in pool using HttpClientFactory
        public async Task<HttpResponseMessage> GetRainfallMessageAsync()
        {
            string baseUriInConfig = _configuration.GetSection("WebAPI").GetSection("RainfallBaseUri").Value;
            _httpClient.BaseAddress = new Uri(baseUriInConfig);
            _httpClient.DefaultRequestHeaders.Clear();
            Task<HttpResponseMessage> clientTask = _httpClient.GetAsync("rainfallapi");
            clientTask.Wait();
            HttpResponseMessage result = clientTask.Result;
            return result;
        }
    }
}
