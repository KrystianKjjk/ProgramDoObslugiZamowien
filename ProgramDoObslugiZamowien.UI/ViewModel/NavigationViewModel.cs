using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.FileManagment;
using ProgramDoObslugiZamowien.UI.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private IEventAggregator _eventAggregator;
        private IDataRepository _dataRepository;
        public ObservableCollection<NavigationItemViewModel> Files { get; set; }
        public NavigationViewModel(IEventAggregator eventAggregator, DataRepository dataRepository)
        {
            _eventAggregator = eventAggregator;
            _dataRepository = dataRepository;
            Files = new ObservableCollection<NavigationItemViewModel> ();
            
            AddFiles = new DelegateCommand(OnAddFiles);
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
                        var requests = GetDataFromFileAndAddToDatabase(file);
                        if (requests.Count != 0)
                            Files.Add(new NavigationItemViewModel(file, requests, _eventAggregator));
                        else
                            MessageBox.Show("Plik nie zawiera poprawnych danych, dlatego nie został dodany");
                    }
                    catch(FileFormatException Ffe)
                    {
                        MessageBox.Show(Ffe.Message);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Problem podczas otwierania lub odczytywania danych z pliku. Sprawdź czy plik zawiera poprawne dane");
                    }    
                }
                else
                    errorFound = true;
            }
            if (errorFound)
                MessageBox.Show("Co najmniej jeden z wybranych plików został już dodany lub posiada niepraidłowe rozszerzenie "); //do it better 
        }

        private bool IsFileAlreadyAdded(string fullFilePath)
        {
            return Files.Any(f => f.FullFilePath == fullFilePath);
        }

        public void Load()
        {

        }

        public ICommand AddFiles { get; set; }

        private List<Request> GetDataFromFileAndAddToDatabase(string fullFilePath)
        {
            var requests = FileData.GetRequestsFromFile(fullFilePath);
            if(requests.Count !=0)
                _dataRepository.AddData(fullFilePath, requests);
            return requests;
        }

    }
}
