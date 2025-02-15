using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FinansBerry.WPF.Models;
using FinansBerry.Services.Implemetations;
using System.Windows.Input;
using TestHQ;
using FinansBerry.API.Interfaces;
using FinansBerry.Services.Interfaces;
using FinansBerry.Models;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;

namespace FinansBerry.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public TradeViewModel TradeVM { get; }
        public PortfolioViewModel PortfolioVM { get; }
        public CandleViewModel CandleVM { get; }
        public TickerViewModel TickerVM { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ShowTradesCommand { get; }
        public ICommand ShowPortfolioCommand { get; }
        public ICommand ShowCandlesCommand { get; }
        public ICommand ShowTickerCommand { get; }
        public ICommand LoadCandlesCommand { get; }
        public ICommand SubscribeCommand { get; }
        public ICommand UnsubscribeCommand { get; }

        public MainViewModel(TradeViewModel tradeVM, PortfolioViewModel portfolioVM, CandleViewModel candleVM, TickerViewModel tickerVM)
        {
            TradeVM = tradeVM;
            PortfolioVM = portfolioVM;
            CandleVM = candleVM;
            TickerVM = tickerVM;

            ShowTradesCommand = new AsyncRelayCommand(async o =>
            {
                try
                {
                    await TradeVM.LoadTrades("BTCUSD", 100);
                    CurrentView = TradeVM;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error showing trades: {ex.Message}");
                }
            });

            ShowPortfolioCommand = new AsyncRelayCommand(async o =>
            {
                try
                {
                    await PortfolioVM.LoadPortfolio();
                    CurrentView = PortfolioVM;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error showing portfolio: {ex.Message}");
                }
            });

            ShowCandlesCommand = new AsyncRelayCommand(async o =>
            {
                try
                {
                    await CandleVM.LoadCandles();
                    CurrentView = CandleVM;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error showing candles: {ex.Message}");
                }
            });

            ShowTickerCommand = new AsyncRelayCommand(async o =>
            {
                try
                {
                    await TickerVM.LoadTicker();
                    CurrentView = TickerVM;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error showing ticker: {ex.Message}");
                }
            });

            LoadCandlesCommand = CandleVM.LoadCandlesCommand;
            SubscribeCommand = CandleVM.SubscribeCommand;
           // UnsubscribeCommand = CandleVM.UnsubscribeCommand;

            CurrentView = TradeVM;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RefreshData()
        {
            try
            {
                await TradeVM.LoadTrades("BTCUSD", 50);
                await TickerVM.LoadTicker();
                await CandleVM.LoadCandles();
                await PortfolioVM.LoadPortfolio();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing data: {ex.Message}");
            }
        }
    }
}