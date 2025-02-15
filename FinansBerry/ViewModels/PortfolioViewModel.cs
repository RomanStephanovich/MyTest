using CommunityToolkit.Mvvm.Input;
using FinansBerry.ConnectorAPIService.PortfolioConfig;
using FinansBerry.Services.Implemetations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinansBerry.WPF.ViewModels
{
    public class PortfolioViewModel : INotifyPropertyChanged
    {
        private readonly PortfolioService _portfolioService;
        private Dictionary<string, decimal> _portfolioBalances;

        public Dictionary<string, decimal> PortfolioBalances
        {
            get => _portfolioBalances;
            set { _portfolioBalances = value; OnPropertyChanged(); }
        }

        public ICommand LoadPortfolioCommand { get; }

        public PortfolioViewModel(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
            LoadPortfolioCommand = new AsyncRelayCommand(LoadPortfolio);
        }

        public async Task LoadPortfolio()
        {
            var assets = PortfolioConfig.Balances;
            PortfolioBalances = await _portfolioService.GetPortfolioBalanceAsync(assets);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}