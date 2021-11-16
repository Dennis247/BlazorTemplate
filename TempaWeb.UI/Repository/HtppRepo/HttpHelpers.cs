using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TempaWeb.UI.Helpers
{
    public class ApiHttpResponse
    {
        public string data { get; set; }
        public HttpStatusCode code { get; set; }
    }
    

    public interface IHttpHelper
    {
        Task<ApiHttpResponse> Post(string url, dynamic payload, Dictionary<string, string> headers = null);
        Task<ApiHttpResponse> Get(string url, Dictionary<string, string> headers = null);
        void setBearerToken(string token);
    }

    public class HttpHelper: IHttpHelper
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;


        public HttpHelper(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }


        public void setBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }


        public async Task<ApiHttpResponse> Post(string url, dynamic payload, Dictionary<string, string> headers = null)
        {
            ApiHttpResponse responseM = new ApiHttpResponse { data = "", code = HttpStatusCode.InternalServerError };
            var content = JsonSerializer.Serialize(payload);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            var result = await _client.PostAsync(url, bodyContent);
            var resultContent = await result.Content.ReadAsStringAsync();
            return new ApiHttpResponse { code = result.StatusCode, data = resultContent };
        }


        public async Task<ApiHttpResponse> Get(string url, Dictionary<string, string> headers = null)
        {
            ApiHttpResponse responseM = new ApiHttpResponse { data = "", code = HttpStatusCode.InternalServerError };
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            var result = await _client.GetAsync(url);
            var resultContent = await result.Content.ReadAsStringAsync();
            return new ApiHttpResponse { code = result.StatusCode, data = resultContent };
        }
    }


    
}
