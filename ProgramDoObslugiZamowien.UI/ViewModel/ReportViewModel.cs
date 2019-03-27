using Prism.Commands;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.Communication;
using ProgramDoObslugiZamowien.UI.FileManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class ReportViewModel : ViewModelPropertyChanged, IReportViewModel
    {
        private ICommunicationService _communicationService;
        private List<Request> _requests;
        private List<Request> _selectedRequests;
         
        private string _choosenReport;
        private string _data;
        private string _selectedClientId;
        private string _minPrice;
        private string _maxPrice;
        public ReportViewModel(ICommunicationService communicationService)
        {

            _communicationService = communicationService;
            NoOfRequestCommand = new DelegateCommand(OnNoOfRequestCommand, CanReportCreate);
            TotalRequestsSumCommand = new DelegateCommand(OnTotalRequestsSumCommand, CanReportCreate);
            ListOfRequestsCommand = new DelegateCommand(OnListOfRequestsCommand, CanReportCreate);
            AverageRequestValueCommand = new DelegateCommand(OnAverageRequestValueCommand, CanReportCreate);
            NoOfRequestNameCommand = new DelegateCommand(OnNoOfRequestNameCommand, CanReportCreate);
            RequestsInRangeCommand = new DelegateCommand(OnRequestsInRangeCommand, CanRequestInRangeCommand);
            GenerateReportCommand = new DelegateCommand(OnGenerateReportCommand, CanGenerateReport);
            SortByName = new DelegateCommand(OnSortByNameCommand, CanReportCreate);
            SortByClientId = new DelegateCommand(OnSortByClientIdCommand, CanReportCreate);
            SelectedClientId = "";
            MinPrice = "";
            MaxPrice = "";
        }

        private bool CanGenerateReport()
        {
            return CanReportCreate() && ChoosenReport != "";
        }

        private void OnSortByClientIdCommand()
        {
            if (SelectedRequests != null)
                SelectedRequests = SelectedRequests.OrderBy(f => f.ClientId).ToList();
        }

        private void OnSortByNameCommand()
        {
            if(SelectedRequests !=null)
                SelectedRequests = SelectedRequests.OrderBy(f => f.Name).ToList();
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

        }

        public ICommand NoOfRequestCommand { get; }
        public ICommand TotalRequestsSumCommand { get; }
        public ICommand ListOfRequestsCommand { get; }
        public ICommand AverageRequestValueCommand { get; }
        public ICommand NoOfRequestNameCommand { get; }
        public ICommand RequestsInRangeCommand { get; }
        public ICommand GenerateReportCommand { get; }
        public ICommand SortByName { get; set; }
        public ICommand SortByClientId { get; set; }

        private void OnGenerateReportCommand()
        {
            if(_communicationService.ListOfData)
                FileData.GenerateReportJSON(SelectedRequests);
            else 
                FileData.GenerateReportJSON(ChoosenReport, Data);
        }

        private void OnRequestsInRangeCommand()
        {
            if(ValidateSelectedClientId())
            {
                SelectedRequests = GetRequestsForClient(SelectedClientId)
                    .Where(f => f.Price*f.Quantity >= double.Parse(MinPrice) && f.Price*f.Quantity <= double.Parse(MaxPrice)).ToList();
                ChooseReport(_communicationService.RequestsInRange(MinPrice, MaxPrice, SelectedClientId),"");
                return;
            }
            SelectedRequests = Requests
                .Where(f => f.Price*f.Quantity >= double.Parse(MinPrice) && f.Price*f.Quantity<= double.Parse(MaxPrice)).ToList();
            ChooseReport(_communicationService.RequestsInRange(MinPrice, MaxPrice), "");
        }

        private void OnNoOfRequestNameCommand()
        {

            int numberOfRequestName = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                numberOfRequestName = NoOfRequestGroupedByName(SelectedClientId);
                ChooseReport(_communicationService.RequestsGroupedByName(SelectedClientId), numberOfRequestName.ToString());
                return;
            }
            ShowRequests();
            numberOfRequestName = NoOfRequestGroupedByName();
            ChooseReport(_communicationService.RequestsGroupedByName(), numberOfRequestName.ToString());
        }

        private void OnAverageRequestValueCommand()
        {

            double Average = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                var clientRequests = GetRequestsForClient(SelectedClientId);                
                Average = SumOfRequests(clientRequests)/ NoOfUniqeRequestsForSpecificClient(SelectedClientId);
                ChooseReport(_communicationService.AverageRequestsValue(SelectedClientId), Average.ToString());
                return;
            }
            ShowRequests();
            Average = SumOfRequests(Requests) / NoOfUnigeRequests();
            //Average = Requests.Average(f => f.Price);
            ChooseReport(_communicationService.AverageRequestsValue(), Average.ToString());
        }

        private void OnListOfRequestsCommand()
        {
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                ChooseReport(_communicationService.ListOfRequests(SelectedClientId), "");
                return;
            }
            ShowRequests();
            ChooseReport(_communicationService.ListOfRequests(), "");
        }

        private void OnTotalRequestsSumCommand()
        {
            double sum = 0;
            if (ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                var clientRequests = GetRequestsForClient(SelectedClientId);
                sum = SumOfRequests(clientRequests);
                ChooseReport(_communicationService.SumOfRequests(SelectedClientId), sum.ToString());
                return; 
            }
            ShowRequests();
            sum = SumOfRequests(Requests);
            ChooseReport(_communicationService.SumOfRequests(), sum.ToString());
        }


        private void OnNoOfRequestCommand()
        {
            if(ValidateSelectedClientId())
            {
                ShowRequests(SelectedClientId);
                int NoOfRequestsForClient = NoOfUniqeRequestsForSpecificClient(SelectedClientId);
                ChooseReport(_communicationService.UnigueRequests(SelectedClientId), NoOfRequestsForClient.ToString());
                return;
            }
            ShowRequests();
            int NoOfRequests = NoOfUnigeRequests();
            ChooseReport(_communicationService.UnigueRequests(), NoOfRequests.ToString());
        }
        private double SumOfRequests(List<Request> requests)
        {
            double sum = 0;
            sum = requests.Aggregate(sum, (s, f) => s + f.Price * f.Quantity);
            return sum;
        }
        private List<Request> GetRequestsForClient(string clientId)
        {
            return Requests.Where(f => f.ClientId == clientId).ToList();
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
        private bool ValidateSelectedClientId()
        {
            if (SelectedClientId == "")
            {
                return false;
            }
            else if(!Request.ValidateClientId(SelectedClientId))
            {
                MessageBox.Show("Wybrany identyfikator klienta jest nieprawidłowy");
                return false;
            }
            return true;
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
        public string ChoosenReport
        {
            get { return _choosenReport; }
            set
            {
                _choosenReport = value;
                OnPropertyChanged();
                ((DelegateCommand)GenerateReportCommand).RaiseCanExecuteChanged();
            }
        }

        public void Load(List<Request> requests)
        {
            Requests = requests;
            ShowRequests();
            RaiseCanReportCreate();
            ChooseReport("", "");
        }
        private int NoOfUniqeRequestsForSpecificClient(string clientId /*,List<Request> requests*/)
        {
            List<long> vs = new List<long>();

            foreach (var request in GetRequestsForClient(clientId))
            {
                if (!vs.Contains(request.RequestId))
                    vs.Add(request.RequestId);
            }
            return vs.Count();
        }
        private int NoOfUnigeRequests()
        {
            List<string> clients = new List<string>();

            foreach (var request in Requests)// find all clients
            {
                if (!clients.Contains(request.ClientId))
                    clients.Add(request.ClientId);
            }
            int count = 0;
            return clients.Aggregate(count, (c, f) => c + NoOfUniqeRequestsForSpecificClient(f));
            //foreach (var client in clients)
            //{
            //    count += NoOfUniqeRequestsForSpecificClient(client);
            //}
            //return count;
        }
        private int NoOfRequestGroupedByName(string clientId=null)
        {
            int FindNumber(List<Request> requests)
            {
                var listOfNames = new List<string>();
                foreach (var request in requests)
                {
                    if (!listOfNames.Contains(request.Name))
                        listOfNames.Add(request.Name);
                }
                return listOfNames.Count();
            }            
            if (clientId!=null)
                return FindNumber(GetRequestsForClient(clientId));
            return FindNumber(Requests);
        }
        private void ShowRequests(string clientId = null )
        {
            if (clientId != null)
            {
                SelectedRequests = GetRequestsForClient(clientId);
                return;
            }
            SelectedRequests = Requests;
        }
        private void ChooseReport(string choosenReport, string data)
        {
            ChoosenReport = choosenReport;
            Data = data;
        }
    }
}
