
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinansBerry.API.Interfaces;
using FinansBerry.Models.Candle;
using FinansBerry.Models.Ticker;
using FinansBerry.Models.Trade;

namespace FinansBerry.Services.Implemetations
{
    public class ApiConnector : IApiConnector
    {
        private readonly IRestClient _restClient;
        private readonly IWebSocketConnector _webSocketClient;

        public event Action<Trade> NewBuyTrade;
        public event Action<Trade> NewSellTrade;
        public event Action<Candle> OnNewCandle;

        public ApiConnector(IRestClient restClient, IWebSocketConnector webSocketClient)
        {
            _restClient = restClient;
            _webSocketClient = webSocketClient;
            _webSocketClient.OnNewTrade += trade =>
            {
                if (trade.Amount > 0)
                    NewBuyTrade?.Invoke(trade);
                else
                    NewSellTrade?.Invoke(trade);
            };
            _webSocketClient.OnNewCandle += candle => OnNewCandle?.Invoke(candle);
        }

        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            try
            {
                return await _restClient.GetNewTradesAsync(pair, maxCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching new trades: {ex.Message}");
                return new List<Trade>();
            }
        }

        public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0)
        {
            try
            {
                return await _restClient.GetCandleSeriesAsync(pair, timeframe,  count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching candle series: {ex.Message}");
                return new List<Candle>();
            }
        }

        public async Task<Ticker> GetTickerInfoAsync(string pair)
        {
            try
            {
                return await _restClient.GetTickerInfoAsync(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ticker info: {ex.Message}");
                return null;
            }
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _webSocketClient.ConnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to WebSocket: {ex.Message}");
            }
        }

        public async Task SubscribeTradesAsync(string pair)
        {
            try
            {
                await _webSocketClient.SubscribeTrades(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to trades: {ex.Message}");
            }
        }

        public async Task SubscribeCandlesAsync(string pair, int periodInSec)
        {
            try
            {
                await _webSocketClient.SubscribeCandles(pair, periodInSec);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to candles: {ex.Message}");
            }
        }

        public async Task UnsubscribeTradesAsync(string pair)
        {
            try
            {
                await _webSocketClient.UnsubscribeTrades(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unsubscribing from trades: {ex.Message}");
            }
        }

        public async Task UnsubscribeCandlesAsync(string pair)
        {
            try
            {
                await _webSocketClient.UnsubscribeCandles(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unsubscribing from candles: {ex.Message}");
            }
        }
    }
}
