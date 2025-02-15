using FinansBerry.API.Interfaces;
using FinansBerry.ConnectorAPIService.Endpoints;
using FinansBerry.Models.Candle;
using FinansBerry.Models.Ticker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Trade = FinansBerry.Models.Trade.Trade;

namespace FinansBerry.API.Implemetations
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;

        public RestClient()
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }

        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            try
            {
                var url = ApiEndpoints.GetTradesUrl(pair, maxCount);
                var response = await _httpClient.GetStringAsync(url);
                var tradeArray = JsonConvert.DeserializeObject<object[][]>(response);

                var trades = tradeArray.Select(trade => new Trade
                {
                    Id = trade[0]?.ToString(),
                    Time = trade[1] != null ? DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(trade[1])) : DateTimeOffset.UtcNow,
                    Amount = Convert.ToDecimal(trade[2]),
                    Price = Convert.ToDecimal(trade[3]),
                    Side = trade[2] != null && Convert.ToDecimal(trade[2]) > 0 ? "buy" : "sell",
                    Pair = pair
                }).ToList();

                return trades;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching trades: {ex.Message}");
                return new List<Trade>();
            }
        }

        public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeframe, long? count = 0)
        {
            try
            {
                var url = ApiEndpoints.GetCandleSeriesUrl(pair, timeframe, count);
                Console.WriteLine($"Sending request to URL: {url}");

                var response = await _httpClient.GetStringAsync(url);
                Console.WriteLine($"Response: {response}");

                var candleArray = JsonConvert.DeserializeObject<object[][]>(response);

                var candles = candleArray.Select(candle => new Candle
                {
                    OpenTime = candle[0] != null ? DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(candle[0])) : DateTimeOffset.UtcNow,
                    OpenPrice = Convert.ToDecimal(candle[1]),
                    HighPrice = Convert.ToDecimal(candle[2]),
                    LowPrice = Convert.ToDecimal(candle[3]),
                    ClosePrice = Convert.ToDecimal(candle[4]),
                    TotalVolume = Convert.ToDecimal(candle[5]),
                    Pair = pair
                }).ToList();

                return candles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching candles: {ex.Message}");
                return new List<Candle>();
            }
        }

        public async Task<Ticker> GetTickerInfoAsync(string pair)
        {
            try
            {
                var url = ApiEndpoints.GetTickerInfoUrl(pair);
                var response = await _httpClient.GetStringAsync(url);
                var tickerArray = JsonConvert.DeserializeObject<object[]>(response);

                return new Ticker
                {
                    Symbol = pair,
                    BidPrice = Convert.ToDecimal(tickerArray[0]),
                    AskPrice = Convert.ToDecimal(tickerArray[1]),
                    LastPrice = Convert.ToDecimal(tickerArray[6]),
                    Volume = Convert.ToDecimal(tickerArray[7]),
                    HighPrice = Convert.ToDecimal(tickerArray[8]),
                    LowPrice = Convert.ToDecimal(tickerArray[9]),
                    DailyChange = Convert.ToDecimal(tickerArray[4]),
                    DailyChangePercentage = Convert.ToDecimal(tickerArray[5])
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ticker info: {ex.Message}");
                return null;
            }
        }
    }
}