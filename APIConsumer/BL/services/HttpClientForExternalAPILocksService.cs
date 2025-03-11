using APIConsumer.BL.Interfaces;
using APIConsumer.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace APIConsumer.BL.services
{
    public class HttpClientForExternalAPILocksService:IHttpClientForExternalAPILocksService
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HttpClientForExternalAPILocksService(IConfiguration config, HttpClient httpClient)
        {
            _configuration = config;
            _httpClient = httpClient;
        }
        /// <summary>
        /// Init: Init HttpClient settings
        /// </summary>
        public void Init()
        {
            string baseUriInConfig = _configuration.GetSection("WebAPI").GetSection("SureLocksBaseUri").Value;
            _httpClient.BaseAddress = new Uri(baseUriInConfig);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "a642ed09-deb5-4b5c-84ec-99e61a39e20c");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
        }
        public async Task<HttpResponseMessage> GetLocksMessagesAsync()           
        {
            // throw new NotImplementedException();

            try
            {
                Init();
                //Create/reuse an HttpClient request that calls external API Get method to query record.
                //The URI of the HttpClient is base URI plus /products path 
                //_httpClient is injected in constructor Dependency Injection
                using (Task<HttpResponseMessage> clientTask = _httpClient.GetAsync("products"))
                {
                    clientTask.Wait();
                    //Get Http Response result
                    HttpResponseMessage result = clientTask.Result;
                    //return Http Response Message result
                    return result;
                }
            }
            catch
            {
                //buble up exception and keep all stacktrace
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetLocksMessagesRangeAsync(string queryString)
        {
            try
            {
                Init();
                //Create/reuse an HttpClient request that calls external API Get method to query record.
                //The URI of the HttpClient is base URI plus /products path and query string 
                //_httpClient is injected in constructor Dependency Injection 
                using (Task<HttpResponseMessage> clientTask = _httpClient.GetAsync($"products?{queryString}"))
                {
                    clientTask.Wait();
                    //Get Http Response result
                    HttpResponseMessage result = clientTask.Result;
                    //return Http Response Message result
                    return result;
                }
            }
            catch
            {
                //buble up exception and keep all stacktrace
                throw;
            }
        }

        public async Task<HttpResponseMessage> CreateLockMessageAsync(Lock model)
        {
            try
            {
                //initial setting for Http Client 
                Init();
                //serialise the object to Json string. The object complying with Lock model 
                var httpContent = JsonConvert.SerializeObject(model);
                //create Json content with encoding format, content-type setting
                var content = new StringContent(httpContent, Encoding.UTF8, "application/json");
                //Create/reuse an HttpClient request that calls external API Post method to create record using Json content.
                //The URI of the HttpClient is base URI plus /products path
                //_httpClient is injected in constructor Dependency Injection          
                using (Task<HttpResponseMessage> clientTask = _httpClient.PostAsync("products", content))
                {
                    clientTask.Wait();
                    //Get Http Response result
                    HttpResponseMessage result = clientTask.Result;
                    //return Http Response Message result
                    return result;
                }
            }
            catch {
                //buble up exception and keep all stacktrace
                throw; 
            }
        }

        public async Task<HttpResponseMessage> UpdateLockMessageAsync(Lock model)
        {
            try
            {
                //initial setting for Http Client 
                Init();
                //serialise the object to Json string. The object complying with Lock model 
                var httpContent = JsonConvert.SerializeObject(model);
                //create Json content with encoding format, content-type setting
                var content = new StringContent(httpContent, Encoding.UTF8, "application/json");
                //Create/reuse an HttpClient request that calls external API Patch method to update record using Json content.
                //The URI of the HttpClient is base URI plus /products path
                //_httpClient is injected in constructor Dependency Injection
                using (Task<HttpResponseMessage> clientTask = _httpClient.PatchAsync("products", content))
                {
                    clientTask.Wait();
                    //Get Http Response result
                    HttpResponseMessage result = clientTask.Result;
                    //return Http Response Message result
                    return result;
                }
            }
            catch
            {
                //buble up exception and keep all stacktrace
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteLockMessageAsync(int id)
        {
            try
            {
                //initial setting for Http Client 
                Init();
                //Create/reuse an HttpClient request that calls external API Delete method to delete record using Json content.
                //The URI of the HttpClient is base URI plus /products path
                //_httpClient is injected in constructor Dependency Injection
                using (Task<HttpResponseMessage> clientTask = _httpClient.DeleteAsync($"products/id/{id}"))
                {
                    clientTask.Wait();
                    //Get Http Response result
                    HttpResponseMessage result = clientTask.Result;
                    //return Http Response Message result
                    return result;
                }
            }
            catch
            {
                //buble up exception and keep all stacktrace
                throw; 
            }
        }
    }
}
