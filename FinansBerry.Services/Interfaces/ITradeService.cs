using FinansBerry.Models.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Services.Interfaces
{
    public interface ITradeService
    {
        Task<IEnumerable<Trade>> GetLatestTradesAsync(string pair, int maxCount);
        Task SubscribeToTradesAsync(string pair, int period);
        Task UnsubscribeFromTradesAsync(string pair);
    }
}
