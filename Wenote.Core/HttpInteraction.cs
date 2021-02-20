using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Wenote.Core {
    static class HttpInteraction {

        #region Asychronized Methods

        public static Task<HttpResponseMessage> GetAsync(string url, string jwt = "") {
            var client = new HttpClient();
            if(jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            return client.GetAsync(url);
        }
        public static Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> data, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            return client.PostAsJsonAsync(url, data);
        }
        public static Task<HttpResponseMessage> PutAsync(string url, Dictionary<string, string> data, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            return client.PutAsJsonAsync(url, data);
        }
        public static Task<HttpResponseMessage> DeleteAsync(string url, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            return client.DeleteAsync(url);
        }


        
        public static Task<HttpResponseMessage> PostAsync(string url, string jsonData, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var request = new HttpRequestMessage(HttpMethod.Post, url) {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            return client.SendAsync(request);
        }
        public static Task<HttpResponseMessage> PutAsync(string url, string jsonData, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var request = new HttpRequestMessage(HttpMethod.Put, url) {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            return client.SendAsync(request);
        }
        
        #endregion

        #region Sychronized Methods

        public static HttpResponseMessage Get(string url, string jwt = "") {
            var client = new HttpClient();
            if (jwt != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            return client.Send(request);
        }

        #endregion
    }
}
