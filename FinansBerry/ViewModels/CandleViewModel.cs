using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinansBerry.Models.Candle;
using FinansBerry.Services.Implemetations;

namespace FinansBerry.WPF.ViewModels
{
    public class CandleViewModel : INotifyPropertyChanged
    {
        private readonly CandleService _candleService;
        private ObservableCollection<Candle> _candles;

        public ObservableCollection<Candle> Candles
        {
            get => _candles;
            set
            {
                _candles = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCandlesCommand { get; }
        public ICommand SubscribeCommand { get; }

        public ICommand UnsubscribeCommand;

        public CandleViewModel(CandleService candleService)
        {
            _candleService = candleService;
            _candles = new ObservableCollection<Candle>();
            _candleService.OnNewCandle += HandleNewCandle;

            LoadCandlesCommand = new AsyncRelayCommand(LoadCandles);
            SubscribeCommand = new AsyncRelayCommand(Subscribe);
            UnsubscribeCommand = new AsyncRelayCommand(UnSubscribe);
        }

        public async Task LoadCandles()
        {
            try
            {
                Console.WriteLine("Loading candles...");
                var candles = await _candleService.GetCandleSeriesAsync("BTCUSD", "1m", 100);
                Candles = new ObservableCollection<Candle>(candles);
                Console.WriteLine("Candles loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading candles: {ex.Message}");
            }
        }

        public async Task Subscribe()
        {
            try
            {
                await _candleService.SubscribeToCandlesAsync("BTCUSD", 60);
                Console.WriteLine("Subscribed to candles.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to candles: {ex.Message}");
            }
        }



        public async Task UnSubscribe()
        {
            try
            {
                await _candleService.UnsubscribeFromCandlesAsync("BTCUSD");
                Console.WriteLine("Subscribed to candles.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error subscribing to candles: {ex.Message}");
            }
        }

        private void HandleNewCandle(Candle candle)
        {
            App.Current.Dispatcher.Invoke(() => Candles.Add(candle));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
