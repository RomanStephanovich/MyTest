using FinansBerry.API.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinansBerry.ConnectorAPIService.Endpoints;
using FinansBerry.Models.Candle;
using FinansBerry.Models.Trade;

namespace FinansBerry.API.Implemetations
{
    public class WebSocketConnector : IWebSocketConnector
    {
        private readonly ClientWebSocket _webSocket;
        private readonly Uri _webSocketUri;
        private readonly Dictionary<string, int> _tradeSubscriptions = new();
        private readonly Dictionary<string, int> _candleSubscriptions = new();

        public event Action<Trade> OnNewTrade;
        public event Action<Candle> OnNewCandle;

        public WebSocketConnector()
        {
            _webSocket = new ClientWebSocket();
            _webSocketUri = WebSocketEndpoints.GetWebSocketUri();
        }

        public async Task ConnectAsync()
        {
            if (_webSocket.State != WebSocketState.Open)
            {
                await _webSocket.ConnectAsync(_webSocketUri, CancellationToken.None);
                _ = ReceiveWebSocketMessages();
            }
        }

        public async Task SubscribeTrades(string pair, int maxCount = 100)
        {
            await ConnectAsync();
            var subscribeMessage = WebSocketEndpoints.CreateSubscribeMessage("trades", pair);
            Console.WriteLine($"Sending subscribe message for trades: {subscribeMessage}");
            await _webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscribeMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
            _tradeSubscriptions[pair] = maxCount;
        }

        public async Task UnsubscribeTrades(string pair)
        {
            var unsubscribeMessage = WebSocketEndpoints.CreateUnsubscribeMessage("trades", pair);
            Console.WriteLine($"Sending unsubscribe message for trades: {unsubscribeMessage}");
            await _webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(unsubscribeMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
            _tradeSubscriptions.Remove(pair);
        }

        public async Task SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {
            await ConnectAsync();
            var key = $"trade:{periodInSec}s:t{pair}";
            var subscribeMessage = WebSocketEndpoints.CreateSubscribeMessage("candles", pair, key);
            Console.WriteLine($"Sending subscribe message for candles: {subscribeMessage}");
            await _webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscribeMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
            _candleSubscriptions[pair] = periodInSec;
        }

        public async Task UnsubscribeCandles(string pair)
        {
            var key = $"trade:{_candleSubscriptions[pair]}s:t{pair}";
            var unsubscribeMessage = WebSocketEndpoints.CreateUnsubscribeMessage("candles", pair, key);
            Console.WriteLine($"Sending unsubscribe message for candles: {unsubscribeMessage}");
            await _webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(unsubscribeMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
            _candleSubscriptions.Remove(pair);
        }

        private async Task ReceiveWebSocketMessages()
        {
            var buffer = new byte[4096];
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    ProcessWebSocketMessage(message);
                }
            }
        }

        private void ProcessWebSocketMessage(string message)
        {
            var jsonArray = JsonConvert.DeserializeObject<JArray>(message);
            if (jsonArray != null && jsonArray.Count > 1)
            {
                var channelData = jsonArray[1];
                if (channelData is JArray tradeArray && tradeArray.Count >= 4)
                {
                    var trade = new Trade
                    {
                        Price = tradeArray[3]?.Value<decimal>() ?? 0,
                        Amount = tradeArray[2]?.Value<decimal>() ?? 0,
                        Time = tradeArray[1] != null
                            ? DateTimeOffset.FromUnixTimeMilliseconds(tradeArray[1].Value<long>())
                            : DateTimeOffset.UtcNow
                    };
                    OnNewTrade?.Invoke(trade);
                }
                else if (channelData is JArray candleArray && candleArray.Count >= 6)
                {
                    var candle = new Candle
                    {
                        Pair = jsonArray[0]?.Value<string>(),
                        OpenPrice = candleArray[1]?.Value<decimal>() ?? 0,
                        HighPrice = candleArray[2]?.Value<decimal>() ?? 0,
                        LowPrice = candleArray[3]?.Value<decimal>() ?? 0,
                        ClosePrice = candleArray[4]?.Value<decimal>() ?? 0,
                        TotalVolume = candleArray[5]?.Value<decimal>() ?? 0,
                        OpenTime = candleArray[0] != null
                            ? DateTimeOffset.FromUnixTimeMilliseconds(candleArray[0].Value<long>())
                            : DateTimeOffset.UtcNow
                    };
                    OnNewCandle?.Invoke(candle);
                }
            }
        }
    }
}