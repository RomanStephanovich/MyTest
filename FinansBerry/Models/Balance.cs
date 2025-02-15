using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.WPF.Models
{
    public class Balance
    {
        public string Currency { get; set; }  // Валюта (например, BTC)
        public decimal Amount { get; set; }   // Количество валюты
    }
}
