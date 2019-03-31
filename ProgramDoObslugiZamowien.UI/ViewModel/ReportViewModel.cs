using Prism.Commands;
using Prism.Events;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.Communication;
using ProgramDoObslugiZamowien.UI.DataManagment;
using ProgramDoObslugiZamowien.UI.Event;
using ProgramDoObslugiZamowien.UI.FileManagment;
using System.Collections.Generic;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class ReportViewModel : ViewModelPropertyChanged, IReportViewModel
    {
        private string _fullFilePath;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDailogService;
        private ICommunicationService _communicationService;
        private List<Request> _requests;
        private List<Request> _selectedRequests;
         
        private string _selectedReport;
        private string _data;
        private string _selectedClientId;
        private string _minPrice;
        private string _maxPrice;
        private string _selectedFile;
        public ReportViewModel(ICommunicationService communicationService, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDailogService = messageDialogService;
            _communicationService = communicationService;
            NoOfRequestCommand = new DelegateCommand(OnNoOfRequestCommand, CanReportCreate);
            TotalRequestsSumCommand = new DelegateCommand(OnTotalRequestsSumCommand, CanReportCreate);
            ListOfRequestsCommand = new DelegateCommand(OnListOfRequestsCommand, CanReportCreate);
            AverageRequestValueCommand = new DelegateCommand(OnAverageRequestValueCommand, CanReportCreate);
            NoOfRequestNameCommand = new DelegateCommand(OnNoOfRequestGroupedByNameCommand, CanReportCreate);
            RequestsInRangeCommand = new DelegateCommand(OnRequestsInRangeCommand, CanRequestInRangeCommand);
            GenerateReportCommand = new DelegateCommand(OnGenerateReportCommand, CanGenerateReport);
            SortByName = new DelegateCommand(OnSortByNameCommand, CanReportCreate);
            SortByClientId = new DelegateCommand(OnSortByClientIdCommand, CanReportCreate);
            RemoveFileCommand = new DelegateCommand(OnRemoveFile, CanReportCreate);
            SelectedClientId = "";
            MinPrice = "";
            MaxPrice = "";
            SelectedFile = "";
        }

        public string MaxPrice
        {
            get { return _maxPrice; }
            set
            {
                _maxPrice = value;
                OnPropertyChanged();
                ((DelegateCommand)RequestsInRangeCommand).RaiseCanExecuteChanged();
            }
        }
        public string MinPrice
        {
            get { return _minPrice; }
            set
            {
                _minPrice = value;
                OnPropertyChanged();
                ((DelegateCommand)RequestsInRangeCommand).RaiseCanExecuteChanged();
            }
        }
        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged();
            }
        }
        public string SelectedClientId
        {
            get { return _selectedClientId; }
            set
            {
                _selectedClientId = value;
                OnPropertyChanged();
            }
        }
        public List<Request> SelectedRequests
        {
            get { return _selectedRequests; }
            set
            {
                _selectedRequests = value;
                OnPropertyChanged();
            }
        }
        public List<Request> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged();
            }
        }
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }
        public string SelectedReport
        {
            get { return _selectedReport; }
            set
            {
                _selectedReport = value;
                OnPropertyChanged();
                ((DelegateCommand)GenerateReportCommand).RaiseCanExecuteChanged();
            }
        }
        public void Load(List<Request> requests, string fullFilePath)
        {
            Requests = requests;
            _fullFilePath = fullFilePath;
            ShowRequests();
            RaiseCanReportCreate();
            SetSelectedReport("", "");
            SelectedFile = FileData.GetFileName(fullFilePath);
        }
        public ICommand NoOfRequestCommand { get; }
        public ICommand TotalRequestsSumCommand { get; }
        public ICommand ListOfRequestsCommand { get; }
        public ICommand AverageRequestValueCommand { get; }
        public ICommand NoOfRequestNameCommand { get; }
        public ICommand RequestsInRangeCommand { get; }
        public ICommand GenerateReportCommand { get; }
        public ICommand SortByName { get; }
        public ICommand SortByClientId { get; }
        public ICommand RemoveFileCommand { get; }

        private bool CanGenerateReport()
        {
            return CanReportCreate() && SelectedReport != "";

        }
        private bool CanReportCreate()
        {
            return Requests != null;
        }
        private bool CanRequestInRangeCommand()
        {
            return double.TryParse(MinPrice, out var no) && double.TryParse(MaxPrice, out var no2)
                && CanReportCreate() && double.Parse(MaxPrice) >= double.Parse(MinPrice);
        }
        private void RaiseCanReportCreate()
        {
            ((DelegateCommand)NoOfRequestCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)TotalRequestsSumCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ListOfRequestsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AverageRequestValueCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NoOfRequestNameCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RequestsInRangeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)GenerateReportCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SortByName).RaiseCanExecuteChanged();
            ((DelegateCommand)SortByClientId).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();

        }

        private void OnRemoveFile()
        {
            var result = _messageDailogService.ShowOkCancelDialog("Czy na pewno chcesz usunąć plik oraz jego dane z programu?", "Pytanie");
            if (result == MessageDialogResult.OK)
            {
                ClearData();

                _eventAggregator.GetEvent<RemoveFileEvent>()
                    .Publish(new RemoveFileEventArgs
                    { FullFilePath = _fullFilePath });
            }
        }
        private void OnGenerateReportCommand()
        {
            if(_communicationService.IsListOfData)
                FileData.GenerateReportJSON(SelectedRequests);
            else 
                FileData.GenerateReportJSON(SelectedReport, Data);
        }
        private void OnSortByClientIdCommand()
        {
            if (SelectedRequests != null)
                SelectedRequests = ReportDataGenerator.Sort(SelectedRequests, nameof(Request.ClientId));
        }
        private void OnSortByNameCommand()
        {
            if (SelectedRequests != null)
                SelectedRequests = ReportDataGenerator.Sort(SelectedRequests, nameof(Request.Name));
        }
        private void OnRequestsInRangeCommand()
        {
            if(ValidateSelectedClientId())
            {
                SelectedRequests = ReportDataGenerator.RequestsInRange(Requests, DoubleParse(MinPrice), DoubleParse(MaxPrice), SelectedClientId);
                SetSelectedReport(_communicationService.RequestsInRange(SelectedClientId, MinPrice, MaxPrice),"");
                return;
            }
            SelectedRequests = ReportDataGenerator.RequestsInRange(Requests, DoubleParse(MinPrice), DoubleParse(MaxPrice));
            SetSelectedReport(_communicationService.RequestsInRange(MinPrice, MaxPrice), "");
        }
        private void OnNoOfRequestGroupedByNameCommand()
        {
            int numberOfRequestName = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                numberOfRequestName = ReportDataGenerator.NoOfRequestGroupedByName(Requests, SelectedClientId);
                SetSelectedReport(_communicationService.RequestsGroupedByName(SelectedClientId), numberOfRequestName.ToString());
                return;
            }
            ShowRequests();
            numberOfRequestName = ReportDataGenerator.NoOfRequestGroupedByName(Requests);
            SetSelectedReport(_communicationService.RequestsGroupedByName(), numberOfRequestName.ToString());
        }
        private void OnAverageRequestValueCommand()
        {
            double Average = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                Average = ReportDataGenerator.AverageRequests(Requests, SelectedClientId);
                SetSelectedReport(_communicationService.AverageRequestsValue(SelectedClientId), Average.ToString());
                return;
            }
            ShowRequests();
            Average = ReportDataGenerator.AverageRequests(Requests);
            SetSelectedReport(_communicationService.AverageRequestsValue(), Average.ToString());
        }
        private void OnListOfRequestsCommand()
        {
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                SetSelectedReport(_communicationService.ListOfRequests(SelectedClientId), "");
                return;
            }
            ShowRequests();
            SetSelectedReport(_communicationService.ListOfRequests(), "");
        }
        private void OnTotalRequestsSumCommand()
        {
            double sum = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                sum = ReportDataGenerator.SumOfRequestsPrice(Requests, SelectedClientId);
                SetSelectedReport(_communicationService.SumOfRequests(SelectedClientId), sum.ToString());
                return; 
            }
            ShowRequests();
            sum = ReportDataGenerator.SumOfRequestsPrice(Requests);
            SetSelectedReport(_communicationService.SumOfRequests(), sum.ToString());
        }        
        private void OnNoOfRequestCommand()
        {
            if(ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                int NoOfRequestsForClient = ReportDataGenerator.NoOfUniqeRequests(Requests, SelectedClientId);
                SetSelectedReport(_communicationService.UnigueRequests(SelectedClientId), NoOfRequestsForClient.ToString());
                return;
            }
            ShowRequests();
            int NoOfRequests = ReportDataGenerator.NoOfUniqeRequests(Requests);
            SetSelectedReport(_communicationService.UnigueRequests(), NoOfRequests.ToString());
        }

        private double DoubleParse(string data)
        {
            return double.Parse(data);
        }
        private bool ValidateSelectedClientId()
        {
            if (SelectedClientId == "")
                return false;
            else if (!Request.ValidateClientId(SelectedClientId))
            {
                _messageDailogService.ShowInfoDialog("Wybrany identyfikator klienta jest nieprawidłowy");
                return false;
            }
            return true;
        }
        private void ClearData()
        {
            SelectedRequests = null;
            Requests = null;

            SelectedClientId = "";
            MinPrice = "";
            MaxPrice = "";

            Data = "";
            SelectedReport = "";
            SelectedFile = "";
            RaiseCanReportCreate();
        }
        private void ShowRequests(string clientId = null )
        {
            if (clientId == null)
            {
                SelectedRequests = Requests;                
                return;
            }
            SelectedRequests = ReportDataGenerator.GetRequests(Requests, clientId);
        }
        private void SetSelectedReport(string selectedReport, string data)
        {
            SelectedReport = selectedReport;
            Data = data;
        }
    }
}
