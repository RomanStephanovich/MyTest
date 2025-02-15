using FinansBerry.ConnectorAPIService.PortfolioConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.API.Implemetations
{
    public class PortfolioCalculator
    {
        private readonly RestClient _restClient;

        public PortfolioCalculator(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Dictionary<string, decimal>> CalculatePortfolioBalanceAsync()
        {
            var balances = PortfolioConfig.Balances;
            var portfolioBalance = new Dictionary<string, decimal>();

            var tasks = balances.Keys.Select(async currency =>
            {
                try
                {
                    var ticker = await _restClient.GetTickerInfoAsync(currency + "USDT");
                    if (ticker != null)
                    {
                        portfolioBalance[currency] = balances[currency] * ticker.LastPrice;
                    }
                    else
                    {
                        Console.WriteLine($"Ticker info for {currency} is null.");
                        portfolioBalance[currency] = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching ticker info for {currency}: {ex.Message}");
                    portfolioBalance[currency] = 0;
                }
            });

            await Task.WhenAll(tasks);

            return portfolioBalance;
        }

        public async Task<Dictionary<string, decimal>> CalculatePortfolioBalanceInAllCurrenciesAsync()
        {
            var balances = PortfolioConfig.Balances;
            var portfolioBalance = new Dictionary<string, decimal>();
            var currencies = PortfolioConfig.Currencies;

            foreach (var targetCurrency in currencies)
            {
                var totalBalance = 0m;

                foreach (var balance in balances)
                {
                    var ticker = await _restClient.GetTickerInfoAsync(balance.Key + targetCurrency);
                    if (ticker != null)
                    {
                        totalBalance += balance.Value * ticker.LastPrice;
                    }
                    else
                    {
                        Console.WriteLine($"Ticker info for {balance.Key} to {targetCurrency} is null.");
                    }
                }

                portfolioBalance[targetCurrency] = totalBalance;
            }

            return portfolioBalance;
        }
    }
}