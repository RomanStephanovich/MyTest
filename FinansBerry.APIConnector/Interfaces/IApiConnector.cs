using CryptoExchange.Net.CommonObjects;
using FinansBerry.Models.Candle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticker = FinansBerry.Models.Ticker.Ticker;
using Trade = FinansBerry.Models.Trade.Trade;


namespace FinansBerry.API.Interfaces
{
    public interface IApiConnector
    {
        Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount);
        Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0);
        Task<Ticker> GetTickerInfoAsync(string pair);

        event Action<Trade> NewBuyTrade;
        event Action<Trade> NewSellTrade;
        
        event Action<Candle> OnNewCandle;

        Task ConnectAsync();
        Task SubscribeTradesAsync(string pair);
        Task SubscribeCandlesAsync(string pair, int periodInSec);
        Task UnsubscribeTradesAsync(string pair);
        Task UnsubscribeCandlesAsync(string pair);
    }
}
