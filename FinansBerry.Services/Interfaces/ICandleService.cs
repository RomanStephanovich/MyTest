using FinansBerry.Models.Candle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHQ;

namespace FinansBerry.Services.Interfaces
{
    public interface ICandleService
    {
        Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0);
        Task SubscribeToCandlesAsync(string pair, int period);
        Task UnsubscribeFromCandlesAsync(string pair);
    }
}
