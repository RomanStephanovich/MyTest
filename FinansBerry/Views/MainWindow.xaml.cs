using FinansBerry.API.Implemetations;
using FinansBerry.Services.Implemetations;
using FinansBerry.WPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;


namespace FinansBerry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PortfolioCalculator _portfolioCalculator;

        public MainWindow()
        {
            InitializeComponent();

            var apiConnector = new ApiConnector(new RestClient(), new WebSocketConnector());
            var tradeVM = new TradeViewModel(new TradeService(apiConnector));
            var portfolioVM = new PortfolioViewModel(new PortfolioService(apiConnector));
            var candleVM = new CandleViewModel(new CandleService(apiConnector));
            var tickerVM = new TickerViewModel(new TickerService(apiConnector));
            var restClient = new RestClient();
            _portfolioCalculator = new PortfolioCalculator(restClient);

            this.DataContext = new MainViewModel(tradeVM, portfolioVM, candleVM, tickerVM);
            Console.WriteLine("Application started.");
        }

        private async void RefreshPortfolioButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var balance = await _portfolioCalculator.CalculatePortfolioBalanceInAllCurrenciesAsync();
                PortfolioDataGrid.ItemsSource = balance.Select(b => new { Currency = b.Key, Balance = b.Value }).ToList();
                Console.WriteLine("Portfolio refreshed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing portfolio: {ex.Message}");
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = (MainViewModel)this.DataContext;
                await vm.RefreshData();
                Console.WriteLine("Data refreshed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing data: {ex.Message}");
            }
        }
    }
}