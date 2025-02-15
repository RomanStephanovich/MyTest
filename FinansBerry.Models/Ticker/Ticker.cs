using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Models.Ticker
{
    // Ensure that the Ticker class matches the structure of the JSON array response
    public class Ticker
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal DailyChange { get; set; }
        public decimal DailyChangePercentage { get; set; }
        public decimal Volume { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
    }
}
