using Prism.Commands;
using Prism.Events;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.Event;
using ProgramDoObslugiZamowien.UI.FileManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{

    public class NavigationItemViewModel
    {
        private string _displayFile;
        private IEventAggregator _eventAggregator;
        
        public string FullFilePath { get; }
        //public string FileExtention { get; }
        public NavigationItemViewModel(string fullFilePath,List<Request> requests, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            OpenReportViewCommand = new DelegateCommand(OnOpenDetailViewExcecute);
            FullFilePath = fullFilePath;
            Requests = requests;
            _displayFile = FileData.GetFileName(fullFilePath);
        }

        public List<Request> Requests { get; }
        private void OnOpenDetailViewExcecute()
        {
            //TODO: otworzyć plik, zdeserializować dane do obiektu w zależności od typu, przesłać event do mainwindow

           // var requests = FileData.GetRequestsFromFile(FullFilePath);


            _eventAggregator
                .GetEvent<OpenFileReportViewEvent>()
                .Publish(
                new OpenFileReportViewEventArgs
                {
                    Requests = this.Requests,
                    FullFilePath = this.FullFilePath
                });
        }

        

        public ICommand OpenReportViewCommand { get; }
        public string DisplayFile
        {
            get { return _displayFile; }
            set
            {
                _displayFile = value;
                //OnPropertyChanged();
            }
        }

    }
}
