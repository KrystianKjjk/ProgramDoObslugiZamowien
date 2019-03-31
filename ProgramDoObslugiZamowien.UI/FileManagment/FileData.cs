using Microsoft.Win32;
using Newtonsoft.Json;
using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;
using System.IO;

namespace ProgramDoObslugiZamowien.UI.FileManagment
{
    public static class FileData
    {
        public static string GetFileName(string FilePath)
        {
            for (int i = FilePath.Length - 1; i > 0; i--)
            {
                if (FilePath[i] == '\\')
                    return FilePath.Remove(0, i + 1);
            }
            return FilePath;
        }
        public static string GetFileExtention(string FileName)
        {
            for (int i = FileName.Length - 1; i > 0; i--)
            {
                if (FileName[i] == '.')
                    return FileName.Remove(0, i);
            }
            return FileName; // or maybe throw exception? 
        }
        public static bool IsAvailableFileExtention(string fullFilePath)
        {
            var fileExt = GetFileExtention(fullFilePath);
            return (fileExt == ".csv" || fileExt == ".json" || fileExt == ".xml") ? true : false;
        }
        public static string ExtentionFilters()
        {
            return "file (*.csv)(*.json)(*.xml)|*.csv;*.json;*.xml|CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml|JSON files (*.json)|*.json";
        }
        public static string[] GetFiles()
        {
            var openfileDialog = new OpenFileDialog
            {
                Filter = FileData.ExtentionFilters(),
                Multiselect = true
            };
            openfileDialog.ShowDialog();
            return openfileDialog.FileNames;
        }
        private static string GetFileNameToSave()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files(*.json) | *.json",
                ValidateNames = true,
                AddExtension = true
            };

            saveFileDialog.ShowDialog();
            string fullFilePath = saveFileDialog.FileName;
            if (GetFileExtention(fullFilePath) != ".json")
                fullFilePath += ".json";
            return fullFilePath;
        }
        public static List<Request> GetRequestsFromFile(string fullFilePath)
        {
            FileReader fileReader;
            string fileExtention = GetFileExtention(fullFilePath);

            switch (fileExtention)
            {
                case ".csv":
                    fileReader = new CsvFileReader(fullFilePath);
                        break;
                case ".json":
                    fileReader = new JsonFileReader(fullFilePath);
                    break;
                case ".xml":
                    fileReader = new XmlFileReader(fullFilePath);
                    break;
                default:
                    throw new FileFormatException("Niepoprawne rozszerzenie pliku");

            }
            List<Request> requests = fileReader.ReadAndValidateRequests();
            return requests;
        }
        public static void GenerateReportJSON (List<Request> requests)
        {

            var requestCollection = new RequestCollection(requests);

            var fullFilePath = GetFileNameToSave();

            using (StreamWriter file = File.CreateText(@fullFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, requestCollection);
            }
        }
        public static void GenerateReportJSON(string report, string data)
        {
            var anonimObject = new { Data = report, Value=data};
            var fullFilePath = GetFileNameToSave();

            using (StreamWriter file = File.CreateText(@fullFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, anonimObject);
            }
        }
    }
}
