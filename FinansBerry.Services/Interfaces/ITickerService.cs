using FinansBerry.Models.Ticker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Services.Interfaces
{
    public interface ITickerService
    {
        Task<Ticker> GetTickerAsync(string pair);
    }
}
