using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.ConnectorAPIService.PortfolioConfig
{
    public static class PortfolioConfig
    {
        public static readonly Dictionary<string, decimal> Balances = new Dictionary<string, decimal>
        {
            { "BTC", 1 },
            { "XRP", 15000 },
            { "XMR", 50 },
            { "DASH", 30 }
        };

        public static readonly string[] Currencies = { "USDT", "BTC", "XRP", "XMR", "DASH" };
    }
}
