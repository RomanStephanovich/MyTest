using FinansBerry.Models.Candle;
using FinansBerry.Models.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.API.Interfaces
{
   public interface IWebSocketConnector
    {
        event Action<Trade> OnNewTrade;
        event Action<Candle> OnNewCandle;

        Task ConnectAsync();
        Task SubscribeTrades(string pair, int maxCount = 100);
        Task UnsubscribeTrades(string pair);
        Task SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0);
        Task UnsubscribeCandles(string pair);
    }
}
