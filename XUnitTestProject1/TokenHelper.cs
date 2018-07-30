using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Talon.Api.Testing
{
    public class TokenHelper
    {

        public static async Task<string> GetToken(int accountId)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                UseDefaultCredentials = true,
            };

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://upadmindev.navitor.com/api/JmeterApi/{accountId}"),
                    Method = HttpMethod.Get
                };
                var resultResponse = await httpClient.SendAsync(request);
                if (resultResponse.IsSuccessStatusCode)
                {
                    var resContent = await resultResponse.Content.ReadAsStringAsync();
                    return resContent.Replace("\"", "");
                }
                return string.Empty;
            }

        }
    }
}
