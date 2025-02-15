using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinansBerry.Services.Interfaces
{
    public interface IPortfoilioService
    {
        Task<Dictionary<string,decimal>> GetPortfolioBalanceAsync(Dictionary<string,decimal> assets);
    }
}
