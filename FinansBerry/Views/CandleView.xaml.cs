using FinansBerry.API.Implemetations;
using FinansBerry.Services.Implemetations;
using FinansBerry.WPF.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinansBerry.WPF.Views
{
    /// <summary>
    /// Interaction logic for CandleView.xaml
    /// </summary>
    public partial class CandleView : UserControl
    {
        private readonly CandleViewModel _candleViewModel;

        // Parameterless constructor for XAML instantiation
        public CandleView()
        {
            InitializeComponent();
            _candleViewModel = new CandleViewModel(new CandleService(new ApiConnector(new RestClient(), new WebSocketConnector())));
            DataContext = _candleViewModel;
        }

        // Constructor with dependency injection
        public CandleView(CandleViewModel candleViewModel)
        {
            InitializeComponent();
            _candleViewModel = candleViewModel;
            DataContext = _candleViewModel;
        }

        private async void LoadCandlesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _candleViewModel.LoadCandles();
        }

        private async void SubscribeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _candleViewModel.Subscribe();
        }

      // public  async void UnsubscribeButton_Click(object sender, System.Windows.RoutedEventArgs e)
       // {
      //      await _candleViewModel.Unsubscribe();
      //  }
    }
}