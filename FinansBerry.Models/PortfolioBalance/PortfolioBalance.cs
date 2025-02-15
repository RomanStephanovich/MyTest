using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Models.PortfolioBalance
{
    public class Portfolio
    {
        public string Asset { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
