using MahApps.Metro.Controls;
using ProgramDoObslugiZamowien.UI.ViewModel;
using System.Windows;

namespace ProgramDoObslugiZamowien.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
             _viewModel.Load();
        }
    }
}
