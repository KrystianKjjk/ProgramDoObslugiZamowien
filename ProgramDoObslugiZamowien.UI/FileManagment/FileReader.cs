using System.Collections.Generic;
using System.IO;
using ProgramDoObslugiZamowien.Model;

namespace ProgramDoObslugiZamowien.UI.FileManagment
{
    public abstract class FileReader
    {
        protected string _fullFilePath;
        public abstract List<Request> ReadAndValidateRequests();
        public FileReader(string fullFilePath)
        {
            _fullFilePath = fullFilePath;
        }
        protected void CheckFileExtention(string ext)
        {
            if (ext != FileData.GetFileExtention(_fullFilePath))
                throw new FileFormatException("Błędne rozszerzenie pliku");
        }
    }
}