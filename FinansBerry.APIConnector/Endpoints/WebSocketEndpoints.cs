using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.ConnectorAPIService.Endpoints
{
    public static class WebSocketEndpoints
    {
        private const string BaseUrl = "wss://api-pub.bitfinex.com/ws/2";

        public static Uri GetWebSocketUri()
        {
            return new Uri(BaseUrl);
        }

        public static string CreateSubscribeMessage(string channel, string symbol, string key = null)
        {
            if (channel == "trades")
            {
                return JsonConvert.SerializeObject(new
                {
                    @event = "subscribe",
                    channel,
                    symbol = $"t{symbol}"
                });
            }
            else if (channel == "candles")
            {
                return JsonConvert.SerializeObject(new
                {
                    @event = "subscribe",
                    channel,
                    key
                });
            }
            return null;
        }

        public static string CreateUnsubscribeMessage(string channel, string symbol, string key = null)
        {
            if (channel == "trades")
            {
                return JsonConvert.SerializeObject(new
                {
                    @event = "unsubscribe",
                    channel,
                    symbol = $"t{symbol}"
                });
            }
            else if (channel == "candles")
            {
                return JsonConvert.SerializeObject(new
                {
                    @event = "unsubscribe",
                    channel,
                    key
                });
            }
            return null;
        }
    }
}

