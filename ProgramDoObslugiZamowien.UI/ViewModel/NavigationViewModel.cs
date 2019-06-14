using Prism.Commands;
using Prism.Events;
using ProgramDoObslugiZamowien.UI.Communication;
using ProgramDoObslugiZamowien.UI.Event;
using ProgramDoObslugiZamowien.UI.FileManagment;
using ProgramDoObslugiZamowien.UI.Repository;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDailogService;
        private IDataRepository _dataRepository;
        public NavigationViewModel(IEventAggregator eventAggregator, IDataRepository dataRepository, IMessageDialogService messageDailogService)
        {
            _eventAggregator = eventAggregator;
            _messageDailogService=messageDailogService;
            _dataRepository = dataRepository;
            Files = new ObservableCollection<NavigationItemViewModel> ();
            AddFilesCommand = new DelegateCommand(OnAddFiles);

            _eventAggregator.GetEvent<RemoveFileEvent>()
                 .Subscribe(AfterFileDeleted);
        }
        public ObservableCollection<NavigationItemViewModel> Files { get; set; }
        public void Load()
        {

        }
        public ICommand AddFilesCommand { get; }

        private void AfterFileDeleted(RemoveFileEventArgs args)
        {
            _dataRepository.RemoveData(args.FullFilePath);

            NavigationItemViewModel fileItem = Files.Where(fI => fI.FullFilePath == args.FullFilePath).SingleOrDefault();
            Files.Remove(fileItem);
        }
        private void OnAddFiles()
        {
            string[] filePath = FileData.GetFiles();

            bool errorFound = false;
            foreach (var file in filePath)
            {
                if (FileData.IsAvailableFileExtention(file) && !IsFileAlreadyAdded(file))
                {
                    try
                    {
                        var result = AddDataFromFileIfExistsToDatabase(file);
                        if (result == true)
                            Files.Add(new NavigationItemViewModel(file, _eventAggregator));
                        else
                            _messageDailogService.ShowInfoDialog($"Plik {file} nie zawiera poprawnych danych, dlatego nie został dodany");
                    }
                    catch (FileFormatException Ffe)
                    {
                        //it shoudn't happend
                        _messageDailogService.ShowInfoDialog(Ffe.Message);
                    }
                    catch (Exception)
                    {
                        _messageDailogService.ShowInfoDialog($"Problem podczas otwierania lub odczytywania danych z pliku {file}. Sprawdź czy plik zawiera poprawne dane");
                    }    
                }
                else
                    errorFound = true;
            }
            if (errorFound)
                _messageDailogService.ShowInfoDialog("Co najmniej jeden z wybranych plików został już dodany lub posiada niepraidłowe rozszerzenie ");
        }
        private bool IsFileAlreadyAdded(string fullFilePath)
        {
            return Files.Any(f => f.FullFilePath == fullFilePath);
        }        
        private bool AddDataFromFileIfExistsToDatabase(string fullFilePath) //return true if data added to database
        {
            var requests = FileData.GetRequestsFromFile(fullFilePath);
            if (requests != null && requests.Count != 0)
            {
                _dataRepository.AddData(fullFilePath, requests);
                return true;
            }
            return false;
        }
    }
}
