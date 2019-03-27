using Prism.Events;
using ProgramDoObslugiZamowien.UI.Event;
using ProgramDoObslugiZamowien.UI.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class MainViewModel: ViewModelPropertyChanged
    {
        private IEventAggregator _eventAggregator;
        private IDataRepository _dataRepository;
        public INavigationViewModel NavigationViewModel { get; }
        public IReportViewModel ReportViewModel { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel,  
            IDataRepository dataRepository,
            IReportViewModel reportDetailViewModel,           
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _dataRepository = dataRepository;
            ReportViewModel = reportDetailViewModel;
            NavigationViewModel = navigationViewModel;

            _eventAggregator.GetEvent<OpenFileReportViewEvent>()
                .Subscribe(AfterOpenReportDetailView);
        }
        
        private void AfterOpenReportDetailView(OpenFileReportViewEventArgs args)
        {
            ReportViewModel.Load(args.Requests);
            
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
