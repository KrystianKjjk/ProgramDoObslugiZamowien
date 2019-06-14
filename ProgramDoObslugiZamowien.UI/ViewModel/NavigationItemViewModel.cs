using Prism.Commands;
using Prism.Events;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.Event;
using ProgramDoObslugiZamowien.UI.FileManagment;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class NavigationItemViewModel
    {
        private IEventAggregator _eventAggregator;        
        public NavigationItemViewModel(string fullFilePath, IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            OpenReportViewCommand = new DelegateCommand(OnOpenDetailViewExcecute);
            FullFilePath = fullFilePath;
            DisplayFile = FileData.GetFileName(fullFilePath);
        }
        public string FullFilePath { get; }
        public List<Request> Requests { get; }
        public ICommand OpenReportViewCommand { get; }
        public string DisplayFile { get; set; }
        private void OnOpenDetailViewExcecute()
        {
            _eventAggregator
                .GetEvent<OpenFileReportViewEvent>()
                .Publish(
                new OpenFileReportViewEventArgs
                {
                    FullFilePath = this.FullFilePath
                });
        }
    }
}
