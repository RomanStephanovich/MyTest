using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.ConnectorAPIService.Endpoints
{
    public static class ApiEndpoints
    {
        private const string BaseUrl = "https://api-pub.bitfinex.com/v2/";

        public static string GetTradesUrl(string pair, int maxCount)
        {
            return $"{BaseUrl}trades/t{pair}/hist?limit={maxCount}";
        }

        public static string GetCandleSeriesUrl(string pair, string timeframe, long? count)
        {
            return $"{BaseUrl}candles/trade:{timeframe}:t{pair}/hist?limit={count}";
        }

        public static string GetTickerInfoUrl(string pair)
        {
            return $"{BaseUrl}ticker/t{pair}";
        }
    }
}
