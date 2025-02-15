
using FinansBerry.API.Interfaces;
using FinansBerry.Models.Ticker;
using FinansBerry.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Services.Implemetations
{
    public class TickerService : ITickerService
    {
        private readonly IApiConnector _apiConnector;

        public TickerService(IApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }

        public async Task<Ticker> GetTickerAsync(string pair)
        {
            try
            {
                return await _apiConnector.GetTickerInfoAsync(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ticker info for {pair}: {ex.Message}");
                return null;
            }
        }
    }
}
