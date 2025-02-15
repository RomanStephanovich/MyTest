using FinansBerry.API.Interfaces;
using FinansBerry.Models.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinansBerry.Services.Implemetations
{
    public class TradeService
    {
        private readonly IApiConnector _apiConnector;
        public event Action<Trade> OnNewTradeReceived;

        public TradeService(IApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
            _apiConnector.NewBuyTrade += trade => OnNewTradeReceived?.Invoke(trade);
            _apiConnector.NewSellTrade += trade => OnNewTradeReceived?.Invoke(trade);
        }

        public async Task<IEnumerable<Trade>> GetLatestTradesAsync(string pair, int maxCount)
        {
            try
            {
                return await _apiConnector.GetNewTradesAsync(pair, maxCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching latest trades: {ex.Message}");
                return new List<Trade>();
            }
        }

        public async Task SubscribeToTradesAsync(string pair)
        {
            try
            {
                await _apiConnector.SubscribeTradesAsync(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to trades: {ex.Message}");
            }
        }

        public async Task UnsubscribeFromTradesAsync(string pair)
        {
            try
            {
                await _apiConnector.UnsubscribeTradesAsync(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unsubscribing from trades: {ex.Message}");
            }
        }
    }
}