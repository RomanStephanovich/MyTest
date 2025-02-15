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
    /// Interaction logic for TickerView.xaml
    /// </summary>
    public partial class TickerView : UserControl
    {
        private readonly TickerViewModel _tickerViewModel;

        // Parameterless constructor for XAML instantiation
        public TickerView()
        {
            InitializeComponent();
        }

        // Constructor with dependency injection
        public TickerView(TickerViewModel tickerViewModel)
        {
            InitializeComponent();
            _tickerViewModel = tickerViewModel;
            DataContext = _tickerViewModel;
        }
    }
}
