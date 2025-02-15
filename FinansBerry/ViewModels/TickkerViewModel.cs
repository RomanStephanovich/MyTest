using CommunityToolkit.Mvvm.Input;
using FinansBerry.Models.Ticker;
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
    public class TickerViewModel : INotifyPropertyChanged
    {
        private readonly TickerService _tickerService;
        private Ticker _ticker;

        public Ticker Ticker
        {
            get => _ticker;
            set { _ticker = value; OnPropertyChanged(); }
        }

        public ICommand LoadTickerCommand { get; }

        public TickerViewModel(TickerService tickerService)
        {
            _tickerService = tickerService;
            LoadTickerCommand = new AsyncRelayCommand(LoadTicker);
        }

        public async Task LoadTicker()
        {
            Ticker = await _tickerService.GetTickerAsync("BTCUSD");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
