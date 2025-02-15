using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.ConnectorAPIService.Endpoints
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api-pub.bitfinex.com/v2/") };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "FinansBerryApp/1.0");
            return httpClient;
        }
    }
}
