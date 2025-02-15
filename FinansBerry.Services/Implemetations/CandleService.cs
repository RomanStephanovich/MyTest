using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinansBerry.API.Interfaces;
using FinansBerry.Models.Candle;
using FinansBerry.Services.Interfaces;

namespace FinansBerry.Services.Implemetations
{

     public class CandleService : ICandleService
    {
        private readonly IApiConnector _apiConnector;

        public event Action<Candle> OnNewCandle;

        public CandleService(IApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
            _apiConnector.OnNewCandle += HandleNewCandle;
        }

        public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0)
        {
            try
            {
                return await _apiConnector.GetCandleSeriesAsync(pair, timeframe, count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching candle series: {ex.Message}");
                return new List<Candle>();
            }
        }

        public async Task SubscribeToCandlesAsync(string pair, int period)
        {
            try
            {
                await _apiConnector.SubscribeCandlesAsync(pair, period);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to candles: {ex.Message}");
            }
        }

        public async Task UnsubscribeFromCandlesAsync(string pair)
        {
            try
            {
                await _apiConnector.UnsubscribeCandlesAsync(pair);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unsubscribing from candles: {ex.Message}");
            }
        }

        private void HandleNewCandle(Candle candle)
        {
            OnNewCandle?.Invoke(candle);
        }
    }
}