using Prism.Events;
using ProgramDoObslugiZamowien.UI.Communication;
using ProgramDoObslugiZamowien.UI.Repository;
using ProgramDoObslugiZamowien.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProgramDoObslugiZamowien.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            EventAggregator eventAggregator = new EventAggregator();
            DataRepository dataRepository = new DataRepository();
            var mainWindow = new MainWindow(
                new MainViewModel(
                    new NavigationViewModel(eventAggregator, dataRepository),
                    dataRepository,
                    new ReportViewModel(
                        new CommunicationService()),
                    eventAggregator));
            mainWindow.Show();
        }
        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured."
                + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
