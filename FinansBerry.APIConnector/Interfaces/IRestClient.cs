using FinansBerry.Models.Candle;
using FinansBerry.Models.Ticker;
using FinansBerry.Models.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.API.Interfaces
{
    public interface IRestClient
    {
        

        Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount);
        Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0);
        Task<Ticker> GetTickerInfoAsync(string pair);
        
     
    }
}
