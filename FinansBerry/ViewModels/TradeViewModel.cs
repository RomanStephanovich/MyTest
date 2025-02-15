using CommunityToolkit.Mvvm.Input;
using FinansBerry.Models.Trade;
using FinansBerry.Services.Implemetations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinansBerry.WPF.ViewModels
{
    public class TradeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Trade> _trades;
        public ObservableCollection<Trade> Trades
        {
            get { return _trades; }
            set { _trades = value; OnPropertyChanged(); }
        }

        private readonly TradeService _tradeService;

        public ICommand LoadTradesCommand { get; }
        public ICommand SubscribeCommand { get; }
        public ICommand UnsubscribeCommand { get; }

        public TradeViewModel(TradeService tradeService)
        {
            _tradeService = tradeService;
            _tradeService.OnNewTradeReceived += trade => Trades.Add(trade);

            Trades = new ObservableCollection<Trade>();

            LoadTradesCommand = new AsyncRelayCommand(() => LoadTrades("BTCUSD", 100));
            SubscribeCommand = new AsyncRelayCommand(Subscribe);
            UnsubscribeCommand = new AsyncRelayCommand(Unsubscribe);
        }

        public async Task LoadTrades(string pair, int maxCount)
        {
            try
            {
                var trades = await _tradeService.GetLatestTradesAsync(pair, maxCount);
                Trades = new ObservableCollection<Trade>(trades);
                Console.WriteLine("Trades loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading trades: {ex.Message}");
            }
        }

        private async Task Subscribe()
        {
            try
            {
                await _tradeService.SubscribeToTradesAsync("BTCUSD");
                Console.WriteLine("Subscribed to trades.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to trades: {ex.Message}");
            }
        }

        private async Task Unsubscribe()
        {
            try
            {
                await _tradeService.UnsubscribeFromTradesAsync("BTCUSD");
                Console.WriteLine("Unsubscribed from trades.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unsubscribing from trades: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}