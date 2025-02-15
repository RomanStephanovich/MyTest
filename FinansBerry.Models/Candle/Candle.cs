using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Models.Candle
{
    public class Candle
    {
        public string Pair { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalVolume { get; set; }
        public DateTimeOffset OpenTime { get; set; }
    }
}
