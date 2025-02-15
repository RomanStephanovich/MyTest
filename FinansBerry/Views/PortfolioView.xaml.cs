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
    /// Interaction logic for Portfolio.xaml
    /// </summary>
    public partial class PortfolioView : UserControl
    {
        private readonly PortfolioViewModel _portfolioViewModel;

        // Parameterless constructor for XAML instantiation
        public PortfolioView()
        {
            InitializeComponent();
        }

        // Constructor with dependency injection
        public PortfolioView(PortfolioViewModel portfolioViewModel)
        {
            InitializeComponent();
            _portfolioViewModel = portfolioViewModel;
            DataContext = _portfolioViewModel;
        }
    }
}