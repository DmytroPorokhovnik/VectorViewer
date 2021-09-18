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
using VectorViewer.ViewModels;

namespace VectorViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly Color _axisColor = Color.FromArgb(255, 150, 150, 150);

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void OuterCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Canvas canvas)) return;          
            var xAxis = new Line()
            {
                X1 = 0,
                Y1 = canvas.ActualHeight / 2,
                X2 = canvas.ActualWidth,
                Y2 = canvas.ActualHeight / 2,
                Stroke = new SolidColorBrush(_axisColor)
            };

            var yAxis = new Line()
            {
                X1 = canvas.ActualWidth / 2,
                Y1 = 0,
                X2 = canvas.ActualWidth / 2,
                Y2 = canvas.ActualHeight,
                Stroke = new SolidColorBrush(_axisColor)
            };
            
            OuterCanvas.Children.Add(xAxis);
            OuterCanvas.Children.Add(yAxis);
        }
    }
}
