using FinansBerry.API.Interfaces;
using FinansBerry.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Services.Implemetations
{
    public class PortfolioService : IPortfoilioService
    {
        private readonly IApiConnector _apiConnector;

        public PortfolioService(IApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }

        public async Task<Dictionary<string, decimal>> GetPortfolioBalanceAsync(Dictionary<string, decimal> assets)
        {
            var portfolioBalances = new Dictionary<string, decimal>();
            var tasks = new List<Task>();

            foreach (var asset in assets)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var ticker = await _apiConnector.GetTickerInfoAsync(asset.Key + "USD");
                        if (ticker != null)
                        {
                            portfolioBalances[asset.Key] = asset.Value * ticker.LastPrice;
                        }
                        else
                        {
                            Console.WriteLine($"Ticker info for {asset.Key} is null.");
                            portfolioBalances[asset.Key] = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error fetching ticker info for {asset.Key}: {ex.Message}");
                        portfolioBalances[asset.Key] = 0;
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return portfolioBalances;
        }

        private async Task<decimal> GetPriceInUSDT(string currency)
        {
            try
            {
                var ticker = await _apiConnector.GetTickerInfoAsync($"{currency}USDT");
                return ticker?.LastPrice ?? 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching price in USDT for {currency}: {ex.Message}");
                return 1;
            }
        }
    }
}