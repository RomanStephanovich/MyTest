﻿using FinansBerry.API.Implemetations;
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
    /// Interaction logic for TradeView.xaml
    /// </summary>
    public partial class TradeView : UserControl
    {
        private readonly TradeViewModel _tradeViewModel;

        // Parameterless constructor for XAML instantiation
        public TradeView()
        {
            InitializeComponent();
        }

        // Constructor with dependency injection
        public TradeView(TradeViewModel tradeViewModel)
        {
            InitializeComponent();
            _tradeViewModel = tradeViewModel;
            DataContext = _tradeViewModel;
        }
    }
}
